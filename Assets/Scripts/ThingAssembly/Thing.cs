using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Text.RegularExpressions;
using UltimateGameTools.MeshSimplifier;


namespace ThingSpace
{
    [RequireComponent(typeof(Boid))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(RuntimeMeshSimplifier))]
    public abstract class Thing : MonoBehaviour
    {
        public Boid motor
        {
            get { return GetComponent<Boid>(); }
        }

        bool inCD;
        float cdLength = 0.25f;

        //child class will change those:
        public int meshIndex;
        public double width = 1, height = 1, depth = 1, speed = 1, seperation = 1, cohesion = 1, red = 1, green = 1, blue = 1, spikyness = 0.1f;
        ///....................................
        Color color;
        Color accentColor;




        public bool attached;

        bool _dead;
        public bool dead
        {
            get { return _dead; }
            set
            {
                _dead = value;
                if (value)
                {
                    ThingGod.god.RemoveFromFlock(this);
                }
                else
                {
                    ThingGod.god.AddToFlock(this);
                }
            }
        }


        public float vertexPercentage = 1;

        public UnityEngine.UI.Text plate = null;


        float initialVertexCount;
        public float vertexCount { get; private set; }



        public Bounds bounds
        {
            get { return GetComponent<Renderer>().bounds; }
        }


        //MONOBEHAVIOUR///////////////////////////////////////


        private void Awake()
        {
            StopAllCoroutines();
            Init();
        }

        void Start()
        {

            bool isOriginal = GetComponent<MeshFilter>().sharedMesh == null;

            if (isOriginal)
            {


                gameObject.name = this.GetType().Name;
                if (meshIndex < 0 || meshIndex >= ThingGod.god.availableModels.Count) meshIndex = (int)(UnityEngine.Random.value * ThingGod.god.availableModels.Count);
                var mesh = ThingGod.god.availableModels[meshIndex].GetComponentInChildren<MeshFilter>().sharedMesh;
                GetComponent<MeshFilter>().mesh = mesh;
                initialVertexCount = mesh.vertexCount;
                vertexCount = initialVertexCount;

                int matCount = GetComponent<MeshRenderer>().materials.Length;
                Material[] mats = new Material[matCount];
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = ThingGod.god.thingMat;
                    mats[i].SetVector("_VertexDisScale", Vector3.one * (float)spikyness);
                }
                GetComponent<MeshRenderer>().materials = mats;

                //GetComponent<MeshRenderer>().material.SetVector("_VertexDisScale", Vector3.one * (float)spikyness);
                //var myMat = GetComponent<MeshRenderer>().material;
                //calculate accent color
                //set color
                color = new Color((float)red, (float)green, (float)blue);
                float h, s, v;
                Color.RGBToHSV(color, out h, out s, out v);
                accentColor = Color.HSVToRGB((((h * 360) + 180f) % 360) / 360f, Mathf.Clamp01(s), Mathf.Clamp01(v));
                //set color


                foreach (var mat in GetComponent<MeshRenderer>().materials)
                {


                    mat.SetColor("_ColorA1", color);
                    mat.SetColor("_ColorA2", accentColor);
                }





                //collider
                var collider = GetComponent<MeshCollider>();
                collider.sharedMesh = mesh;
                collider.convex = true;

                //set size
                gameObject.transform.localScale = new Vector3((float)width, (float)height, (float)depth);


            }



            StartCoroutine(IntervalBasedActions());
            StartCoroutine(ResetCD());
        }



        IEnumerator ResetCD()
        {
            while (true)
            {
                yield return new WaitForSeconds(cdLength);
                inCD = false;
            }
        }

        void Update()
        {
            motor.inEffect = !attached;

            if (plate != null)
            {
                plate.transform.position = transform.position + (CameraSwitcher.main.useMain ? 1 : 2) * Vector3.up;
                plate.text = name + ": " + vertexCount;
                plate.transform.rotation = Quaternion.LookRotation(plate.transform.position - CameraSwitcher.main.ActiveCam.position, CameraSwitcher.main.ActiveCam.up);
            }


            motor.speed = (float)speed;
            motor.cohWeight = (float)cohesion;
            motor.seekWeight = (float)seperation;
        }




        //MONOBEHAVIOUR///////////////////////////////////////


        public void ResetFlags()
        {
            dead = false;
            attached = false;
        }
        void ChangeVertexAmount(Thing who, float change)
        {


            var runtimeSimplifier = who.GetComponent<RuntimeMeshSimplifier>();
            who.vertexPercentage = Mathf.Clamp(who.vertexPercentage + change, 0.03f, 1f);
            //Debug.LogFormat("{0} now will have {1}% vertices, changed {2}", who.name, who.vertexPercentage * 100, change);
            runtimeSimplifier.Simplify(who.vertexPercentage * 100);

            if (who == this)
            {
                vertexCount = GetComponent<MeshFilter>().mesh.vertexCount;
            }

        }

        public void Steal(Thing another)
        {

            if (another == null || another == this || inCD || !another.gameObject.activeInHierarchy || !this.gameObject.activeInHierarchy) return;
            //Debug.Log(name + " steal " + another.name);

            ChangeVertexAmount(another, -0.1f);
            ChangeVertexAmount(this, 0.1f);


            if (ThingGod.StealEvent != null) ThingGod.StealEvent(this, another);
            inCD = true;
        }

        public void Gift(Thing another)
        {

            if (another == null || another == this || inCD || !another.gameObject.activeInHierarchy || !this.gameObject.activeInHierarchy) return;

            ChangeVertexAmount(another, +0.1f);
            ChangeVertexAmount(this, -0.1f);

            // another.Tuli += Tuli;
            // Tuli = 0;
            //Debug.Log(name + " gift " + another.name);
            if (ThingGod.GiftingEvent != null) ThingGod.GiftingEvent(this, another);
            //Gifting, voluntarily transform one’s own Tulis to others;

            inCD = true;
        }

        //public void Stick(Thing another)
        //{
        //    if (another == null || another == this || inCD) return;
        //    if (attached) return;
        //    Debug.Log(name + " stick " + another.name);
        //    //Follow, attach onto another thing for a limited period of time;
        //    transform.position = another.transform.position + another.transform.GetComponent<Collider>().bounds.extents.x * (transform.position - another.transform.position).normalized;
        //    motor.rb.velocity = Vector3.zero;
        //    //create a new joint to connect
        //    var myJoint = gameObject.GetComponent<CharacterJoint>();
        //    if (myJoint == null) myJoint = gameObject.AddComponent<CharacterJoint>();
        //    //connect to rb
        //    myJoint.connectedBody = another.motor.rb;
        //    if (ThingGod.StickEvent != null) ThingGod.StickEvent(this, another);
        //    attached = true;
        //    //release        
        //    Invoke("ReleaseSticking", 10);
        //    inCD = true;
        //}

        public void Clone(Thing another)
        {
            if (another == null || another == this || inCD || !another.gameObject.activeInHierarchy || !this.gameObject.activeInHierarchy) return;
            if (!CloneRegulator.instance.canClone) return;
            var cloneLayer = Regex.Matches(another.gameObject.name, "(Clone)").Count;
            if (cloneLayer > 5) return;
            //Debug.Log(name + " mate " + another.name);
            ThingGod.god.CloneThing(another, transform.position, another.transform.localScale * 0.9f);
            if (ThingGod.CloneEvent != null) ThingGod.CloneEvent(this, another);
            //Mate, give birth to a baby that resembles other thing;
            inCD = true;
        }

        public void Erase(Thing another)
        {
            if (another == null || another == this || inCD || !another.gameObject.activeInHierarchy || !this.gameObject.activeInHierarchy) return;
            //Debug.Log(name + " Erase " + another.name);

            Destroy(another.plate.gameObject);
            Destroy(another.GetComponent<Thing>());
            Destroy(another.GetComponent<Boid>());
            Destroy(another.GetComponent<RuntimeMeshSimplifier>());
            Destroy(another.GetComponent<MeshSimplify>());
            if (ThingGod.EraseEvent != null) ThingGod.EraseEvent(this, another);
            inCD = true;
        }

        public void Group(Thing another)
        {
            if (another == null || another == this || inCD || !another.gameObject.activeInHierarchy || !this.gameObject.activeInHierarchy) return;

            motor.cohWeight *= 3f;
            motor.aliWeight *= 3f;
            motor.seekWeight /= 3f;
        }

        public void Hide(Thing another)
        {
            if (another == null || another == this || inCD || !another.gameObject.activeInHierarchy || !this.gameObject.activeInHierarchy) return;

            motor.cohWeight /= 3f;
            motor.aliWeight /= 3f;
            motor.seekWeight *= 3f;
        }

        public void Seek(Thing another)
        {
            if (another == null || another == this || inCD || !another.gameObject.activeInHierarchy || !this.gameObject.activeInHierarchy) return;
            //Aim, walk towards the direction of a shan, an er, a monolith, or the tuli mountain. 
            motor.target = another.transform;
            if (ThingGod.SeekEvent != null) ThingGod.SeekEvent(this, another);
        }


        //void ReleaseSticking()
        //{
        //    attached = false;
        //    var joints = GetComponents<Joint>();
        //    for (int i = 0; i < joints.Length; i++)
        //    {
        //        Destroy(joints[i]);
        //    }
        //}

        Thing GetClosestThing(Thing center)
        {
            foreach (var thing in ThingGod.god.things)
            {
                if (thing == center) continue;
                if (Vector3.Distance(thing.transform.position, center.transform.position) < 10)
                {
                    return thing;
                }
            }

            return null;
        }

        IEnumerator IntervalBasedActions()
        {
            yield return new WaitForSeconds(5);
            while (true)
            {

                if (!dead && !attached)
                {
                    var closest = GetClosestThing(this);
                    if (closest != null) IntervalAction(GetClosestThing(this));
                    yield return new WaitForSeconds(5);
                }

            }
        }

        // Update is called once per frame


        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Thing>())
            {
                OnTouch(collision.gameObject.GetComponent<Thing>());
            }
        }


        public abstract void OnTouch(Thing other);
        public abstract void IntervalAction(Thing closestThing);
        public abstract void Init();







        // void OnTriggerEnter(Collider other)
        // {
        //     if (fRecord.ContainsKey(other.GetComponent<Thing>()))
        //     {

        //     }

        // }


        public static double Random(double start, double end)
        {
            return UnityEngine.Random.Range((float)start, (float)end);
        }
    }


}
public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


}
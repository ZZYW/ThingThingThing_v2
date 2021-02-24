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

        //user input
        //will be filled with json data
        public string[] intervalActions = new string[] { };
        public string[] touchActions = new string[] { };
        //        
        public int meshIndex;
        public Color color;
        Color accentColor;
        public Boid motor
        {
            get { return GetComponent<Boid>(); }
        }

        bool _attached;
        public bool attached
        {
            get { return _attached; }
            set
            {
                _attached = value;
                if (value)
                {
                    ThingGod.god.RemoveFromFlock(this);
                }
                else
                {
                    ThingGod.god.AddToFlock(this);
                    var joint = GetComponent<Joint>();
                    if (joint != null)
                    {
                        joint.connectedBody = null;
                        Destroy(joint);
                    }
                }
            }
        }
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


        public Dictionary<Thing, int> fRecord = new Dictionary<Thing, int>();

        public Bounds bounds
        {
            get { return GetComponent<Renderer>().bounds; }
        }


        //MONOBEHAVIOUR///////////////////////////////////////
        void Start()
        {
            Init();

            //test

            if (GetComponent<MeshFilter>().sharedMesh == null)
            {
                meshIndex = (int)(Random.value * ThingGod.god.availableModels.Count);

                var mesh = ThingGod.god.availableModels[meshIndex].GetComponentInChildren<MeshFilter>().sharedMesh;
                GetComponent<MeshFilter>().mesh = mesh;
                GetComponent<MeshRenderer>().material = ThingGod.god.thingMat;
                var myMat = GetComponent<MeshRenderer>().material;
                //calculate accent color
                float h, s, v;
                Color.RGBToHSV(color, out h, out s, out v);
                accentColor = Color.HSVToRGB((((h * 360) + 180f) % 360) / 360f, Mathf.Clamp01(s / 2f), Mathf.Clamp01(v / 2f));
                //set color
                myMat.SetColor("_ColorA1", color);
                myMat.SetColor("_ColorA2", accentColor);

                //collider
                var collider = GetComponent<MeshCollider>();
                collider.sharedMesh = mesh;
                collider.convex = true;
            }

            StartCoroutine(IntervalBasedActions());
        }

        void Update()
        {
            motor.inEffect = !attached;
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
            who.vertexPercentage = Mathf.Clamp(who.vertexPercentage + change, 0.1f,1f);
            Debug.LogFormat("{0} now will have {1}% vertices, changed {2}", who.name, who.vertexPercentage * 100, change);
            runtimeSimplifier.Simplify( who.vertexPercentage * 100 );           
        }

        public void Steal(Thing another)
        {
            if (another == null) return;
            Debug.Log(name + " steal " + another.name);

            ChangeVertexAmount(another, -0.1f);
            ChangeVertexAmount(this, 0.1f);

            if (ThingGod.StealEvent != null) ThingGod.StealEvent(this, another);
        }

        public void Gift(Thing another)
        {
            if (another == null) return;

            ChangeVertexAmount(another, +0.1f);
            ChangeVertexAmount(this, -0.1f);

            // another.Tuli += Tuli;
            // Tuli = 0;
            Debug.Log(name + " gift " + another.name);
            if (ThingGod.GiftingEvent != null) ThingGod.GiftingEvent(this, another);
            //Gifting, voluntarily transform one’s own Tulis to others;
        }

        public void Stick(Thing another)
        {
            if (another == null) return;
            Debug.Log(name + " stick " + another.name);
            //Follow, attach onto another thing for a limited period of time;
            transform.position = another.transform.position + another.transform.GetComponent<Collider>().bounds.extents.x * (transform.position - another.transform.position).normalized;
            motor.rb.velocity = Vector3.zero;
            //create a new joint to connect
            var myJoint = gameObject.AddComponent<CharacterJoint>();
            //connect to rb
            myJoint.connectedBody = another.motor.rb;
            if (ThingGod.StickEvent != null) ThingGod.StickEvent(this, another);
            //release        
            Invoke("ReleaseSticking", 10);
        }

        public void Clone(Thing another)
        {
            if (another == null) return;
            if (!CloneRegulator.instance.canClone) return;
            var cloneLayer = Regex.Matches(another.gameObject.name, "(Clone)").Count;
            if (cloneLayer > 5) return;
            Debug.Log(name + " mate " + another.name);
            ThingGod.god.CloneThing(another, transform.position, another.transform.localScale * 0.9f);
            if (ThingGod.CloneEvent != null) ThingGod.CloneEvent(this, another);
            //Mate, give birth to a baby that resembles other thing;
        }

        public void Erase(Thing another)
        {
            if (another == null) return;
            Debug.Log(name + " kill " + another.name);
            ThingGod.god.TryErase(another);
            if (ThingGod.EraseEvent != null) ThingGod.EraseEvent(this, another);
        }

        public void Group(Thing another)
        {
            motor.cohWeight *= 3f;
            motor.aliWeight *= 3f;
            motor.seekWeight /= 3f;
        }

        public void Hide(Thing another)
        {
            motor.cohWeight /= 3f;
            motor.aliWeight /= 3f;
            motor.seekWeight *= 3f;
        }

        public void Seek(Thing another)
        {
            if (another == null) return;
            //Aim, walk towards the direction of a shan, an er, a monolith, or the tuli mountain. 
            motor.target = another.transform;
            if (ThingGod.SeekEvent != null) ThingGod.SeekEvent(this, another);
            Invoke("ReleaseTarget", 15f);
        }

        public void DecreaseScore(int n, Thing who)
        {
            if (fRecord.ContainsKey(who))
            {
                fRecord[who] -= n;
            }
            else
            {
                fRecord[who] = -n;
            }
        }
        public void IncreaseScore(int n, Thing who)
        {
            if (fRecord.ContainsKey(who))
            {
                fRecord[who] += n;
            }
            else
            {
                fRecord[who] = n;
            }
        }

        void ReleaseSticking()
        {
            attached = false;
        }

        void ReleaseTarget()
        {
            motor.target = null;
        }

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
                    IntervalAction(GetClosestThing(this));
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
    }
}
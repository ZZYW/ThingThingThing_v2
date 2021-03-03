using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace ThingSpace
{
    public class ThingGod : MonoBehaviour
    {
        public static ThingGod god = null;
        public static System.Action<Thing> ThingBornEvent;
        public static System.Action<Thing, Thing> StealEvent, EraseEvent, CloneEvent, GiftingEvent, SeekEvent, StickEvent;
        [HideInInspector]
        public List<Thing> things = new List<Thing>();
        [HideInInspector]
        public List<Thing> flock = new List<Thing>();
        // public int initialTuli = 10;
        public Transform[] monoliths;
        public ParticleSystem ps;

        int burstParticleCount = 1;

        public List<GameObject> availableModels = new List<GameObject>();
        public string modelPath = "Resources/Curated_cleaned";
        public Material thingMat;
        public Material deadMat;
        public UnityEngine.UI.Text plateSample;

        public AudioClip[] audioClips;

        void Awake()
        {
            god = this;
            availableModels = Resources.LoadAll<GameObject>(modelPath).ToList();
            //Debug.Log(availableModels.Count);
            availableModels.RemoveAll(i => i == null);
        }

        void OnEnable()
        {
            ThingBornEvent += OnThingBorn;
            StealEvent += OnSteal;
            EraseEvent += OnErase;
            CloneEvent += OnClone;
            GiftingEvent += OnGifting;
            SeekEvent += OnSeek;
            StickEvent += OnStick;
        }
        void OnDisable()
        {
            ThingBornEvent -= OnThingBorn;
            StealEvent -= OnSteal;
            EraseEvent -= OnErase;
            CloneEvent -= OnClone;
            GiftingEvent -= OnGifting;
            SeekEvent -= OnSeek;
            StickEvent -= OnStick;
        }

        /////////////////////////////////////////////////////////////////////////////////
        //Callbacks

        void OnSteal(Thing actor, Thing receiver)
        {
            FireStealParticle(actor.transform.position);
            //receiver.DecreaseScore(1, actor);

        }

        void OnErase(Thing actor, Thing receiver)
        {
            things.Remove(receiver);
            flock.Remove(receiver);

            Material[] mats = new Material[receiver.GetComponent<MeshRenderer>().sharedMaterials.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = deadMat;
            }
            receiver.GetComponent<MeshRenderer>().sharedMaterials = mats;
            //for(int i=0;i < receiver.GetComponent<MeshRenderer>().sharedMaterials.Length; i++)
            //{
            //    receiver.GetComponent<MeshRenderer>().sharedMaterials[i] = deadMat;
            //}
            //receiver.GetComponent<MeshRenderer>().sharedMaterials = 
            FireEraseParticle(actor.transform.position);
            //receiver.DecreaseScore(2, actor);
        }

        void OnClone(Thing actor, Thing receiver)
        {
            CloneRegulator.instance.Cloned();
            //receiver.IncreaseScore(2, actor);
        }

        void OnGifting(Thing actor, Thing receiver)
        {
            FireGiftingParticle(actor.transform.position);
            //receiver.IncreaseScore(1, actor);
        }

        void OnSeek(Thing actor, Thing receiver)
        {

        }
        void OnStick(Thing actor, Thing receiver)
        {

        }

        void OnThingBorn(Thing t)
        {
            things.Add(t);
            flock.Add(t);
            FireBornParticle(t.transform.position);
            //Debug.Log("new thing " + t.name + " is born.");
            //create a overhead text for it
            t.plate = getNewPlateText();


        }

        UnityEngine.UI.Text getNewPlateText()
        {
            var newText = Instantiate(plateSample, plateSample.transform.parent);
            newText.gameObject.SetActive(true);
            return newText;
        }
        /////////////////////////////////////////////////////////////////////////////////
        //Particle stuff


        ParticleSystem.EmitParams EffectParticleParas(Color c)
        {
            var emitParas = new ParticleSystem.EmitParams();
            emitParas.startColor = c;
            return emitParas;
        }

        public void FireEraseParticle(Vector3 pos)
        {
            ps.transform.position = pos;
            ps.Emit(EffectParticleParas(Color.red), burstParticleCount);
        }

        public void FireStealParticle(Vector3 pos)
        {
            ps.transform.position = pos;
            ps.Emit(EffectParticleParas(Color.black), burstParticleCount);
        }

        public void FireGiftingParticle(Vector3 pos)
        {
            ps.transform.position = pos;
            ps.Emit(EffectParticleParas(Color.green), burstParticleCount);
        }

        public void FireBornParticle(Vector3 pos)
        {
            ps.transform.position = pos;
            ps.Emit(EffectParticleParas(Color.white), burstParticleCount);
        }

        /////////////////////////////////////////////////////////////////////////////////

        //public IEnumerator EraseThingEnum(Thing receiver)
        //{
        //    receiver.gameObject.SetActive(false);
        //    receiver.dead = true;
        //    yield return new WaitForSeconds(10);
        //    //respawn
        //    // receiver.Tuli = initialTuli;
        //    receiver.gameObject.SetActive(true);
        //    receiver.gameObject.transform.position = SpawnPos();
        //    receiver.gameObject.transform.Translate(Vector3.up);
        //    receiver.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero - receiver.transform.position;
        //    receiver.ResetFlags();
        //}

        //internal void TryErase(Thing another)
        //{
        //    StartCoroutine(EraseThingEnum(another));
        //}

        public void CloneThing(Thing template, Vector3 pos, Vector3 scale)
        {
            // Debug.Log("original has coh weight ->" + template.motor.cohWeight);
            var newOne = GameObject.Instantiate(template.gameObject, pos, Quaternion.identity);
            newOne.transform.localScale = scale;
            newOne.GetComponent<Thing>().ResetFlags();
            if (ThingBornEvent != null) ThingBornEvent(newOne.GetComponent<Thing>());
        }

        public void AddToFlock(Thing t)
        {
            if (!flock.Contains(t))
            {
                flock.Add(t);
            }
        }
        public void RemoveFromFlock(Thing t)
        {
            if (flock.Contains(t))
            {
                flock.Remove(t);
            }
        }

        Vector3 SpawnPos()
        {
            return new Vector3(Random.Range(-10, 10), 1.4f, Random.Range(-10, 10));
            //   return monoliths[Random.Range(0, monoliths.Length)].transform.position;
        }




        ////////////////////////////////////////////////////////////////////////////////////////////
        //MONOBEHAVIOUR

        void Start()
        {

        }


        void Update()
        {
            //reset when it is out of bound
            foreach (var thing in things)
            {
                if ((thing.transform.position - Vector3.zero).sqrMagnitude > 1000)
                {
                    //Debug.Log("reset " + thing.name + "'s position to 000");
                    var rb = thing.GetComponent<Rigidbody>();
                    var delta = rb.position - Vector3.zero;

                    rb.position = Vector3.zero;
                    rb.velocity = Random.insideUnitSphere * 5;
                }
            }
        }
    }
}
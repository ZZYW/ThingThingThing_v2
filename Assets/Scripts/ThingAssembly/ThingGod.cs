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
        public int initialTuli = 10;
        public Transform[] monoliths;
        public ParticleSystem ps;

        int burstParticleCount = 30;

        void Awake()
        {
            god = this;
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
            receiver.DecreaseScore(1, actor);

        }

        void OnErase(Thing actor, Thing receiver)
        {
            FireEraseParticle(actor.transform.position);
            receiver.DecreaseScore(2, actor);
        }

        void OnClone(Thing actor, Thing receiver)
        {
            CloneRegulator.instance.Cloned();
            receiver.IncreaseScore(2, actor);
        }

        void OnGifting(Thing actor, Thing receiver)
        {
            FireGiftingParticle(actor.transform.position);
            receiver.IncreaseScore(1, actor);
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
            Debug.Log("new thing " + t.name + " is born.");


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

        public IEnumerator EraseThingEnum(Thing receiver)
        {
            receiver.gameObject.SetActive(false);
            receiver.dead = true;
            yield return new WaitForSeconds(10);
            //respawn
            receiver.Tuli = initialTuli;
            receiver.gameObject.SetActive(true);
            receiver.gameObject.transform.position = SpawnPos();
            receiver.gameObject.transform.Translate(Vector3.up);
            receiver.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero - receiver.transform.position;
            receiver.ResetFlags();
        }

        internal void TryErase(Thing another)
        {
            StartCoroutine(EraseThingEnum(another));
        }

        public void CloneThing(Thing template, Vector3 pos, Vector3 scale)
        {
            Debug.Log("original has coh weight ->" + template.motor.cohWeight);
            var newOne = GameObject.Instantiate(template.gameObject, pos, Quaternion.identity);
            newOne.transform.localScale = scale;
            newOne.GetComponent<Thing>().ResetFlags();
            if (ThingBornEvent != null) ThingBornEvent(newOne.GetComponent<Thing>());

            Debug.Log("clone has coh weight ->" + newOne.GetComponent<Thing>().motor.cohWeight);
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

        public void CreateThings(ThingDataManager.ThingsSettings settings)
        {
            //TODO
            // var newOne = GameObject.Instantiate(agent, SpawnPos(), Quaternion.identity).AddComponent<Thing>();
            // //apply settings
            // newOne.intervalActions = new string[] { setting.intervalAction };
            // newOne.touchActions = new string[] { setting.touchAction };
            // newOne.motor.SetMass(setting.mass);
            // newOne.motor.SetMaxSpeed(setting.maxSpeed);
            // newOne.name = setting.name;
            //...


            // if (ThingBornEvent != null) ThingBornEvent(newOne);

        }



        ////////////////////////////////////////////////////////////////////////////////////////////
        //MONOBEHAVIOUR

        void Start()
        {
            foreach (var newOne in GameObject.FindObjectsOfType<Thing>())
            {
                if (ThingBornEvent != null) ThingBornEvent(newOne);
            }

        }


        void Update()
        {
            //reset when it is out of bound
            foreach (var thing in things)
            {
                if ((thing.transform.position - Vector3.zero).sqrMagnitude > 9999)
                {
                    var rb = thing.GetComponent<Rigidbody>();
                    var delta = rb.position - Vector3.zero;

                    rb.position = Vector3.zero;
                    rb.velocity = Random.insideUnitSphere * 5;
                }
            }
        }
    }
}
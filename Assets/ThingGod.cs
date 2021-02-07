using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThingGod : MonoBehaviour
{
    public static ThingGod god = null;
    public static System.Action<Thing> NewThingBorn;
    [HideInInspector]
    public List<Thing> things = new List<Thing>();
    [HideInInspector]
    public List<Thing> flock = new List<Thing>();
    public int initialTuli = 10;
    public int thingCount = 40;
    public Transform[] monoliths;
    public ParticleSystem ps;

    public GameObject agent;

    void Awake()
    {
        god = this;
    }

    void OnEnable()
    {
        NewThingBorn += NewThingBornCallback;
    }
    void OnDisable()
    {
        NewThingBorn -= NewThingBornCallback;
    }

    void NewThingBornCallback(Thing t)
    {
        things.Add(t);
        flock.Add(t);
    }

    ParticleSystem.EmitParams DeathParas(Vector3 pos)
    {
        var emitParas = new ParticleSystem.EmitParams();
        emitParas.position = pos;
        emitParas.startColor = Color.red;
        return emitParas;
    }

    public IEnumerator DestroyThing(Thing t)
    {
        t.gameObject.SetActive(false);
        ps.Emit(DeathParas(t.transform.position), 10);
        t.dead = true;
        yield return new WaitForSeconds(10);
        //respawn
        t.Tuli = initialTuli;
        t.gameObject.SetActive(true);
        t.gameObject.transform.position = SpawnPos();
        t.gameObject.transform.Translate(Vector3.up);
        t.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero - t.transform.position;
        t.dead = false;
    }

    public void CloneThing(Thing template, Vector3 pos, Vector3 scale)
    {
        var newOne = GameObject.Instantiate(template, pos, Quaternion.identity);
        newOne.transform.localScale = scale;
        if (NewThingBorn != null) NewThingBorn(newOne);
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

    void MakeThing()
    {

    }

    void InitAllThings()
    {
        for (int i = 0; i < thingCount; i++)
        {
            var newOne = GameObject.Instantiate(agent, SpawnPos(), Quaternion.identity).AddComponent<Thing>();
            if (NewThingBorn != null) NewThingBorn(newOne);
        }
    }






    // Start is called before the first frame update
    void Start()
    {
        foreach (var newOne in GameObject.FindObjectsOfType<Thing>())
        {
            if (NewThingBorn != null) NewThingBorn(newOne);
        }
        InitAllThings();
    }

    // Update is called once per frame
    void Update()
    {
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

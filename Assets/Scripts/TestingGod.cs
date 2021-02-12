using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class TestingGod : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Button]
    public void TestSteal()
    {
        var a = GetRandomThing();
        var b = GetClosestThing(a);
        if (b != null)
        {
            a.Steal(b);
        }
    }

    [Button]
    public void TestStick()
    {
        var a = GetRandomThing();
        var b = GetClosestThing(a);
        if (b != null)
        {
            a.Stick(b);
        }
    }

    [Button]
    public void TestErase()
    {
        var a = GetRandomThing();
        var b = GetClosestThing(a);
        if (b != null)
        {
            a.Erase(b);
        }
    }

    [Button]
    public void TestClone()
    {        
        var a = GetRandomThing();
        var b = GetClosestThing(a);
        if (b != null)
        {
            a.Clone(b);
        }
    }

    Thing GetRandomThing()
    {
        var list = ThingGod.god.things;
        return list[Random.Range(0, list.Count)];
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


}

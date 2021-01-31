using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingMaker : MonoBehaviour
{
    int count = 20;
    GameObject[] monoliths;
    List<Boid> allBoids;

    // Start is called before the first frame update
    void Start()
    {
        allBoids = new List<Boid>();

        monoliths = GameObject.FindGameObjectsWithTag("Monolith");
        for (int i = 0; i < count; i++)
        {
            var newBoid = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newBoid.transform.position = monoliths[Random.Range(0, monoliths.Length)].transform.position;
            newBoid.AddComponent<Thing>();

            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

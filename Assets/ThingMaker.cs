using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingMaker : MonoBehaviour
{
    int count = 20;
    GameObject[] monoliths;

    // Start is called before the first frame update
    void Start()
    {
        monoliths = GameObject.FindGameObjectsWithTag("Monolith");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

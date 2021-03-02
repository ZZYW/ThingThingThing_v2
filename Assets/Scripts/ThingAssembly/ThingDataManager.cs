using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingDataManager : MonoBehaviour
{
    public TextAsset test;

    void Awake()
    {





    }

    void Translate()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        // ThingsSettings settings = JsonUtility.FromJson<ThingsSettings>(test.text);
        // ThingGod.god.CreateThings(settings);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    public struct ThingsSettings
    {
        [System.Serializable]
        public struct Thing
        {
            public string name;
            public float mass;
            public float maxSpeed;
            public string touchAction;
            public string intervalAction;
        }
        public Thing[] things;
    }
}

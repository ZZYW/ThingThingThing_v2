using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThingSpace;


public class ThingMakerWebGL : MonoBehaviour
{
#if UNITY_WEBGL
    public string prefabFolderPath = "ThingPrefabs";
    public float spawnAreaRadius = 10;
    public float bornRate = 0.2f;
    public List<GameObject> allPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateNewThings());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CreateNewThings()
    {
        //var allPrefabs = Resources.LoadAll<GameObject>(prefabFolderPath);
        foreach(var prefab in allPrefabs)
        {
            var go = GameObject.Instantiate(prefab);
            //reset new thing's position
            go.transform.position = Random.insideUnitSphere * spawnAreaRadius;
            var newThing = go.GetComponent<Thing>();
            if (ThingGod.ThingBornEvent != null) ThingGod.ThingBornEvent(newThing);
            yield return new WaitForSeconds(bornRate);
        }
    }
#endif
}

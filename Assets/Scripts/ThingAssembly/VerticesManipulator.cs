using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VerticesManipulator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RemoveOneVertex(gameObject);
    }

    public static void RemoveOneVertex(GameObject target)
    {
        Mesh output = new Mesh();
        Mesh targetMesh = target.GetComponent<MeshFilter>().mesh;
        output.vertices = targetMesh.vertices;
        output.triangles = targetMesh.triangles;
        output.uv = targetMesh.uv;

        //do stuff
        var vList = output.vertices.ToList();
        var tList = output.triangles.ToList();
        var uvList = output.uv.ToList();
        tList.ForEach(i=>Debug.Log(i));
        int indexOfRemoval = (int)(Random.value * vList.Count);
        vList.RemoveAt(indexOfRemoval);
        tList.RemoveAt(indexOfRemoval);
        uvList.RemoveAt(indexOfRemoval);

        output.vertices = vList.ToArray();
        output.triangles = tList.ToArray();
        output.uv = uvList.ToArray();
        


        output.RecalculateBounds();
        output.RecalculateNormals();
        output.RecalculateTangents();

        target.GetComponent<MeshFilter>().mesh = output;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class FloatingOtter : Thing
    {
        //public GameObject candyPrefab;
        private GameObject currentCandy;
        private Vector3 candyPosOffset = new Vector3(0, 1, 1);
        Color myColor;

        public override void Init()
        {
            meshIndex = 28;
            vertexPercentage = 1;
            width = 3;
            height = 3;
            depth = 2;
            speed = 15;
            cohesion = 10;
            seperation = 6;
            spikyness = 0.6;

            var colorStr = "#b590f9";

            ColorUtility.TryParseHtmlString(colorStr, out myColor);
            red = 0.71;
            green = 0.565;
            blue = 0.976;

            StartCoroutine(WashFace());
        }

        public override void OnTouch(Thing other)
        {
            //if (currentCandy == null && !dead && !attached && !other.dead)
            //{
            //    // give candy to other thing
            //    var candyPos = other.transform.position + candyPosOffset;
            //    //currentCandy = Instantiate(candyPrefab, candyPos, Quaternion.identity);
            //    //currentCandy.transform.parent = other.transform;
            //}
        }

        public override void IntervalAction(Thing closestThing)
        {
            if (!dead && !attached)
            {
                if (currentCandy != null)
                {
                    // keep candy position updated
                    currentCandy.transform.position = transform.position + candyPosOffset;
                }
                if (Vector3.Distance(transform.position, closestThing.transform.position) < 10)
                {
                    // give candy to closest thing
                    var other = closestThing.GetComponent<FloatingOtter>();
                    if (other != null && currentCandy != null)
                    {
                        currentCandy.transform.parent = other.transform;
                        currentCandy = null;
                    }
                }
            }
        }

        IEnumerator WashFace()
        {
            while (true)
            {
                yield return new WaitForSeconds(10);
                if (!dead && !attached)
                {
                    // change color to white - face washing
                    foreach (var mat in GetComponent<MeshRenderer>().materials)
                    {
                        mat.SetColor("_ColorA1", Color.white);
           
                    }
                    yield return new WaitForSeconds(2);
                    // change color back to original
                    foreach (var mat in GetComponent<MeshRenderer>().materials)
                    {
                        mat.SetColor("_ColorA1", myColor);
  
                    }
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class odd_ie : Thing
    {
        public override void Init()
        {
            meshIndex = 11;
      
            speed = 15.0;
            cohesion = 9.0;
            seperation = 8.0;
            spikyness = 0.75;

            width = 4.0;
            height = 2.0;
            depth = 3.0;

            //color
            red = 0.965;
            green = 0.459;
            blue = 1.0;

            var matCount = GetComponent<MeshRenderer>().materials.Length;
            var mats = new Material[matCount];
            for (var i = 0; i < mats.Length; i++)
            {
                mats[i] = ThingGod.god.thingMat;
                
                mats[i].SetVector("_VertexDisScale", Vector3.one * (float)spikyness);
            }
            GetComponent<MeshRenderer>().materials = mats;


            StartCoroutine(LonelyTime());
        }

        public override void IntervalAction(Thing closestThing)
        {
            var shape = UnityEngine.Random.Range(0, 3);
            switch (shape)
            {
                case 0:
                    transform.localScale = new Vector3((float)width, (float)height, (float)depth);
                    break;
                case 1:
                    transform.localScale = new Vector3((float)width * 0.5f, (float)height * 4f, (float)depth * 3f);
                    break;
                case 2:
                    transform.localScale = new Vector3((float)width * 2f, (float)height, (float)depth * 2f);
                    break;
            }

            StartCoroutine(ChangeBack());
        }

        IEnumerator ChangeBack()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 5));
            transform.localScale = new Vector3((float)width, (float)height, (float)depth);
        }


        public override void OnTouch(Thing other)
        {
            StartCoroutine(ShrinkExpand());
        }

        IEnumerator ShrinkExpand()
        {
            Transform originalTransform = transform;
            attached = true;
            transform.localScale = new Vector3(3, 3, 3);
            yield return new WaitForSeconds(1.0f);
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            yield return new WaitForSeconds(2.0f);
            transform.localScale = new Vector3((float)width, (float)height, (float)depth);
            attached = false;
        }

        IEnumerator LonelyTime()
        {
            while (true)
            {
                if (!attached && !dead)
                {
                    yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));                    
                    yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));
                    motor.target = null;
                }
                else
                {
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }
}

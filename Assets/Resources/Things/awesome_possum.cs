using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class awesome_possum : Thing
    {
        public override void Init()
        {
            meshIndex = 23;
            width = 5;
            height = 1;
            depth = 5;
            speed = 20;
            cohesion = 7;
            seperation = 5;
            spikyness = 0.4;

      

        }

        public override void IntervalAction(Thing closestThing)
        {
            if (closestThing != null)
            {
                Seek(closestThing);                
                motor.cohWeight = 0;
                motor.aliWeight = 0;
                attached = false;
                Steal(closestThing);
                motor.speed = UnityEngine.Random.Range(18, 22);

                transform.localScale = new Vector3((float)width, (float)height, (float)depth) * UnityEngine.Random.Range(0.8f, 1.2f);
                transform.Rotate(Vector3.up, UnityEngine.Random.Range(-10, 10));
            }
            else
            {
                motor.cohWeight = (float)cohesion;
                motor.aliWeight = (float)seperation;
            }

            if (UnityEngine.Random.Range(0, 100) < 10 && !attached)
            {
                motor.speed = UnityEngine.Random.Range(8, 14);               
           
                transform.localScale = new Vector3((float)width, (float)height, (float)depth) * UnityEngine.Random.Range(0.5f, 1f);
                transform.Rotate(Vector3.up, UnityEngine.Random.Range(-20, 20));
            }
        }

        public override void OnTouch(Thing other)
        {
            if (attached) return;
         

        }
    }
}

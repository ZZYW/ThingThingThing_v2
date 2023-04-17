using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class BINK : Thing
    {
        public override void Init()
        {
            meshIndex = 21;
   
            speed = 10;
            cohesion = 5;
            seperation = 5;
            spikyness = 0.5;

            width = 3;
            height = 3;
            depth = 3;

            // convert color hex code to RGB values
            red = 0.502;
            green = 0.502;
            blue = 0.502;


            transform.Rotate(Vector3.forward, 45f); // make the BINK rotate at an angle

            StartCoroutine(Spiral()); // start spiral behavior
        }

        // spiral behavior
        IEnumerator Spiral()
        {
            float t = 0f;
            while (true)
            {
                t += Time.deltaTime;
                Vector3 newPos = new Vector3(Mathf.Sin(t / 2), Mathf.Cos(t), Mathf.Cos(t / 2));
                transform.position = newPos;
                yield return null;
            }
        }

        public override void IntervalAction(Thing closestThing)
        {
            // Sweet and silly behavior when meeting another Thing
            Group(closestThing);
            Hide(closestThing);
        }

        public override void OnTouch(Thing other)
        {
            // Sweet and silly behavior when touched by another Thing
            Gift(other);
        }
    }
}

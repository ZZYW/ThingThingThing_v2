using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class baba_is_a_sweetheart : Thing
    {
        public override void Init()
        {
            //Initialization code
            meshIndex = 21;
 
            width = 3;
            height = 3;
            depth = 3;
            speed = 10;
            cohesion = 5;
            seperation = 5;
            spikyness = 0.5f;
            red = 0.502;
            green = 0.502;
            blue = 0.502;

          
        }

        public override void IntervalAction(Thing closestThing)
        {
            // Jump up and down
            StartCoroutine(Jump(UnityEngine.Random.Range(0.1f, 0.5f)));
        }

        public override void OnTouch(Thing other)
        {
            // Check if the other Thing is gift-worthy
            if (other.vertexCount < vertexCount && UnityEngine.Random.Range(0, 1) < 0.3)
            {
                Gift(other);
            }
        }

        IEnumerator Jump(double duration)
        {
            // Jump up and down for a UnityEngine.Random.Range duration between 0.1 and 0.5 seconds
            float startY = transform.position.y;
            float endY = transform.position.y + UnityEngine.Random.Range(0.5f, 1);
            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                float currentValue = Mathf.Lerp(startY, endY, timer / (float)duration);
                transform.position = new Vector3(transform.position.x, currentValue, transform.position.z);
                yield return null;
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                float currentValue = Mathf.Lerp(endY, startY, timer / (float)duration);
                transform.position = new Vector3(transform.position.x, currentValue, transform.position.z);
                yield return null;
            }
        } 
    }
}

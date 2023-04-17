using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class Spongebob : Thing
    {
        private bool _isSleeping = false;
        private Thing _closestThing;

        public override void Init()
        {
            // Initialization code goes here.
            meshIndex = 21;

            speed = 10;
            cohesion = 5;
            seperation = 5;
            spikyness = 0.5f;
            width = 3;
            height = 3;
            depth = 3;
            ColorUtility.TryParseHtmlString("#808080", out Color color);
            red = 0.502;
            green = 0.502;
            blue = 0.502;

            // Set color.
            foreach (var mat in GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_ColorA1", color);
      
            }
            // Set smile and texture.
            foreach (var mat in GetComponent<MeshRenderer>().materials)
            {
                mat.SetFloat("_Smile", 1);
                mat.SetFloat("_Texture", 0.01f);
            }
        }

        public override void IntervalAction(Thing closestThing)
        {
            _closestThing = closestThing;
            _isSleeping = true;
            // Sleep
            motor.target = null;
            motor.speed = 0;

            StartCoroutine(WakeUp());
        }

        public override void OnTouch(Thing other)
        {
            // Say hi
            if (!_isSleeping && _closestThing == other)
            {
                Debug.Log("Hi!");
                // Set smile.
                foreach (var mat in GetComponent<MeshRenderer>().materials)
                {
                    mat.SetFloat("_Smile", 1);
                }
            }
        }

        IEnumerator WakeUp()
        {
            // Walk dog.
            yield return new WaitForSeconds(5);
            motor.speed = (float)speed;
            motor.target = _closestThing.transform;

            // Change texture.
            foreach (var mat in GetComponent<MeshRenderer>().materials)
            {
                mat.SetFloat("_Texture", 0.99f);
            }

            yield return new WaitForSeconds(10);
            // End walk and sleep again.
            motor.target = null;
            motor.speed = 0;

            // Set smile and texture.
            foreach (var mat in GetComponent<MeshRenderer>().materials)
            {
                mat.SetFloat("_Smile", 0.2f);
                mat.SetFloat("_Texture", 0.01f);
            }
            _isSleeping = false;
        }
    }
}

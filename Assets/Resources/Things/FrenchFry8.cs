using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class FrenchFry8 : Thing
    {
        public override void Init()
        {
            // assign values to all the existing variables. including red gree and blue.
            meshIndex = 33;
            speed = 20;
            cohesion = 1;
            seperation = 10;
            spikyness = 0.28;

            width = 5;
            height = 1;
            depth = 2;

            // set color
            Color color = ColorUtility.TryParseHtmlString("#fefcdd", out Color c) ? c : Color.white;
            red = 0.996;
            green = 0.988;
            blue = 0.867;

            transform.position = UnityEngine.Random.insideUnitSphere * 5;


        }

        public override void OnTouch(Thing other)
        {
            // slap behavior

        }

        public override void IntervalAction(Thing closestThing)
        {
            // eat roadkill behavior
            Debug.Log("FrenchFry8 is eating roadkill.");
        }

        // define additional behaviors below
        // Note: it is also possible to override other parent class functions as necessary
        // However, make sure to keep the original functionality of the Thing class intact.
    }
}

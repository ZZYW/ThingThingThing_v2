
using UnityEngine;
namespace ThingSpace
{
    public class beachbauble : Thing
    {
        public override void Init()
        {
            //you can assign those variables value here:
            meshIndex = 22;

            speed = 1;
            seperation = 5;
            cohesion = 5;

            width = 3;
            height = 2.5;
            depth = 1.5;

            red = 0.3;
            green = Random(0, 2);
            blue = 0.2;
            spikyness = 0;
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Clone(other);

            //we can also use if statement to add a condition before an action
            //in this case we throw out a random number between 0 and 10, if it is bigger than 3, this Thing will Seek
            if (Random(0, 10) > 3)
            {
                Seek(other);
            }

        }

        public override void OnTouch(Thing other)
        {
            //another if statment, now it will see if the other Thing has more than 100 vertex, if yes, then Steal, else, Gift
            if (other.vertexCount > 100)
            {
                Gift(other);
            }
        }

    }
}

            
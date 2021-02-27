
using UnityEngine;
namespace ThingSpace
{
    public class bbqq : Thing
    {
        public override void Init()
        {
            //you can assign those variables value here:
            meshIndex = 3;

            speed = 5;
            seperation = 0.1;
            cohesion = 5;

            width = 5;
            height = 1;
            depth = 1;

            red = 1;
            green = 0;
            blue = 0;
            spikyness = 0;
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Seek(other);

            //we can also use if statement to add a condition before an action
            //in this case we throw out a random number between 0 and 10, if it is bigger than 3, this Thing will Seek
            if (Random(0, 10) > 3)
            {
                Erase(other);
            }

        }

        public override void OnTouch(Thing other)
        {
            //another if statment, now it will see if the other Thing has more than 100 vertex, if yes, then Steal, else, Gift
            if (other.vertexCount > 100)
            {
                Steal(other);
            }
            else
            {
                Gift(other);
            }


        }

    }
}

            
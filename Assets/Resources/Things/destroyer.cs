
using UnityEngine;
namespace ThingSpace
{
    public class destroyer : Thing
    {
        public override void Init()
        {
            //you can assign those variables value here:
            meshIndex = 30;

            speed = 20;
            seperation = 0;
            cohesion = 10;

            width = 5;
            height = 5;
            depth = 5;

            red = 1;
            green = 0;
            blue = 0;
            spikyness = 1;
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            

            //we can also use if statement to add a condition before an action
            //in this case we throw out a random number between 0 and 10, if it is bigger than 3, this Thing will Seek
            Seek(other);
            

        }

        public override void OnTouch(Thing other)
        {
            Steal(other);
            Erase(other);


        }

    }
}

            
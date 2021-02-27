
using UnityEngine;
namespace ThingSpace
{
    public class broccolo : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 16;
            seperation = 10;
            cohesion = 10;

            //shape and size            
            meshIndex = 37; //which geometry you want to use, range 0 - 43
            width = 5;
            height = 5;
            depth = 5;
            spikyness = 1;
            
            //color
            red = 0.47;
            green = 0.62;
            blue = 0.05;            
            
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
                Steal(other);
            }
            else
            {
                Gift(other);
            }


        }

    }
}

            
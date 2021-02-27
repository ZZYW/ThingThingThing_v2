
using UnityEngine;
namespace ThingSpace
{
    public class sheherhers : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 15;
            seperation = 9;
            cohesion = 2;

            //shape and size            
            meshIndex = 29; //which geometry you want to use, range 0 - 43
            width = 3;
            height = 3;
            depth = 3;
            spikyness = 0.5;
            
            //color
            red = 0;
            green = 1.0;
            blue = 1.0;            
            
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Seek(other);

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
                Erase(other);
            }
            else
            {
                Erase(other);
            }


        }

    }
}

            
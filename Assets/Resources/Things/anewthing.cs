
using UnityEngine;
namespace ThingSpace
{
    public class anewthing : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 3;
            seperation = 1.3;
            cohesion = 5;

            //shape and size            
            meshIndex = 28; //which geometry you want to use, range 0 - 43
            width = 0.8;
            height = 1;
            depth = 1.3;
            spikyness = 1;
            
            //color
            red = 0.3;
            green = Random(0, 2);
            blue = 0.8;            
            
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Steal(other);

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

            
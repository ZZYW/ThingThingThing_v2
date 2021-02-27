
using UnityEngine;
namespace ThingSpace
{
    public class Panda : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 2;
            seperation = 5;
            cohesion = 3;

            //shape and size            
            meshIndex = 28; //which geometry you want to use, range 0 - 43
            width = 5;
            height = 2;
            depth = 1;
            spikyness = 0;
            
            //color
            red = 0.8;
            green = Random(0, 2);
            blue = 0.5;            
            
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Gift(other);

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
                Gift(other);
            }


        }

    }
}

            
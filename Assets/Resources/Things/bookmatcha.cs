
using UnityEngine;
namespace ThingSpace
{
    public class bookmatcha : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 3;
            seperation = 2;
            cohesion = 7;

            //shape and size            
            meshIndex = 40; //which geometry you want to use, range 0 - 43
            width = 1;
            height = 3;
            depth = 1;
            spikyness = 1;
            
            //color
            red = 0.7;
            green = 0;
            blue = 0.5;            
            
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Seek(other);

            //we can also use if statement to add a condition before an action
            //in this case we throw out a random number between 0 and 10, if it is bigger than 3, this Thing will Seek
            if (Random(0, 10) > 3)
            {
                Clone(other);
            }

        }

        public override void OnTouch(Thing other)
        {
            //another if statment, now it will see if the other Thing has more than 100 vertex, if yes, then Steal, else, Gift
            if (other.vertexCount > 100)
            {
                Gift(other);
            }
            else
            {
                Steal(other);
            }


        }

    }
}

            
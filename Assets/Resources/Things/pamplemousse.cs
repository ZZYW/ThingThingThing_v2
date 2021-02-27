
using UnityEngine;
namespace ThingSpace
{
    public class pamplemousse : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 10;
            seperation = 5;
            cohesion = 7;

            //shape and size            
            meshIndex = 28; //which geometry you want to use, range 0 - 43
            width = 2;
            height = 5;
            depth = 2;
            spikyness = 0;
            
            //color
            red = Random(0.5,1);
            green = Random(0.5, 1);
            blue = 0.5;            
            
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Steal(other);

            //we can also use if statement to add a condition before an action
            //in this case we throw out a random number between 0 and 10, if it is bigger than 3, this Thing will Seek
            if (Random(0, 10) > 5)
            {
                Clone(other);
            }

        }

        public override void OnTouch(Thing other)
        {
            //another if statment, now it will see if the other Thing has more than 100 vertex, if yes, then Steal, else, Gift
            if (other.vertexCount > 50)
            {
                Steal(other);
            }
            else
            {
                Seek(other);
            }


        }

    }
}

            
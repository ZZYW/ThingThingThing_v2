
using UnityEngine;
namespace ThingSpace
{
    public class spikeRike : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 13;
            seperation = 2;
            cohesion = 4;

            //shape and size            
            meshIndex = 32; //which geometry you want to use, range 0 - 43
            width = 1;
            height = 1;
            depth = 1;
            spikyness = 1;
            
            //color purple!
            red = 1;
            green = 0;
            blue = 1;            
            
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
                Clone(other);
            }

        }

        public override void OnTouch(Thing other)
        {
            //another if statment, now it will see if the other Thing has more than 100 vertex, if yes, then Steal, else, Gift
            if (other.vertexCount > 100)
            {
                Steal(other);
                Clone(other);
            }
            else
            {
                Gift(other);
            }
            
            if (Random(0, 10) > 3)
            {
                Erase(other);
            }
            


        }

    }
}

            
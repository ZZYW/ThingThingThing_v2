
using UnityEngine;
namespace ThingSpace
{
    public class foobar : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 1;
            seperation = 5;
            cohesion = 10;

            //shape and size            
            meshIndex = 14; //which geometry you want to use, range 0 - 43
            width = 5;
            height = 5;
            depth = 3;
            spikyness = 0;
            
            //color
            red = 0.0;
            green = 0.6;
            blue = 0.5;            
            
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Clone(other);

            //we can also use if statement to add a condition before an action
            //in this case we throw out a random number between 0 and 10, if it is bigger than 3, this Thing will Seek

            Seek(other);


        }

        public override void OnTouch(Thing other)
        {
            //another if statment, now it will see if the other Thing has more than 100 vertex, if yes, then Steal, else, Gift
            if (other.vertexCount > 1)
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

            
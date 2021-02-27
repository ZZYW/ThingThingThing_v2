
using UnityEngine;
namespace ThingSpace
{
    public class bug : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 3;
            seperation = 4.2;
            cohesion = 7.7;

            //shape and size            
            meshIndex = 42; //which geometry you want to use, range 0 - 43
            width = 4.2;
            height = 2;
            depth = 5;
            spikyness = .99;
            
            //color
            red = .7;
            green = .9;
            blue = Random(0, 2);            
            
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
            if (other.vertexCount > 25)
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

            
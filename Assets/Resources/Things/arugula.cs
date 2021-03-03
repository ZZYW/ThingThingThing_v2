
using UnityEngine;
namespace ThingSpace
{
    public class arugula : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 0.2;
            seperation = 2;
            cohesion = 8;

            //shape and size            
            meshIndex = 27; //which geometry you want to use, range 0 - 43
            width = 4;
            height = 4;
            depth = 4;
            spikyness = Random(0,1);
            
            //color
            red = Random(0,1);
            green = Random(0,1);
            blue = Random(0,1);            
            
        }

        public override void IntervalAction(Thing other)
        {    
            if(other.vertexCount > 10) {
                Seek(other);
            }
        }

        public override void OnTouch(Thing other)
        {
            //another if statment, now it will see if the other Thing has more than 100 vertex, if yes, then Steal, else, Gift
            if (Random(0,10) == 3)
            {
                Clone(other);
            } 
            else {
                other.meshIndex = 27; 
            }

        }

    }
}

            
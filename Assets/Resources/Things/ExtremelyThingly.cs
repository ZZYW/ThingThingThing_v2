
using UnityEngine;
namespace ThingSpace
{
    public class ExtremelyThingly : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 18;
            seperation = 0.1;
            cohesion = 8;

            //shape and size            
            meshIndex = 19; //which geometry you want to use, range 0 - 43
            width = 5;
            height = 5;
            depth = 1;
            spikyness = 0.3;
            
            //color
            red = Random(0,1);
            green = Random(0, 1);
            blue = Random(0,1);            
            
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Seek(other);
        }

        public override void OnTouch(Thing other)
        {
         Steal(other);
         
        }

    }
}

            
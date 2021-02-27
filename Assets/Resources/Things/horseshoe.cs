
using UnityEngine;
namespace ThingSpace
{
    public class horseshoe : Thing
    {
        public override void Init()
        {

            //movement related
            speed = 1.1;
            seperation = 5;
            cohesion = 5;

            //shape and size            
            meshIndex = 23; //which geometry you want to use, range 0 - 43
            width = 1;
            height = 5;
            depth = 1;
            spikyness = 1;
            
            //color
            red = 1;
            green = 0.9;
            blue = .2;            
            
        }

        public override void IntervalAction(Thing other)
        {
            //call a function like this
            Clone(other);

        }

        public override void OnTouch(Thing other)
        {
            Clone(other);


        }

    }
}

            
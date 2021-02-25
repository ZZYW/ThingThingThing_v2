using UnityEngine;
namespace ThingSpace
{
    public class Huge : Thing
    {
        public override void Init()
        {
            speed = 5;
            seperation = 4;

            width = 3;
            height = 1;
            depth = 0.4;

            red = 0.3;
            green = 1;
            blue = 0.2;
        }

        public override void IntervalAction(Thing other)
        {
          
        }

        public override void OnTouch(Thing other)
        {
            Clone(other);
        }

    }
}
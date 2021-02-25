using UnityEngine;
namespace ThingSpace
{
    public class Yelo : Thing
    {
        public override void Init()
        {
            meshIndex = -1;
            speed = 3;
            cohesion = 2;
            red = 0;
            green = 0;
            blue = 1;

      

        }

        public override void IntervalAction(Thing other)
        {
            Clone(other);
        }

        public override void OnTouch(Thing other)
        {

            if (other.vertexCount > 1)
            {
                Seek(other);
            }
            // Erase(other);


            // if (other.Tuli <= 0)
            // {

            // }
        }

    }
}
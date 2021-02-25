using UnityEngine;
namespace ThingSpace
{
    public class Yelo : Thing
    {
        public override void Init()
        {
            speed = 3;
            cohesion = 2;
      

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
using UnityEngine;

namespace ThingSpace
{
    public class little_blorbo_man : Thing
    {

        public override void Init()
        {
            // Set mesh Index to 28, Vertex Count to 50, Speed to 12, Separation to 7 and Cohesion to 6.
            meshIndex = 28;

            speed = 12;
            seperation = 7;
            cohesion = 6;
            spikyness = 0.4;

            // Set width to 4, height to 5, and depth to 2.
            width = 4;
            height = 5;
            depth = 2;

            //Set color to #a870a9 which corresponds to RGB color (0.659, 0.439, 0.663).

            red = 0.659;
            green = 0.439;
            blue = 0.663;
        }

        public override void OnTouch(Thing other)
        {
            // Steal items from other Things and avoid them.
            if (other != null && !other.dead)
            {
                Steal(other);
                Seek(other);
            }
        }

        public override void IntervalAction(Thing closestThing)
        {
            // Fly around in the clouds

        }
    }
}

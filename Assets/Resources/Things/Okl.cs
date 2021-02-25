using UnityEngine;
namespace ThingSpace
{
    public class Okl : Thing
    {
        public override void Init()
        {
            speed = -1;
            //red = Random.value;
            //green = Random.value;
            //blue = Random.value;
        }

        public override void IntervalAction(Thing other)
        {
            // Clone(closestThing);
            // Erase(closestThing);
            // Steal(closestThing);
            // Gift(closestThing);            
            Gift(other);
        }

        public override void OnTouch(Thing other)
        {

            
                Steal(other);

    
        }

    }
}
using UnityEngine;
namespace ThingSpace
{
    public class Yelo : Thing
    {
        public override void Init()
        {
            motor.SetMass(1);
            motor.SetMaxSpeed(3);
            motor.aliWeight = 1;
        }

        public override void IntervalAction(Thing closestThing)
        {
            Clone(closestThing);
        }

        public override void OnTouch(Thing other)
        {


            // Erase(other);


            if (other.Tuli <= 0)
            {

            }
        }

    }
}
using UnityEngine;
namespace ThingSpace
{
    public class Okl : Thing
    {
        public override void Init()
        {
            motor.SetMass(1);
            motor.SetMaxSpeed(1);
            motor.aliWeight = 1;
            color = new Color(0,0.2f,0.4f);
        }

        public override void IntervalAction(Thing other)
        {
            // Clone(closestThing);
            // Erase(closestThing);
            // Steal(closestThing);
            // Gift(closestThing);            
              Steal(other);
        }

        public override void OnTouch(Thing other)
        {

            
                Steal(other);

    
        }

    }
}
using UnityEngine;
namespace ThingSpace
{
    public class Huge : Thing
    {
        public override void Init()
        {
            motor.SetMass(10);
            motor.SetMaxSpeed(5);
            motor.sepWeight = 4;
            color = new Color(1,0,0);            
        }

        public override void IntervalAction(Thing other)
        {
            Seek(other);
        }

        public override void OnTouch(Thing other)
        {
            Erase(other);
        }

    }
}
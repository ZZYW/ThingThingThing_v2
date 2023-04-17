using UnityEngine;

namespace ThingSpace
{
    public class Yoyaga : Thing
    {
        public override void Init()
        {
            // Initialization code goes here.
            meshIndex = 40;
   
            speed = 2;
            cohesion = 8;
            seperation = 2;
            spikyness = 0.8;
            width = 4;
            height = 3;
            depth = 5;
            var color = ColorUtility.TryParseHtmlString("#d9f3f7", out Color newColor) ? newColor : Color.white;
            red = 0.851;
            green = 0.953;
            blue = 0.969;

            //audioSource.volume = 0.5f;

            // Implement other behaviors as defined by the user.
        }
        
        public override void IntervalAction(Thing closestThing)
        {
            if (closestThing == null) return;
            
            // When meeting other Things, this Thing pulls out its electric bass guitar and starts jamming.
            audioSource.Play();
            Seek(closestThing);
        }

        public override void OnTouch(Thing other)
        {
            // no-op
            // This Thing loves metal music and has a fiery personality.
        }

        // Implement other behaviors needed.
    }
}

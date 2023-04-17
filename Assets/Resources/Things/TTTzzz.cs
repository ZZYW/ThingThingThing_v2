using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class TTTzzz : Thing
    {
        Material[] mats;

        public override void Init()
        {
            meshIndex = 26;
            speed = 10;
            cohesion = 5;
            seperation = 5;
            spikyness = 0.5;
            width = 3;
            height = 3;
            depth = 3;

            red = 1f;
            green = 0.851f;
            blue = 0.678f;
          
        }

        public override void OnTouch(Thing other)
        {
            // Smile and give other a hug
   
        }

        public override void IntervalAction(Thing closestThing)
        {
           


            // Shining and have different spots
            foreach (var mat in mats)
            {
                float blinkChance = 0.25f;
                if (UnityEngine.Random.Range(0f, 1f) < blinkChance)
                    mat.SetFloat("_BlinkScale", UnityEngine.Random.Range(0.5f, 2f));
                if (UnityEngine.Random.Range(0f, 1f) < blinkChance)
                    mat.SetFloat("_BlinkShift", UnityEngine.Random.Range(0f, 1f));
                if (UnityEngine.Random.Range(0f, 1f) < blinkChance)
                    mat.SetFloat("_BlinkSpeed", UnityEngine.Random.Range(0.4f, 4f));
                if (UnityEngine.Random.Range(0f, 1f) < blinkChance)
                    mat.SetTextureOffset("_BlinkTex", new Vector2(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)));
            }
        }
    }
}

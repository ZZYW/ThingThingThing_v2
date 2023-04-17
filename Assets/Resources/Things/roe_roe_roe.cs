using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class roe_roe_roe : Thing
    {
        public override void Init()
        {
            meshIndex = 21;
            speed = 10;
            cohesion = 5;
            seperation = 8;
            spikyness = .49;

            width = 4d;
            height = 4d;
            depth = 2d;

            ColorUtility.TryParseHtmlString("#6b1a1a", out Color colorCustom);
            red = 0.420;
            green = 0.102;
            blue = 0.102;

            // set color
            Color color = new Color((float)red, (float)green, (float)blue);
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            Color accentColor = Color.HSVToRGB((((h * 360) + 180f) % 360) / 360f, Mathf.Clamp01(s), Mathf.Clamp01(v));
            //

            foreach (var mat in GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_ColorA1", colorCustom);
                mat.SetColor("_ColorA2", accentColor);
            }
        }

        public override void IntervalAction(Thing closestThing)
        {
            width += 0.1d;
            height += 0.1d;
            depth += 0.1d;
        }

        public override void OnTouch(Thing other)
        {
            motor.speed *= -1;
        }
    }
}

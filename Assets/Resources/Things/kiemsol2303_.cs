using UnityEngine;
using System.Collections;

namespace ThingSpace
{
    public class kiemsol2303_ : Thing
    {
        public override void Init()
        {
            meshIndex = 28;

            speed = 6;
            cohesion = 6;
            seperation = 2;
            spikyness = 0.03;
            width = 3;
            height = 5;
            depth = 5;
            // parse color string to hex
            string hexColor = "#4a2e00";


            // assign color variables
            red = 0.290d;
            green = 0.180d;
            blue = 0.0d;
        }

        public override void IntervalAction(Thing closestThing)
        {
            // Run away when other things are closer than 10 units
            if (Vector3.Distance(transform.position, closestThing.transform.position) < 10)
            {
                motor.target = closestThing.transform;
            }
        }

        public override void OnTouch(Thing other)
        {
            // If it comes close to contact it becomes very furious and aggressive and attacks.
            if (Vector3.Distance(transform.position, other.transform.position) <= 1)
            {
                var audioSource = other.GetComponent<AudioSource>();

                audioSource.Play();
                other.dead = true;
                deathRattle();
            }
        }

        private void deathRattle()
        {
            // Add explosion particle effect for 3 seconds



            StartCoroutine(DeathRattleCoroutine());
        }
        IEnumerator DeathRattleCoroutine()
        {
            yield return new WaitForSeconds(3);

        }

    }
}

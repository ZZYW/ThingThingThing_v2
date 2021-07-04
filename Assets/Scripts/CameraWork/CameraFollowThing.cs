using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ThingSpace
{
    public class CameraFollowThing : MonoBehaviour
    {

        public Transform followTarget;
        public float followDistance;
        public float elevation;
        public UnityEngine.UI.Text HUDText;
        public bool rollCalling = true;
        public float interval = 5f;


        //smooth
        Vector3 posVel;


        void FixedUpdate()
        {
            if (followTarget == null) return;

            Vector3 posTarget = followTarget.position - (followDistance * followTarget.forward) + new Vector3(0, elevation, 0);
            transform.position = Vector3.SmoothDamp(transform.position, posTarget, ref posVel, 1);


            // transform.LookAt(followTarget);
            var lookRot = Quaternion.LookRotation( followTarget.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation.normalized, lookRot, 0.1f);

        }

        IEnumerator ChangeFollowTarget()
        {
            while (rollCalling)
            {
                followTarget = RandomThing().transform;
                yield return new WaitForSeconds(interval);
            }

        }


        Thing RandomThing()
        {
            int len = ThingGod.god.things.Count;
            return ThingGod.god.things[Random.Range(0, len)];
        }

    }
}
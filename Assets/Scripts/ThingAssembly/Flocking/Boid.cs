using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace ThingSpace
{
    [RequireComponent(typeof(Rigidbody))]
    public class Boid : MonoBehaviour
    {
        public float sepWeight = 1;
        public float aliWeight = 1;
        public float cohWeight = 1;
        public float seekWeight = 10;
        public float repellerWeight = 1;
        public Rigidbody rb
        {
            get
            {
                return gameObject.GetComponent<Rigidbody>();
            }
        }

        public float maxForceSq = 100;    // Maximum steering force
        public float speed = 10;   // Maximum speed
        public bool inEffect = true;

        Vector3 finalForce = Vector3.zero;
        public Transform target;

        bool initialized;

        private void Awake()
        {
            if (!initialized) InitParas();
        }
        
        public void InitParas()
        {
            //default               
            //sepWeight += Random.Range(-0.2f, 0.2f);
            //aliWeight += Random.Range(-0.2f, 0.2f);
            //cohWeight += Random.Range(-0.2f, 0.2f);
            //seekWeight += Random.Range(-0.2f, 0.2f);
            //speed += Random.Range(-4f, 4f);
            maxForceSq += Random.Range(-20f, 20f);
            rb.velocity = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            rb.useGravity = true;
            initialized = true;
        }

        // void OnDrawGizmos()
        // {
        //     var nextPos = (transform.position + finalForce * 100);
        //     Gizmos.DrawWireSphere(nextPos, 1);
        //     Gizmos.DrawLine(transform.position, nextPos);
        // }


        // We accumulate a new acceleration each time based on three rules
        void Flock(List<Boid> boids)
        {                        
            // Debug.Log(boids.Count);
            Vector3 sep = Separate(boids);   // Separation
            Vector3 ali = Align(boids);      // Alignment
            Vector3 coh = Cohesion(boids);   // Cohesion
            Vector3 seek = target ? Seek(target.position) : Vector3.zero;

            // Arbitrarily weight these forces
            sep *= sepWeight;
            ali *= aliWeight;
            coh *= cohWeight;
            seek *= seekWeight;

            // Add the force vectors to acceleration
            finalForce += sep;
            finalForce += ali;
            finalForce += coh;
            finalForce += seek;
        }

        private void Update()
        {
            if (!inEffect) return;


            for (int i = 0; i < ThingGod.god.flock.Count; i++)
            {
                if (ThingGod.god.flock[i] == null) ThingGod.god.flock.RemoveAt(i);
            }


            //reset finalforce
            finalForce = Vector3.zero;
            var boidList = ThingGod.god.flock.Select(i => i.GetComponent<Boid>()).ToList();
            
            Flock(boidList);

            if (target != null)
            {
                if (target.GetComponent<Thing>() == null) target = null;
            }
        }

        // // Method to update transform.position
        void FixedUpdate()
        {
            if (!inEffect) return;
            rb.AddForce(finalForce * Time.fixedDeltaTime, ForceMode.VelocityChange);

            // float rotationSmoothSpeed = 10;
            // if (rb.velocity.normalized != Vector3.zero)
            // {
            //     Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
            //     Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSmoothSpeed);
            //     rb.rotation = newRotation;
            // }
        }


        // A method that calculates and applies a steering force towards a target
        // STEER = DESIRED MINUS VELOCITY
        Vector3 Seek(Vector3 target)
        {
            Vector3 desired = target - transform.position;  // A vector pointing from the transform.position to the target
                                                            // Normalize desired and scale to maximum speed
            desired.Normalize();
            desired *= speed;
            // Steering = Desired minus Velocity
            Vector3 steer = desired - rb.velocity;
            while (steer.sqrMagnitude > maxForceSq)
            {
                steer *= 0.95f;
            }
            return steer;
        }

        // Separation
        // Method checks for nearby boids and steers away
        Vector3 Separate(List<Boid> boids)
        {
            float desiredseparation = 25.0f;
            Vector3 steer = new Vector3(0, 0, 0);
            int count = 0;
            // For every boid in the system, check if it's too close
            foreach (Boid other in boids)
            {
                float d = Vector3.Distance(transform.position, other.transform.position);
                // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself)
                if ((d > 0) && (d < desiredseparation))
                {
                    // Calculate vector pointing away from neighbor
                    Vector3 diff = transform.position - other.transform.position;
                    diff.Normalize();
                    diff /= d;        // Weight by distance
                    steer += diff;
                    count++;            // Keep track of how many
                }
            }

            // Average -- divide by how many
            if (count > 0)
            {
                steer /= (float)count;
            }

            // As long as the vector is greater than 0
            if (steer.sqrMagnitude > 0)
            {
                // Implement Reynolds: Steering = Desired - Velocity
                steer.Normalize();
                steer *= speed;
                steer -= rb.velocity;
                while (steer.sqrMagnitude > maxForceSq)
                {
                    steer *= 0.95f;
                }
            }
            return steer;
        }

        // Alignment
        // For every nearby boid in the system, calculate the average velocity
        Vector3 Align(List<Boid> boids)
        {
            float neighbordist = 50;
            Vector3 sum = new Vector3(0, 0);
            int count = 0;

            foreach (Boid other in boids)
            {
                float d = Vector3.Distance(transform.position, other.transform.position);
                if ((d > 0) && (d < neighbordist))
                {
                    sum += other.rb.velocity;
                    count++;
                }
            }

            if (count > 0)
            {
                sum /= (float)count;
                sum.Normalize();
                sum *= speed;
                Vector3 steer = sum - rb.velocity;
                while (steer.sqrMagnitude > maxForceSq)
                {
                    steer *= 0.95f;
                }
                return steer;
            }
            else
            {
                return new Vector3(0, 0);
            }
        }

        // Cohesion
        // For the average transform.position (i.e. center) of all nearby boids, calculate steering vector towards that transform.position
        Vector3 Cohesion(List<Boid> boids)
        {
            float neighbordist = 50;
            Vector3 sum = new Vector3(0, 0);   // Start with empty vector to accumulate all transform.positions
            int count = 0;
            foreach (Boid other in boids)
            {

                float d = Vector3.Distance(transform.position, other.transform.position);
                if ((d > 0) && (d < neighbordist))
                {
                    sum += other.transform.position; // Add transform.position
                    count++;
                }
            }
            if (count > 0)
            {
                sum /= count;
                return Seek(sum);  // Steer towards the transform.position
            }
            else
            {
                return new Vector3(0, 0);
            }
        }
    }
}
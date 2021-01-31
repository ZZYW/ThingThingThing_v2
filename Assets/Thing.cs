using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    public float Tuli { get; private set; }

    void Move()
    {

    }

    public void Steal(Thing another)
    {
        //Steal, getting other Thing’s Tuili forcefully;
    }

    public void Gift(Thing another)
    {
        //Gifting, voluntarily transform one’s own Tulis to others;
    }

    public void Adopt(Thing another)
    {
        //Brainwashed, adopting other thing’s parameters completely;
    }

    public void StichTo(Thing another)
    {
        //Follow, attach onto another thing for a limited period of time;
    }

    public void Mate(Thing another)
    {
        //Mate, give birth to a baby that resembles other thing;
    }

    public void Kill(Thing another)
    {
        //Kill, destroy other thing and force it to be reborn at the monolith;
    }

    public void Pled()
    {
        //Plead, making a unique noise, which will increase kind acts from shans and malicious acts from ers. 

    }

    public void TurnAway(Thing another)
    {
        //Steer away, turn away from other thing;
    }

    public void Aim(Thing another)
    {
        //Aim, walk towards the direction of a shan, an er, a monolith, or the tuli mountain. 
    }


    Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

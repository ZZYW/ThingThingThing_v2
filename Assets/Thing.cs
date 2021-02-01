using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{

    public float Tuli;
    bool _attached;
    public bool attached
    {
        get { return _attached; }
        set
        {
            _attached = value;
            if (value)
            {
                ThingGod.god.RemoveFromFlock(this);
            }
            else
            {
                ThingGod.god.AddToFlock(this);
            }
        }

    }
    bool _dead;
    public bool dead
    {
        get { return _dead; }
        set
        {
            _dead = value;
            if (value)
            {
                ThingGod.god.RemoveFromFlock(this);
            }
            else
            {
                ThingGod.god.AddToFlock(this);
            }
        }
    }

    public Bounds bounds
    {
        get { return GetComponent<Renderer>().bounds; }
    }

    void Move()
    {
    }

    public void Steal(Thing another)
    {
        Debug.Log(name + " steal " + another.name);
        Tuli += another.Tuli;
        another.Tuli = 0;
    }

    public void Gift(Thing another)
    {
        another.Tuli += Tuli;
        Tuli = 0;
        Debug.Log(name + " gift " + another.name);
        //Gifting, voluntarily transform one’s own Tulis to others;
    }

    public void Adopt(Thing another)
    {
        //Brainwashed, adopting other thing’s parameters completely;
        Debug.Log(name + " adopt " + another.name);
    }

    //============================================================
    public void Stick(Thing another)
    {
        Debug.Log(name + " stick " + another.name);
        //Follow, attach onto another thing for a limited period of time;
        StartCoroutine(StickOnto(another.myBoid.rb));
    }

    IEnumerator StickOnto(Rigidbody targetRB)
    {
        attached = true;
        while (Vector3.Distance(targetRB.position, transform.position) > bounds.size.x)
        {
            var pos = transform.position;
            var delta = targetRB.position - pos;
            myBoid.rb.position = delta.normalized * 10f * Time.fixedDeltaTime + pos;
            yield return new WaitForFixedUpdate();
        }
        var myJoint = gameObject.AddComponent<CharacterJoint>();
        Debug.Log("Sticking Starts.");
        myBoid.rb.velocity = Vector3.zero;
        myJoint.connectedBody = targetRB;
        yield return new WaitForSeconds(10);
        myJoint.connectedBody = null;
        Destroy(myJoint);
        Debug.Log("Sticking Ends.");
        attached = false;
    }
    //============================================================

    public void Mate(Thing another)
    {
        Debug.Log(name + " mate " + another.name);
        ThingGod.god.CloneThing(another, transform.position, another.transform.localScale);
        //Mate, give birth to a baby that resembles other thing;
    }

    public void Kill(Thing another)
    {
        Debug.Log(name + " kill " + another.name);
        StartCoroutine(ThingGod.god.DestroyThing(another));

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



    Boid myBoid;

    // Start is called before the first frame update
    void Start()
    {

        myBoid = gameObject.AddComponent<Boid>();
    }

    // Update is called once per frame
    void Update()
    {
        myBoid.inEffect = !attached;
    }
}

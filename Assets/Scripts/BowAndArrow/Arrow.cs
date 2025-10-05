using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool canBeDestroyed = false;
    private Rigidbody rb;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude > 0.01f) // make sure it's actually moving
        {
            Quaternion look = Quaternion.LookRotation(rb.velocity.normalized);
            rb.MoveRotation(look);
        }
    }

    void Update()
    {
        if (canBeDestroyed)
        {
            StartCoroutine(SelfDestruct());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with = " + collision.gameObject.name);

        //make the arrow stop moving completely if it hits the ground
        if (collision.gameObject.tag == "Ground") //will be destroyed after the self destruct coroutine ends
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        //check if arrow hit another arrow
        if (collision.gameObject.tag == "Arrow")
        {
            Debug.Log("collided with arrow");
            // Physics.IgnoreCollision(gameObject, collision, false);
        }

        else //in case arrows are hitting each other somehow
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("destroying game object");
        Destroy(gameObject);
    }
}

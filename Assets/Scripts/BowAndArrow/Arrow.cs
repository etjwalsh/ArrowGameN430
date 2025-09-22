using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool canBeDestroyed = false;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with = " + collision.gameObject.name);

        //in case arrows are hitting each other somehow
        if (canBeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}

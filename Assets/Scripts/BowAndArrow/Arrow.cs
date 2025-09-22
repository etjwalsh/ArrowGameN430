using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool canBeDestroyed = false;
    void Start()
    {
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

        //in case arrows are hitting each other somehow
        if (canBeDestroyed)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("destroying game object");
        Destroy(gameObject);
    }
}

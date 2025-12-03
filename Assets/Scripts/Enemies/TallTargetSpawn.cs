using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallTargetSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //move the target to the correct position
        Vector3 pos = this.transform.position;
        pos.y += 3.0f;
        this.transform.position = pos;
    }

}

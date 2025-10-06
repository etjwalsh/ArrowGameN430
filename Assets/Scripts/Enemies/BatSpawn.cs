using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        pos.y += Random.Range(1, 10);
        this.transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject bow;

    // private bool holding = false;
    private float power;
    private int numArrows;
    public int maxArrows = 1;
    [SerializeField] private int powerScale = 10; //how much power per second builds
    [SerializeField] private int maxPower = 10; //max bow power
    private Rigidbody rbArrow;

    private void Awake()
    {
        rbArrow = arrow.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) //holding down the arrow
        {
            //instantiate the arrow ! 
            Instantiate(arrow, bow.transform.position + new Vector3(1, 1, 0), bow.transform.rotation);
            // holding = true; 
            if (power <= maxPower)
            {
                power += powerScale * Time.deltaTime; //build power up
            }
            else
            {
                power = maxPower;
            }
        }

        if (Input.GetMouseButtonUp(0)) //no longer holding down the arrow
        {
            // holding = false; 
            ShootArrow(power, arrow); //shoot the arrow
            power = 0; //reset power
        }
    }

    //p is for power
    private void ShootArrow(float p, GameObject arrow)
    {

    }
}

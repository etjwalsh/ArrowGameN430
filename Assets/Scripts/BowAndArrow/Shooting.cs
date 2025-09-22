using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject bow;

    private GameObject newArrow;
    private Arrow arrowScript;
    private float power;
    private int numArrows = 0;
    public int maxArrows = 1;
    [SerializeField] private int basePower = 150;
    [SerializeField] private int powerScale = 100; //how much power per second builds
    [SerializeField] private int maxPower = 10; //max bow power
    private Rigidbody rbArrow;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("power = " + power);
        if (Input.GetMouseButton(0)) //holding down the arrow
        {
            if (numArrows < maxArrows) //make sure the correct amount of arrows spawn
            {
                if (rbArrow == null)
                {
                    //instantiate the arrow ! 
                    //instantiate arrow object at the bow's location + a little up, rotated -85 degrees on the z axis
                    newArrow = Instantiate(arrow, bow.transform.position + new Vector3(0, 0.15f, 0), bow.transform.rotation * Quaternion.Euler(80, 0, -85));
                    numArrows++; // to count how many arrows have been spawned

                    rbArrow = newArrow.GetComponent<Rigidbody>(); //get the new arrow's rigidbody
                    arrowScript = newArrow.GetComponent<Arrow>();
                    Debug.Log(rbArrow);

                }
            }
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
            ShootArrow(power); //shoot the arrow
            power = 0; //reset power
        }
    }

    private void ShootArrow(float p) //p is for power
    {
        //set the arrow's bool for being destroyable 
        arrowScript.canBeDestroyed = true;

        //add force to the arrow based on the base power and the amount of time the arrow is held down
        rbArrow.AddForce(new Vector3(0, 100, basePower * p), ForceMode.Force);
        rbArrow.useGravity = true; //gives the arrow "bullet drop" 

        //reset all values of the arrow after it leaves the bow
        numArrows = 0;
        newArrow = null;
    }
}

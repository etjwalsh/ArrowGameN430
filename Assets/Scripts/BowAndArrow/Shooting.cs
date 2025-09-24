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
    // [SerializeField] private int basePower = 150;
    [SerializeField] private int powerScale = 100; //how much power per second builds
    [SerializeField] private int maxPower = 10; //max bow power
    private Rigidbody rbArrow;

    //variables for getting mouse position
    private Ray ray;
    private RaycastHit hit;
    private Vector3 targetPoint;

    void Start()
    {
    }

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
            ray = Camera.main.ScreenPointToRay(Input.mousePosition); //get where the mouse position is when the key is lifted up
            //cast the ray out and get the point in the distance that it hits
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else //if it doesn't hit anything
            {
                targetPoint = ray.GetPoint(1000); //target a spot 100 units down the ray
            }
            Debug.Log("ray is = " + ray);
            Debug.DrawLine(rbArrow.position, targetPoint, Color.red, 2.5f);

            ShootArrow(power, targetPoint); //shoot the arrow
            power = 0; //reset power
            newArrow = null; //reset the reference to the instantiated arrow
            rbArrow = null; //reset the reference to the instantiated arrow's rigidbody
        }
    }

    private void ShootArrow(float p, Vector3 target) //p is for power
    {
        //set the arrow's bool for being destroyable 
        arrowScript.canBeDestroyed = true;

        //add force to the arrow based on the base power and the amount of time the arrow is held down
        // rbArrow.MovePosition(new Vector3(target.x, target.y, target.z * p), ForceMode.Force);
        rbArrow.useGravity = true; //gives the arrow "bullet drop" 

        //reset all values of the arrow after it leaves the bow
        numArrows = 0;
        newArrow = null;
    }
}

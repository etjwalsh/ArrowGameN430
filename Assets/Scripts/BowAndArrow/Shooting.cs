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
    [SerializeField] private int powerScale = 100; //how much power per second builds
    [SerializeField] private int maxPower = 10; //max bow power
    [SerializeField] private float moveScale = 0.1f; //how much the bow and arrow 
    private Rigidbody rbArrow;

    //variables for raycasting
    private Ray ray;
    private RaycastHit hit;
    private Vector3 targetPoint;
    private int layerMask;


    void Start()
    {
        //set the layermask 
        layerMask = ~LayerMask.GetMask("Bow");
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
                    //instantiate arrow object at the bow's location + a little up, rotated -85 degrees on the z axis
                    newArrow = Instantiate(arrow, bow.transform.position + new Vector3(0, 0.15f, 0), bow.transform.rotation * Quaternion.Euler(0, 90, 0), bow.transform);
                    numArrows++; // to count how many arrows have been spawned

                    rbArrow = newArrow.GetComponent<Rigidbody>(); //get the new arrow's rigidbody
                    arrowScript = newArrow.GetComponent<Arrow>(); //get the new arrow's script
                }
            }
            //limit power
            if (power < maxPower)
            {
                power += powerScale * Time.deltaTime; //build power up
                //move the arrow back slighly
                rbArrow.transform.position -= new Vector3(0, -(moveScale * Time.deltaTime) / 5, moveScale * Time.deltaTime);
            }
            else
            {
                //limit power if its too high
                power = maxPower;
            }
        }

        if (Input.GetMouseButtonUp(0)) //no longer holding down the arrow
        {
            //Destroy(newArrow.GetComponent<Sway>());
            newArrow.transform.parent = null;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition); //get where the mouse position is when the key is lifted up
            //cast the ray out and get the point in the distance that it hits
            if (Physics.Raycast(ray, out hit, 500f, layerMask))
            {
                targetPoint = hit.point;
            }
            else //if it doesn't hit anything
            {
                targetPoint = ray.GetPoint(500); //target a spot 500 units down the ray
            }
            Debug.DrawLine(rbArrow.position, targetPoint, Color.red, 2.5f); //for debugging purposes. These don't actually show up in game

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

        Vector3 direction = (target - rbArrow.transform.position).normalized; //get the direction from the arrow's current position to the target

        rbArrow.transform.rotation = Quaternion.LookRotation(direction); //set the arrow to be facing in that direction

        rbArrow.velocity = direction * p; //actually shoot the arrow

        //add force to the arrow based on the base power and the amount of time the arrow is held down
        // rbArrow.MovePosition(new Vector3(target.x, target.y, target.z * p));
        rbArrow.useGravity = true; //gives the arrow "bullet drop" 

        //reset all values of the arrow after it leaves the bow
        numArrows = 0;
        newArrow = null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject bow;

    public AlembicStreamPlayer ASP;

    private GameObject newArrow;
    private Arrow arrowScript;
    private float power;
    private int numArrows = 0;
    private float moveScale = 1f; //how much the arrow moves back
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

        //get reference to alembic stream player
        ASP = bow.GetComponent<AlembicStreamPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("power is = " + power);
        if (Input.GetMouseButton(0)) //holding down the arrow
        {
            if (numArrows < GameManager.instance.maxArrows) //make sure the correct amount of arrows spawn
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
            if (power < GameManager.instance.maxPower)
            {
                power += GameManager.instance.powerScale * Time.deltaTime; //build power up
                //move the arrow back slighly
                rbArrow.transform.position -= new Vector3(0, -(moveScale * Time.deltaTime) / 5, moveScale * Time.deltaTime);

                //increase time of alembic player
                if (ASP.CurrentTime < 0.3f)
                {
                    ASP.CurrentTime += power * Time.deltaTime / 5;
                }
                else if (ASP.CurrentTime > 0.3f && ASP.CurrentTime < 0.7f)
                {
                    ASP.CurrentTime += power * Time.deltaTime / 25;
                }
                else
                {
                    ASP.CurrentTime += power * Time.deltaTime / 50;
                }
            }
            else
            {
                //limit power if its too high
                power = GameManager.instance.maxPower;
                //set alembic player to the end of the animation
                ASP.CurrentTime = ASP.EndTime;
            }

            //set this arrow's damage amount
            arrowScript.damage = Mathf.RoundToInt(power);
        }

        if (Input.GetMouseButtonUp(0)) //no longer holding down the arrow
        {
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

            //reset animation
            ASP.CurrentTime = ASP.StartTime;
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

using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

    // Assign position to head towards
    public Transform beltEnd;
    // Belt speed
    private float speed = 5.0f;
   

    // Moves pick-ups along belt at speed
    void OnTriggerStay(Collider col)
    {     
        // Only want to move crates etc tagged Pick-Up
        if (col.gameObject.tag == "Pick-Up")
        {    
            // Move pick-up towards end point
            col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, beltEnd.position, speed * Time.deltaTime);       
        }     
        else if(col.gameObject.transform.parent.gameObject.tag == "Pick-Up")
        {
            col.transform.parent.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(col.gameObject.transform.position, beltEnd.position, speed * Time.deltaTime));
        }
    }


}

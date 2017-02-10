using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

    // Assign position to head towards
    public Transform beltEnd;
    // Belt speed
    public float speed = 2.0f;

    

    // Moves pick-ups along belt at speed
    void OnTriggerStay(Collider col)
    {
      
        // Only want to move box
        if (col.gameObject.tag == "Pick-Up" && col.transform.parent.tag != "Fork")
        {
           // Debug.Log(col.transform.parent.tag);
            // Move pick-up towards end point       
            col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, beltEnd.position, speed * Time.deltaTime);
        }

        





    }
       


}

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
        // added new trigger. Need 

        if (col.isTrigger)
        {
            col.transform.position =  Vector3.MoveTowards(col.transform.position, beltEnd.position, speed * Time.deltaTime);
        }
    }
       


}

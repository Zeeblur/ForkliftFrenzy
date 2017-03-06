using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        // This might be called if the crate collides with the conveyor belt trigger area? 
        if (other.gameObject.tag != "Fork")
        {
            return;
        }

        if (transform.localPosition.y > 0.5f)
        {
            Debug.Log("pick me up");


            //TODO try parenting object
            this.transform.SetParent(other.transform);
        }
    }
    
    void OnCollisionStay(Collision colInfo)
    {
        if (colInfo.gameObject.layer == 8)
        {
            //Debug.Log("On the ground");
            this.transform.SetParent(GameObject.Find("CrateHolder").transform);
        }
    }
}

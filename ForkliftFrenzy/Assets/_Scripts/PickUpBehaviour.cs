using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (transform.localPosition.y > 0.5f)
        {
            //Debug.Log("pick me up");

            //TODO try parenting object
            this.transform.SetParent(other.transform);
        }
    }
    
    void OnCollisionStay(Collision colInfo)
    {
        if (colInfo.collider.tag == "ground")
        {
            //Debug.Log("On the ground");
            this.transform.SetParent(GameObject.Find("CrateHolder").transform);
        }
    }
}

using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (transform.position.y > 0.5f )
        Debug.Log("pick me up");

        //TODO try parenting object
        //this.transform.SetParent(other.transform);
    }
}

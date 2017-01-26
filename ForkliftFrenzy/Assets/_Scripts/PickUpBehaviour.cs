using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        Debug.Log("pick me up");

        //TODO try parenting object
        //this.transform.SetParent(other.transform);
    }
}

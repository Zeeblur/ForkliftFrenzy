using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Impacts/WoodCrate_Normal_Impact", GetComponent<Transform>().position);
    }
    void OnTriggerStay(Collider other)
    {
        
        // This might be called if the crate collides with the conveyor belt trigger area? 
        if (other.gameObject.tag != "Fork")
        {
            return;
        }
        
        //TODO buggy but works
        this.transform.SetParent(other.transform);
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

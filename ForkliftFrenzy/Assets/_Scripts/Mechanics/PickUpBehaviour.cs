using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour
{

    FMOD.Studio.EventInstance normalImpact;

    void Start()
    {
        normalImpact = FMODUnity.RuntimeManager.CreateInstance("event:/Impacts/WoodCrate_Normal_Impact");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(normalImpact, GetComponent<Transform>(), GetComponent<Rigidbody>());
        normalImpact.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        normalImpact.setVolume(0.8f);
    }
    void OnTriggerEnter(Collider other)
    {
        normalImpact.start();
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

using UnityEngine;
using System.Collections;
using FMOD.Studio;
using FMODUnity;


public class MetalImpactSFX : MonoBehaviour {

    EventInstance metalImpact;

	// Use this for initialization
	void Start () {
        metalImpact = RuntimeManager.CreateInstance("event:/Impacts/Forks_Impact_ShelfMetal");

        var me = GetComponent<Rigidbody>().gameObject.tag;

        Debug.Log("I'm on the " + me);
    }
	
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collided with "+ col.gameObject.tag);
    }
}

using UnityEngine;
using System.Collections;

public class AirConSFX : MonoBehaviour {

    FMOD.Studio.EventInstance airconSFX;

	// Setup event and play from start
	void Start () {
        airconSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/Air_Conditioning");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(airconSFX, GetComponent<Transform>(), GetComponent<Rigidbody>());
        airconSFX.setVolume(2.0f);
        airconSFX.start();
	}
	
	
}

using UnityEngine;
using System.Collections;

public class ClockIn : MonoBehaviour {

    // Set main camera in inspector
    public Camera mainCam;
    public Transform clockIn;
    public Transform player;

    public bool moveCam = false;
    public float speed = 2.0f;

    void Update()
    {
        if (moveCam)
            ClockInFocus();
    }


    private void ClockInFocus()
    {
        Debug.Log("changing cam target");
        mainCam.transform.position = clockIn.position;
        mainCam.transform.LookAt(clockIn);

    }


	
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Fork")
        {
            moveCam = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Fork")
        {
            moveCam = false;
        }
    }
}

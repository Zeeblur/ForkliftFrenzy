using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    public GameObject target;
    public float rotateSpeed = 5;

    private float currentX;
    private float currentY;

    void Start()
    {
        // set default target
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        // need invert option!!!!!
        currentY = ClampAngle(currentY, 2, 90);
    }

    float ClampAngle(float angle, float from, float to)
    {
        if (angle > 180) angle = 360 - angle;
        angle = Mathf.Clamp(angle, from, to);
        if (angle < 0) angle = 360 + angle;

        return angle;
    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        transform.position = target.transform.position + (rotation * new Vector3(0, 0, -10));
        transform.LookAt(target.transform.position, rotation * Vector3.up);
       
    }

}

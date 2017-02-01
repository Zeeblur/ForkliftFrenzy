using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    public GameObject target;
    public float rotateSpeed = 5;

    private float currentX;
    private float currentY;

    void Start()
    {
    }

    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);

        transform.position = target.transform.position + (rotation * new Vector3(0, 0, -10));
        transform.LookAt(target.transform.position, rotation * Vector3.up);
        
       
    }

}

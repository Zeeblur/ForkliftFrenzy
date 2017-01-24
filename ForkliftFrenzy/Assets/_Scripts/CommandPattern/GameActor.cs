using UnityEngine;
using System.Collections;

public class GameActor : MonoBehaviour
{
    public float speed = 2.0f;
    public float forkSpeed = 2.0f;

    private GameObject fork;
    private GameObject[] rightWheels;
    private GameObject[] leftWheels;

    public float turnSpeed = 2.0f;

    private Vector3 offset;

    // Initialise fork object
    void Start()
    {
        fork = GameObject.FindGameObjectWithTag("Fork");
        rightWheels = GameObject.FindGameObjectsWithTag("RightWheel");
        leftWheels = GameObject.FindGameObjectsWithTag("LeftWheel");

        // camera offset 
        offset = new Vector3(transform.position.x, transform.position.y + 8.0f, transform.position.z + 7.0f);
    }

    void Update()
    {
        CameraRotation();
    }

    public void Drive(float direction)
    {
        Vector3 translation = this.transform.forward;

        translation *= direction * speed * Time.deltaTime;

        this.transform.position += translation;

        float rotation = 10;
        rotation *= direction;

        // spin wheels according to direction
        SpinWheels(rightWheels, new Vector3(1.0f, 0.0f, 0.0f), rotation);
        SpinWheels(leftWheels, new Vector3(0.0f, 1.0f, 0.0f), -rotation);
    }

    public void Turn(float rotation, bool moving)
    {
        // up vector
        Vector3 axis = new Vector3(0.0f, 1.0f, 0.0f);

        this.transform.Rotate(axis, rotation);

        Vector3 currentAngle = rightWheels[0].transform.rotation.eulerAngles;

        // stopp spin at 45 degress
        if (rightWheels[0].transform.rotation.eulerAngles.y > 0.0f && rotation == 0.0f)
        {
            rightWheels[0].transform.localRotation = Quaternion.identity; //Quaternion.Lerp(Quaternion.Euler(currentAngle), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f), Time.deltaTime * speed);
        }

        if (rotation == 0.0f)
        {
           // SpinWheels(rightWheels, new Vector3(0.0f, 1.0f, 0.0f), rotation);
        }
        else
            SpinWheels(rightWheels, new Vector3(0.0f, 1.0f, 0.0f), rotation);
    }

    private void SpinWheels(GameObject[] wheels, Vector3 axis, float rotation)
    {
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(axis, rotation);
        }
    }

    public void LowerForks()
    {
        // constraint
        if (fork.transform.localPosition.y < 0.55f)
            return;

        Vector3 translation = new Vector3(0.0f, -0.1f, 0.0f);
        translation *= forkSpeed * Time.deltaTime;
        fork.transform.Translate(translation);
    }

    public void RaiseForks()
    {
        // constraint
        if (fork.transform.localPosition.y > 2.22f)
            return;

        Vector3 translation = new Vector3(0.0f, 0.1f, 0.0f);
        translation *= forkSpeed * Time.deltaTime;
        fork.transform.Translate(translation);
    }

    public void Ability()
    {

    }

    public void CameraRotation()
    {
       //float h = Input.GetAxis("Mouse Y");
       //float v = Input.GetAxis("Mouse X");
       //Camera.main.gameObject.transform.Rotate(transform.position, h* 40.0f);
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        Camera.main.transform.position = transform.position + offset;
        Camera.main.transform.LookAt(transform.position);
    }

}

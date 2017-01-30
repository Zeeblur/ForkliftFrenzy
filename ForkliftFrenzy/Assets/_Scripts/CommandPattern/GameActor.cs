using UnityEngine;
using System.Collections;

enum forkType { redneck, dinky };

public class GameActor : MonoBehaviour
{
    public float speed = 2.0f;
    public float forkSpeed = 2.0f;

    private GameObject fork;
    private GameObject upperBound;     // game object reference to highest point
    private GameObject[] backWheels;
    private GameObject[] frontWheels;
    private Light[] breakLights = new Light[2];

    public float turnSpeed = 2.0f;

    private Vector3 offset;
    private Vector3 offsetY;

    public Vector3 com;
    public Rigidbody rb;

    // temp will change
    public GameObject red;
    public GameObject dink;

    private int forkliftChoice = 0;

    // Initialise fork object
    void Start()
    {
        fork = GameObject.FindGameObjectWithTag("Fork");
        upperBound = GameObject.Find("UpperBound");
        backWheels = GameObject.FindGameObjectsWithTag("backWheels");
        frontWheels = GameObject.FindGameObjectsWithTag("frontWheels");

        // camera offset 
        offset = new Vector3(transform.position.x, transform.position.y + 8.0f, transform.position.z + 7.0f);
        offsetY = new Vector3(transform.position.x, transform.position.y, transform.position.z + 7.0f);

        BreakLightsInit();
    }

    void Update()
    {
        rb.centerOfMass = com;
        CameraRotation();

        // Temp test will change
        if (Input.GetKeyDown(KeyCode.F))
        {
            forkliftChoice ^= 1;
        }

        ChangeFork();
    }

    void ChangeFork()
    {
        switch((forkType)forkliftChoice)
        {
            case forkType.redneck:
                dink.SetActive(false);
                red.SetActive(true);
                
                break;

            case forkType.dinky:
                dink.SetActive(true);
                red.SetActive(false);

                break;
        }
    }

    public void Drive(float direction)
    {
        Vector3 translation = this.transform.forward;

        translation *= direction * speed * Time.deltaTime;

        this.transform.position += translation;

        float rotation = 10;
        rotation *= direction;

        // spin wheels according to direction
        SpinWheels(backWheels, new Vector3(1.0f, 0.0f, 0.0f), rotation);
        SpinWheels(frontWheels, new Vector3(1.0f, 0.0f, 0.0f), rotation);
    }

    public void Turn(float rotation, bool moving)
    {
        // spin wheels

        // stopp spin at 45 degress
        if (frontWheels[0].transform.rotation.eulerAngles.y > 0.0f && rotation == 0.0f)
        {
            foreach(GameObject frontWheel in frontWheels)
            {
                frontWheel.transform.localRotation = Quaternion.identity;
            }
        }

        if (rotation == 0.0f)
        {
            // SpinWheels(rightWheels, new Vector3(0.0f, 1.0f, 0.0f), rotation);
        }
        else
        {
            SpinWheels(frontWheels, new Vector3(0.0f, 1.0f, 0.0f), rotation);
        }

        // translation of fork
        if (!moving)
            return;

        // up vector
        Vector3 axis = new Vector3(0.0f, 1.0f, 0.0f);

        this.transform.Rotate(axis, rotation);
               
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
        if (fork.transform.localPosition.y < 0.0f)
            return;

        Vector3 translation = new Vector3(0.0f, -0.1f, 0.0f);
        translation *= forkSpeed * Time.deltaTime;
        fork.transform.Translate(translation);
    }

    public void RaiseForks()
    {
        // constraint
        if (fork.transform.localPosition.y > upperBound.transform.localPosition.y)
            return;

        Vector3 translation = new Vector3(0.0f, 0.1f, 0.0f);
        translation *= forkSpeed * Time.deltaTime;
        fork.transform.Translate(translation);
    }

    public void Ability()
    {

    }

    private void BreakLightsInit()
    {
        GameObject[] lightGO = GameObject.FindGameObjectsWithTag("breakLights");

        for (int i = 0; i < lightGO.Length; ++i)
        {
            // get reference to each light then set to off
            breakLights[i] = lightGO[i].GetComponent<Light>();
            breakLights[i].enabled = false;
        }
    }

    public void CameraRotation()
    {
       //float h = Input.GetAxis("Mouse Y");
       //float v = Input.GetAxis("Mouse X");
       //Camera.main.gameObject.transform.Rotate(transform.position, h* 40.0f);
    }

    void LateUpdate()
    {
        // transform x asis
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        //offsetY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
        Camera.main.transform.position = transform.position + offset;// + offsetY;
        Camera.main.transform.LookAt(transform.position);
    }

}

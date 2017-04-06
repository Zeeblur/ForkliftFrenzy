using UnityEngine;
using System.Collections.Generic;

public class GameActor : MonoBehaviour
{
    public float speed = 2.0f;
    public float forkSpeed = 2.0f;

    private GameObject fork;
    private GameObject upperBound;     // game object reference to highest point
    private List<GameObject> backWheels = new List<GameObject>();
    private List<GameObject> frontWheels = new List<GameObject>();
    private Light[] breakLights = new Light[2];

    public float turnSpeed = 2.0f;

    public Vector3 com;
    public Rigidbody rb;

    // reference to models to instantiate forklift at runtime
    public GameObject[] forkModelPrefabs;

    private ForkLift currentFork = ForkLift.ENGIE;

    // Initialise fork object
    void Awake()
    {
        ChangeFork(ForkLift.ENGIE);
    }

    private void AttachForkliftModel()
    {
        fork = GameObject.FindGameObjectWithTag("Fork");
        upperBound = GameObject.Find("UpperBound");

        // find wheels and add to list

        backWheels = new List<GameObject>();
        backWheels.AddRange(GameObject.FindGameObjectsWithTag("backWheels"));

        frontWheels.Clear();
        frontWheels.AddRange(GameObject.FindGameObjectsWithTag("frontWheels"));

       // BreakLightsInit();
    }

    public void ChangeFork(ForkLift forkliftChoice)
    {
        // get rid of old fork
        if (transform.childCount > 1)
            DestroyImmediate(transform.GetChild(1).gameObject);

        GameObject forklift = Instantiate(forkModelPrefabs[(int)forkliftChoice]) as GameObject;
        forklift.transform.SetParent(this.transform, false);

        // change target of camera to new fl
        Camera.main.gameObject.GetComponent<FollowCam>().target = forklift;
        AttachForkliftModel();
        currentFork = forkliftChoice;
    }

    public void Drive(float direction)
    {
        

        Vector3 translation = this.transform.forward;

        translation *= direction * speed * Time.deltaTime;

        this.transform.position += translation;

        if (currentFork == ForkLift.TANK || currentFork == ForkLift.SPEEDY)
            return;

        float rotation = 10;
        rotation *= direction;



        // spin wheels according to direction
        SpinWheels(backWheels, new Vector3(1.0f, 0.0f, 0.0f), rotation);
        SpinWheels(frontWheels, new Vector3(1.0f, 0.0f, 0.0f), rotation);
    }

    public void Turn(float rotation, bool moving)
    {


        // stopp spin at 45 degress
        //if (frontWheels[0].transform.rotation.eulerAngles.y > 0.0f && rotation == 0.0f && moving)

        //if (rotation == 0.0f && moving)
        //{
        //    // reset wheels
        //    foreach (GameObject frontWheel in frontWheels)
        //    {
        //        frontWheel.transform.localRotation = Quaternion.identity;
        //    }
        //}


        // rotate wheel
       // SpinWheels(frontWheels, new Vector3(0.0f, 1.0f, 0.0f), rotation);


        // do not turn when not moving forwards
        if (!moving)
            return;

        // up vector
        Vector3 axis = new Vector3(0.0f, 1.0f, 0.0f);

        this.transform.Rotate(axis, rotation);
               
    }

    private void SpinWheels(List<GameObject> wheels, Vector3 axis, float rotation)
    {
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(axis, rotation);
        }
    }

    public void LowerForks()
    {
        if (currentFork == ForkLift.SPEEDY)
        {
            GameObject[] pistons = GameObject.FindGameObjectsWithTag("Piston");
            Vector3 trans = new Vector3(0.0f, -0.1f, -0.1f);
            trans *= forkSpeed * Time.deltaTime;

            foreach(GameObject go in pistons)
            {
                go.transform.Translate(trans);
            }
            return;
        }

        // constraint
        if (fork.transform.localPosition.y < 0.0f)
            return;

        Vector3 translation = new Vector3(0.0f, -0.1f, 0.0f);
        translation *= forkSpeed * Time.deltaTime;
        fork.transform.Translate(translation);
    }

    public void RaiseForks()
    {
        if (currentFork == ForkLift.SPEEDY)
        {
            GameObject[] pistons = GameObject.FindGameObjectsWithTag("Piston");
            Vector3 trans = new Vector3(0.0f, +0.1f, +0.1f);
            trans *= forkSpeed * Time.deltaTime;

            foreach (GameObject go in pistons)
            {
                go.transform.Translate(trans);
            }
            return;
        }

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
}

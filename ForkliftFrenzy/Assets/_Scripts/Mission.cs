using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// spawn struct
public struct Spawn
{
    // Game world transform of box spawn
    Transform location;
    // Has this spawn been used/filled?
    public bool full;

    // Constructor - set properties
    public Spawn(Transform t)
    {
        location = t;
        full = false;
    }

    // Get method, returns location
    public Transform GetTransform()
    {
        return location;
    }
}

// Mission class called by GM - spawns boxes in random spawn locatios based on mission difficulty
public class Mission : MonoBehaviour {

    // Assign crate prefab in inspector
    public GameObject cratePrefab;
    // Hold list of all available spawn locations on map
    private List<Spawn> spawnLoc = new List<Spawn>();
    // List to hold all created boxes
    private List<GameObject> missionCrates = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        // Setup list of spawn points
        SpawnLocationInit();
        // Debugging - remove later
        Debug.Log("spawn locations: " + spawnLoc.Count);
	}
	
	
    // Add all possible spawn locations for crates to spawnLoc list
    void SpawnLocationInit()
    {
        // Grab all game objects tagged as crateSpawn
        GameObject[] parents = GameObject.FindGameObjectsWithTag("crateSpawn");

        // for every shelf
        foreach (GameObject go in parents)
        {
            // add each child
            foreach (Transform tr in go.transform)
            {
                // to the list!
                spawnLoc.Add(new Spawn(tr));
            }
        }
    }

    // Spawn number of boxes in random spawn locations
    // Number determined by given difficulty (enum in GM)
    public void SpawnBoxes(Difficulty choice)
    {
        Debug.Log("Spawning boxes...");
        for (int i = 0; i < (int)choice; i++)
        {
            // Instantiate crate prefab
            GameObject crate = Instantiate(cratePrefab) as GameObject;
            // Pick a random spawn point
            int rng = Random.Range(0, spawnLoc.Count);

            // keep checking if space is full
            while (spawnLoc[rng].full)
            {
                rng = Random.Range(0, spawnLoc.Count);
            }

            // Set crate transform to spawn it at location
            crate.transform.SetParent(spawnLoc[rng].GetTransform(), false);

            // Add crate to list of mission crates
            missionCrates.Add(crate);
        }
        Debug.Log(missionCrates.Count + " boxes spawned");
    }


    // Update missionCrates list after delivering box - called by GM.SendBox()
    public void UpdateCratesList(GameObject crate)
    {
        // Remove given crate from list
        missionCrates.Remove(crate);
        Debug.Log(missionCrates.Count + " crates left in current mission");

        // Check if all crates delivered
        if(missionCrates.Count == 0)
        {
            // Mission success!
            Debug.Log("Mission success! All crates delivered!");
        }
    }

    // Remove any crates left over from previous mission - called by GM
    public void ClearBoxes()
    {

    }
}

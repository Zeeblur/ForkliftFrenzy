using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// spawn struct
public struct Spawn
{
    Transform location;
    public bool full;

    public Spawn(Transform t, bool f)
    {
        location = t;
        full = f;
    }

    public Transform GetTransform()
    {
        return location;
    }
}

public class Mission : MonoBehaviour {

    public GameObject cratePrefab;
    private List<Spawn> spawnLoc = new List<Spawn>();

	// Use this for initialization
	void Start ()
    {
        SpawnLocationInit();
        Debug.Log("spawn" + spawnLoc.Count);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    // retreive all possible spawn locations for crates
    void SpawnLocationInit()
    {
        GameObject[] parents = GameObject.FindGameObjectsWithTag("crateSpawn");

        // for every shelf
        foreach (GameObject go in parents)
        {
            // add each child
            foreach (Transform tr in go.transform)
            {
                spawnLoc.Add(new Spawn(tr, false));
            }
        }
    }

    void ClearBoxes()
    {

    }

    public void SpawnBoxes(Difficulty choice)
    {
        for (int i = 0; i < (int)choice; i++)
        {
            GameObject crate = Instantiate(cratePrefab) as GameObject;

            int rng = Random.Range(0, spawnLoc.Count);

            // keep checking if space is full
            while (spawnLoc[rng].full)
            {
                rng = Random.Range(0, spawnLoc.Count);
            }


            crate.transform.SetParent(spawnLoc[rng].GetTransform(), false);

            // set spawnlocation to full to stop them spawning ontop of eachother
            spawnLoc[rng] = new Spawn(spawnLoc[rng].GetTransform(), true);
        }
    }
}

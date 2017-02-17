using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Determines number of boxes to be spawned based on difficulty choice
public enum Difficulty { EASY = 5, MEDIUM = 10, HARD = 15 };

// GM starts missions, updates HUD with mission timer & player score
public class GameManager : MonoBehaviour {

    // Is a mission already in progress?
    private bool inPlay = false;
    // Mission time
    private float timeLeft = 120;

    public Mission currentMission; // Is it better to use GetComponent/similar here rather than dragging via inspector for code files? Or create mission anew when necessary?
    // HUD items
    private Text timer;
    private Text score;
    // Current player score
    private int currentScore = 0;

   
	// Assign timer & score Text objects
	void Start () {
        timer = GameObject.Find("Timer").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If mission in progress,
        if (inPlay)
        {
            // Update timer & score on HUD
            UpdateUI();
        }
	}

    // Start a new mission, called from ClipboardUI.cs
    public void StartMission(Difficulty choice)
    {
        // Ensure a mission isn't already in progress
        if(!inPlay)
        {
            // Mission now in progress
            inPlay = true;
            // Start mission
            currentMission.SpawnBoxes(choice);
            Debug.Log("GM calling start mission");
        }
    }

    // Keeps HUD updated to display timer & score
    private void UpdateUI()
    {
        timeLeft -= Time.deltaTime;

        // format time float 
        int val = (int)(timeLeft * 100.0f);
        int minutes = val / (60 * 100);
        int seconds = (val % (60 * 100)) / 100;
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        // update score
        score.text = currentScore.ToString();

    }

    // Add points to player score based on box type
    // Removes crate from missionCrates list
    public void SendBox(GameObject crate)
    {
        // check for type of box here
        currentScore += 200;

        // Remove this box from missionCrates list
        currentMission.UpdateCratesList(crate);
    }
}

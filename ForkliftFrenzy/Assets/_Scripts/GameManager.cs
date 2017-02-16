using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Difficulty { EASY = 1, MEDIUM, HARD };

public class GameManager : MonoBehaviour {

    // Singleton pattern
    public static GameManager instance = null;


    private bool inPlay = false;
    private float timeLeft = 120;

    public Mission currentMission; // Is it better to use GetComponent/similar here rather than dragging via inspector for code files?

    private Text timer;
    private Text score;

    private int currentScore = 0;

    // Ensure only 1 instance of GM
    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
            // if not, set instance to this
            instance = this;
        // if instance already exists & is not this
        else if (instance != this)
            // Then destroy this (singleton)
            Destroy(gameObject);

        // Sets this to be not destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }


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
    public void SendBox(GameObject box)
    {
        // check for type of box here
        currentScore += 200;
    }
}

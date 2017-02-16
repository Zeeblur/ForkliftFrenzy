using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Difficulty { EASY = 1, MEDIUM, HARD };

public class GameManager : MonoBehaviour {

    private bool inPlay = false;
    private float timeLeft = 120;

    public Mission currentMission;

    private Text timer;
    private Text score;

    private int currentScore = 0;

	// Use this for initialization
	void Start () {
        timer = GameObject.Find("Timer").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!inPlay && Input.GetKeyDown(KeyCode.P))
        {
            inPlay = true;
            currentMission.SpawnBoxes(Difficulty.HARD);
            Debug.Log("Start Mission");
        }
	
        if (inPlay)
        {
            UpdateUI();
        }
	}

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

    public void SendBox(GameObject box)
    {
        // check for type of box here
        currentScore += 200;
    }
}

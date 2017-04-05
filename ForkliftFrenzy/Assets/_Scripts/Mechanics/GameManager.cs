using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum Difficulty { EASY = 1, MEDIUM, HARD };
public enum ForkLift { SPEEDY, ENGIE, TANK, TRICKSY };

public class GameManager : MonoBehaviour {
    // Mission in progress already?
    private bool inPlay = false;
    private float timeLeft = 60;
    // Mission should be component attached to Warehouse
    public Mission currentMission;
    // For HUD canvas
    private Text timer;
    private Text score;
    // Updates to be shown on HUD
    private int currentScore = 0;

    // var for persistence
    private PlayerData playerData;
    private GameActor playerScript;

    // to store current boxes in play
    public int totalBoxes = 0;

    // multiplier for difficulty
    public const int boxMultiplier = 10;
    // Shown on mission finish
    public GameObject endGameUI;
    private string endGameMessage;
    private bool gameOver = false;

    public GameObject ForkliftSelection;
    public GameObject Scene;
    public GameObject PauseScreen;

    private ForkliftLoader forkliftLoader;

	// Use this for initialization
	void Start () {
        timer = GameObject.Find("Timer").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
        playerData = GetComponent<PlayerData>();
        playerScript = GameObject.Find("Player").GetComponent<GameActor>();

        // store original endgame message for parsing
        endGameMessage = endGameUI.GetComponentInChildren<Text>().text;

        // initial forklift unlock for player
        playerData.UnlockForklift(ForkLift.ENGIE);

        forkliftLoader = new ForkliftLoader(playerData);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	
        if (inPlay)
        {
            UpdateUI();

            if (timeLeft <= 0 || totalBoxes == 0)
                EndGame();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ShowForkliftSelection(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (ForkliftSelection.activeSelf)
                {
                    ShowForkliftSelection(false);
                    return;
                }

                ShowPauseScreen(!PauseScreen.activeSelf);
            }
        }

        if (gameOver && Input.anyKeyDown)
        {
            ResetGame();
        }

        // gamehacks to add score and end game quickly
        if (Input.GetKeyDown(KeyCode.I))
            currentScore += 200;

        if (Input.GetKeyDown(KeyCode.N))
            EndGame();
	}

    // show forklift selection screen
    public void ShowForkliftSelection(bool show)
    {
        Debug.Log("Show Screen");
        ForkliftSelection.SetActive(show);
        Scene.SetActive(!show);
    }

    public void ShowPauseScreen(bool show)
    {
        Debug.Log("Show Pause Screen");
        PauseScreen.SetActive(show);
        Scene.SetActive(!show);
    }

    // Start le mission with given difficulty
    public void StartMission(Difficulty choice)
    {
        // Inplay check
        if (inPlay)
            return;

        inPlay = true;
        currentMission.ClearBoxes();
        currentMission.SpawnBoxes(choice);
        totalBoxes = (int)choice * boxMultiplier;

        // TODO change to var depending on difficulty
        timeLeft = 60;
        Debug.Log("Start Mission");
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

    private void ResetGame()
    {
        Debug.Log("Reset");
        gameOver = false;
        inPlay = false;

        // hide UI
        endGameUI.SetActive(false);
        currentScore = 0;
        timeLeft = 0;

        UpdateUI();

    }

    private void EndGame()
    {
        inPlay = false;
        gameOver = true;

        // update player data
        bool highbeat = playerData.HasBeatenHighscore(currentScore);
        playerData.dirty = true;

        // set original end game message
        string newText = endGameMessage;

        // update text and show ui
        if (highbeat)
        {
            newText = newText.Replace("HS", "You beat your highscore!");
        }
        else
        {
            newText = newText.Replace("HS", "\n");
        }

        newText = newText.Replace("%", "" + currentScore);

        // replace money with earnings
        int earnings = currentScore / 10;

        newText = newText.Replace("$", "" + earnings);

        int timeBonus = 0;

        // time bonus for extra time
        if (timeLeft >= 10)
        {
            // get rounded 
            timeBonus = (int)timeLeft / 10;

            timeBonus *= 10;
            
            newText = newText.Replace("tb", "+ Time Bonus " + timeBonus);
        }
        else
        {
            newText = newText.Replace("tb", "");
        }

        playerData.AddMoney(earnings + timeBonus);

        endGameUI.GetComponentInChildren<Text>().text = newText;
        Debug.Log("Show UI END");
        endGameUI.SetActive(true);
    }

    public void SendBox(GameObject box)
    {
        // TODO: Check for type of box here
        currentScore += 200;
        totalBoxes--;
    }


    // collider for respawn zone
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "backWheels" || other.tag == "frontWheels")
        {
            // TODO will need to change this to UI, just for testing 1234 keys for forklift select
        }
    }


    // returns forklift data
    public List<ForkliftData> GetForklifts()
    {
        return forkliftLoader.forkliftList;
    }
    
    public void NewForkliftSelection(int choice)
    {
        // hide forklift ui and change fork
        ShowForkliftSelection(false);
        playerScript.ChangeFork((ForkLift)choice);
    }

    public bool BuyForklift(int choice)
    {
        // update player pref.

        // remove money from player
        if (!playerData.AddMoney(forkliftLoader.forkliftList[choice].price))
        {
            // if cannot remove money then return false, forklift is not bought
            Debug.Log("Cannot buy forklift");
            return false;
        }

        // else unlock fork
        playerData.UnlockForklift((ForkLift)choice);

        // able to be bought
        return true;
    }
}

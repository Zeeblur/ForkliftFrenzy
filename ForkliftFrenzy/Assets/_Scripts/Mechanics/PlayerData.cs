using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    // used to get/set values needed for player persistence

    /* Needed Values
     * UserName: String
     * currentScore: (maybe) 
     * HighestScore: int
     * Level?XP if doing
     * Unlocked forklifts: bool[]
     * Money: int
     * 
     */

    // keys for access
    private string userKey = "Username";

    // key for unlocked forks
    private string[] forkKey;

    private string highKey = "HighestScore";

    // test var for persistence
    private Text highScore;

    public bool dirty = false; // flag to see if player needs saving

    void Awake()
    {
        forkKey = Enum.GetNames(typeof(ForkLift));

        // test text canvas
        highScore = GameObject.Find("HighScore").GetComponent<Text>();

        GatherData();
    }

    void Update()
    {
        // if player has changed, update preferences
        if (dirty)
            UpdateData();
    }

    private void GatherData()
    {
        // check if keys exist. If so update ui
        if (PlayerPrefs.HasKey(highKey))
            highScore.text = "HighScore: " + PlayerPrefs.GetInt(highKey);

    }

    private void UpdateData()
    {

        // update ui test
        highScore.text = "HighScore: " + PlayerPrefs.GetInt(highKey);

        // write data
        PlayerPrefs.Save();

        dirty = false;
    }

    public bool HasBeatenHighscore(int currScore)
    {
        if (currScore > PlayerPrefs.GetInt(highKey))
        {
            // update player preferences 
            PlayerPrefs.SetInt(highKey, currScore);
            return true;
        }
        else { return false; }
    }
}

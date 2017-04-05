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
    private bool[] unlockedForks = { false, false, false, false };

    private string highKey = "HighestScore";

    private string moneyKey = "Money";

    // test var for persistence
    private Text highScore;

    public bool dirty = false; // flag to see if player needs saving

    private int totalMoney;

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
        
        // get fork data
        for (int i = 0; i < forkKey.Length; i++)
        {
            if (PlayerPrefs.HasKey(forkKey[i]))
            {
                // if it's a 1, fork is unlocked, else not locked
                unlockedForks[i] = PlayerPrefs.GetInt(forkKey[i]) > 0 ? true : false;
            }
        }

        totalMoney = PlayerPrefs.GetInt(moneyKey);

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

    // return false if not enough money left
    public bool AddMoney(int moneyIN)
    {

        // if subtracting moneys - check if funds are ok if not return false
        if (moneyIN < 0 && totalMoney < moneyIN)
            return false;

        // update money
        totalMoney += moneyIN;
        PlayerPrefs.SetInt(moneyKey, totalMoney);

        return true;
    }

    public void UnlockForklift(ForkLift choice)
    {
        PlayerPrefs.SetInt(forkKey[(int)choice], 1);

        // update local copy
        unlockedForks[(int)choice] = true;
    }

    public bool IsForkUnlocked(ForkLift choice)
    {
        return unlockedForks[(int)choice];
    }

    public void ResetPlayerData()
    {
        // Lock all forklifts
        for (int i = 0; i < unlockedForks.Length; i++)
        {
            PlayerPrefs.SetInt(forkKey[i], 0);
            unlockedForks[i] = false;
        }

        // reset highscore
        PlayerPrefs.SetInt(highKey, 0);

        // reset money
        PlayerPrefs.SetInt(moneyKey, 0);
        totalMoney = 0;

        // starter forklift unlock
        UnlockForklift(ForkLift.ENGIE);

        //update data
        UpdateData();
    }
}

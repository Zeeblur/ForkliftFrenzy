using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Deals with UI appearing on clock-in clipboard. Allows difficulty select &
// start mission buttons. Tracks difficulty selected, sends data to GM on GM.StartMission(diff)

public class ClipboardUI : MonoBehaviour {

    // Assign buttons in inspector
    public Button easyBtn, medBtn, hardBtn, startBtn;
    // Public for debugging, make private later
    public string difficulty;
    public Difficulty choice;

    // Assign buttons and listeners
    void Start()
    {
        // Grab button components 
        easyBtn = easyBtn.GetComponent<Button>();
        medBtn = medBtn.GetComponent<Button>();
        hardBtn = hardBtn.GetComponent<Button>();
        startBtn = startBtn.GetComponent<Button>();
        // Add listener functions, run when clicked
        easyBtn.onClick.AddListener(EasyDiff);
        medBtn.onClick.AddListener(MedDiff);
        hardBtn.onClick.AddListener(HardDiff);
        startBtn.onClick.AddListener(StartMission);
    }

    // If easy button pressed - set difficulty to easy
    public void EasyDiff()
    {
        choice = Difficulty.EASY;   
    }
    // If med button pressed - set diff to medium
    public void MedDiff()
    {
        choice = Difficulty.MEDIUM;
    }
    // If hard button pressed = set diff to hard
    public void HardDiff()
    {
        choice = Difficulty.HARD;
    }
        
    // If start button pressed - ask GM to start mission
    public void StartMission()
    {
        // GM to call start mission
        
    }
}

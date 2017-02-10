using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Deals with UI appearing on clock-in clipboard. Allows difficulty select,
// start mission. Tracks difficulty selected, sends data to GM on GM.StartMission(diff)

public class ClipboardUI : MonoBehaviour {


    public Button easyBtn, medBtn, hardBtn, startBtn;
    public string difficulty;


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
        difficulty = "easy";
        
    }
    // If med button pressed - set diff to medium
    public void MedDiff()
    {
        difficulty = "medium";
    }
    // If hard button pressed = set diff to hard
    public void HardDiff()
    {
        difficulty = "hard";
    }
        
    // If start button pressed - ask GM to start mission
    public void StartMission()
    {
        // GM.StartMission(difficulty);
    }
}

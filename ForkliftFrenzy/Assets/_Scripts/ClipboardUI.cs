using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Deals with UI appearing on clock-in clipboard. Allows difficulty select &
// start mission buttons. Tracks difficulty selected, sends data to GM on GM.StartMission(diff)

public class ClipboardUI : MonoBehaviour {

    // Assign buttons in inspector
    public Button easyBtn, medBtn, hardBtn, startBtn;
    // Public for debugging, make private later
    public Difficulty choice;
    // Has choice been made to allow mission start
    private bool chosen = false;
    // Access GM
    public GameManager GM;
    // To be heard
    public Transform listenerPos;

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
        chosen = true;
        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);
    }
    // If med button pressed - set diff to medium
    public void MedDiff()
    {
        choice = Difficulty.MEDIUM;
        chosen = true;
        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);
    }
    // If hard button pressed = set diff to hard
    public void HardDiff()
    {
        choice = Difficulty.HARD;
        chosen = true;
        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);
    }
        
    // If start button pressed - ask GM to start mission
    public void StartMission()
    {
        if (chosen)
        {
            // GM to call start mission
            GM.StartMission(choice);
            Debug.Log("GM to call start mission");
        }
    }
}

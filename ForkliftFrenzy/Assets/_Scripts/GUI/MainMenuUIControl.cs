using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUIControl : MonoBehaviour {

    public Button SPBtn, MPBtn, OpBtn, ExBtn, YBtn, Nbtn;
    public Canvas quitPrompt;
    private bool displayQuitPrompt = false;



    void Awake()
    {
        // Ensure popup canvas is hidden on start
        quitPrompt.gameObject.SetActive(displayQuitPrompt);
    }


    void Start()
    {
        SPBtn = SPBtn.GetComponent<Button>();
        MPBtn = MPBtn.GetComponent<Button>();
        OpBtn = OpBtn.GetComponent<Button>();
        ExBtn = ExBtn.GetComponent<Button>();
        YBtn = YBtn.GetComponent<Button>();
        Nbtn = Nbtn.GetComponent<Button>();
        quitPrompt = quitPrompt.GetComponent<Canvas>();
        SPBtn.onClick.AddListener(StartSinglePlayer);
        MPBtn.onClick.AddListener(StartMultiplayer);
        OpBtn.onClick.AddListener(ShowOptions);
        ExBtn.onClick.AddListener(ShowQuitPrompt);
        YBtn.onClick.AddListener(ExitGame);
        Nbtn.onClick.AddListener(HideQuitPrompt);
    }




    private void StartSinglePlayer()
    {
        // Load sp scene

        Debug.Log("Clicked SP button.");
    }

    private void StartMultiplayer()
    {
        // Load mp scene

        Debug.Log("Clicked MP button.");
    }

    private void ShowOptions()
    {
        // Show options scene

        Debug.Log("Clicked options button");
    }

    private void ShowQuitPrompt()
    {
        Debug.Log("Clicked exit game");

        // Reverse bool
        displayQuitPrompt = !displayQuitPrompt;

        if (displayQuitPrompt)
        {
            quitPrompt.gameObject.SetActive(displayQuitPrompt);
        }

        // Disable other buttons
        SPBtn.enabled = false;
        MPBtn.enabled = false;
        OpBtn.enabled = false;

    }

    private void HideQuitPrompt()
    {
        // Re-enable main menu buttons
        SPBtn.enabled = true;
        MPBtn.enabled = true;
        OpBtn.enabled = true;

        // Hide quit prompt canvas
        displayQuitPrompt = !displayQuitPrompt;
        quitPrompt.gameObject.SetActive(displayQuitPrompt);
    }
   
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif


        Application.Quit();


    }
}

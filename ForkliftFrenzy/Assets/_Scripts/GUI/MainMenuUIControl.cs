using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MainMenuUIControl : MonoBehaviour {

    public Button SPBtn, OpBtn, ExBtn, YBtn, Nbtn;
    public Canvas quitPrompt;
    private bool displayQuitPrompt = false;
    public Transform listenerPos;


    void Awake()
    {
        // Ensure popup canvas is hidden on start
        quitPrompt.gameObject.SetActive(displayQuitPrompt);
    }


    void Start()
    {
        SPBtn = SPBtn.GetComponent<Button>();     
        OpBtn = OpBtn.GetComponent<Button>();
        ExBtn = ExBtn.GetComponent<Button>();
        YBtn = YBtn.GetComponent<Button>();
        Nbtn = Nbtn.GetComponent<Button>();
        quitPrompt = quitPrompt.GetComponent<Canvas>();
        SPBtn.onClick.AddListener(StartSinglePlayer);
        OpBtn.onClick.AddListener(ShowOptions);
        ExBtn.onClick.AddListener(ShowQuitPrompt);
        YBtn.onClick.AddListener(ExitGame);
        Nbtn.onClick.AddListener(HideQuitPrompt);


    }

    public void HoverSFX()
    {
        // Play sound effect for button hover
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Hover", listenerPos.position);
    }


    private void StartSinglePlayer()
    {
        // Load sp scene

        

        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);

        // Debug
        Debug.Log("Clicked SP button.");
    }


    private void ShowOptions()
    {
        // Show options scene

        
        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);

        // Debug
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
            // Play sound effect for button selection
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);
        }

        // Disable other buttons
        SPBtn.enabled = false;
        OpBtn.enabled = false;

    }

    private void HideQuitPrompt()
    {
        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);

        // Re-enable main menu buttons
        SPBtn.enabled = true;
        OpBtn.enabled = true;

        // Hide quit prompt canvas
        displayQuitPrompt = !displayQuitPrompt;
        quitPrompt.gameObject.SetActive(displayQuitPrompt);
    }
   
    private void ExitGame()
    {
        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif


        Application.Quit();


    }
}

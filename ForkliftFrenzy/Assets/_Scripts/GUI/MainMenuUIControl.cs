using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class MainMenuUIControl : MonoBehaviour
{

    public Transform listenerPos;

    public Texture quitBackg, yesIm, noIm;

    private MovieTexture movie;

    private GUIStyle buttonTexture;
    public Font myFont;

    bool interaction = false;
    bool quit = false;

    FMOD.Studio.EventInstance music;
    void Awake()
    {
        movie = (MovieTexture)this.GetComponent<Renderer>().material.mainTexture;

        buttonTexture = GUIStyle.none;
        buttonTexture.font = myFont;
        buttonTexture.alignment = TextAnchor.MiddleCenter;
        buttonTexture.fontSize = 30;

        listenerPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Opening_Menu");
        music.start();
    }

    public void HoverSFX()
    {
        // Play sound effect for button hover
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Hover", listenerPos.position);
    }

    private void OnGUI()
    {

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movie, ScaleMode.StretchToFill);
        movie.Play();


        // wait for film to play
        StartCoroutine(Wait(movie.duration));

        if (!interaction)
            return;


        float buttonWidthStart = 330;
        float buttonHeightStart = 150;

        // initialise buttons
        bool singleBtn = GUI.Button(new Rect(buttonWidthStart, buttonHeightStart, 200, 100), "Single Player", GUIStyle.none);
        bool exitBtn = GUI.Button(new Rect(buttonWidthStart, buttonHeightStart + 410, 200, 100), "Exit", GUIStyle.none);

        if (singleBtn)
            StartSinglePlayer();

        if (exitBtn)
            ShowQuitPrompt();

        if (quit)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), quitBackg, ScaleMode.ScaleToFit);

            bool yes = GUI.Button(new Rect(buttonWidthStart + 420, buttonHeightStart + 300, 100, 100), yesIm, GUIStyle.none);
            bool no = GUI.Button(new Rect(buttonWidthStart - 150, buttonHeightStart + 300, 100, 100), noIm, GUIStyle.none);

            if (yes)
                ExitGame();

            if (no)
                HideQuitPrompt();
        }

    }


    private IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        interaction = true;
    }

    private void StartSinglePlayer()
    {

        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);
        // Stop  bg music
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // Load sp scene
        SceneManager.LoadScene("SoundProto");
    }


    private void ShowQuitPrompt()
    {
        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);

        // show prompt 
        quit = true;
    }

    private void HideQuitPrompt()
    {
        // Play sound effect for button selection
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Mouse_Click", listenerPos.position);

        // Hide quit prompt canvas
        quit = false;
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

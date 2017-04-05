using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUIControl : MonoBehaviour {

    public Texture quitBackg, yesIm, noIm;
    private bool displayQuitPrompt = false;

    private MovieTexture movie;

    private GUIStyle buttonTexture;
    public Font myFont;

    bool interaction = false;
    bool quit = false;

    void Awake()
    {
        movie = (MovieTexture)this.GetComponent<Renderer>().material.mainTexture;

        buttonTexture = GUIStyle.none;
        buttonTexture.font = myFont;
        buttonTexture.alignment = TextAnchor.MiddleCenter;
        buttonTexture.fontSize = 30;
        
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
        // Load sp scene

        Debug.Log("Clicked SP button.");
        SceneManager.LoadScene("SPPrototype");
    }

    private void ShowQuitPrompt()
    {
        // show prompt 
        quit = true;
    }

    private void HideQuitPrompt()
    {
        // Hide quit prompt canvas
        quit = false;
    }
   
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif


        Application.Quit();


    }
}

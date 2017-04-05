using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScreen : MonoBehaviour {

    public Button resumeBtn;
    public Button restartBtn;
    public Button resetCharBtn;
    public Button quitBtn;

    public Image sticker;

    public Sprite[] sprites;

    public GameManager gameMan;

	// Use this for initialization
	void Start ()
    {
        // add handlers
        resumeBtn.onClick.AddListener(Resume);
        restartBtn.onClick.AddListener(Restart);
        resetCharBtn.onClick.AddListener(ResetChar);
        quitBtn.onClick.AddListener(Quit);


        // chose random image for sticker
        RandomiseSticker();
    }

    public void Resume()
    {
        // handler for resuming game, closes window unpauses play
        gameMan.ShowPauseScreen(false);
    }

    public void Restart()
    {
        // refreshes mission quits early
        gameMan.ShowPauseScreen(false);
        gameMan.EndGame();
    }

    public void ResetChar()
    {
        // clears player data pref
        gameMan.ClearPlayer();
        gameMan.ShowPauseScreen(false);
    }

    public void Quit()
    {
        // closes game

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void RandomiseSticker()
    {
        int choice = Random.Range(0, 9);

        sticker.sprite = sprites[choice];
    }

    // handler for enabling
    private void OnEnable()
    {
        RandomiseSticker();

        // pause game
        gameMan.inPlay = false;
    }

    private void OnDisable()
    {
        // restart play
        gameMan.inPlay = true;
    }
}

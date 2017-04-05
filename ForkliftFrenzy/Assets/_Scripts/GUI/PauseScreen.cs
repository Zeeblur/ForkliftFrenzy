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

	// Use this for initialization
	void Start ()
    {
        // add handlers
        resumeBtn.onClick.AddListener(resume);
        restartBtn.onClick.AddListener(restart);
        resetCharBtn.onClick.AddListener(resetChar);
        quitBtn.onClick.AddListener(quit);


        // chose random image for sticker
        randomiseSticker();
    }

    public void resume()
    {
        // handler for resuming game, closes window unpauses play
    }

    public void restart()
    {
        // refreshes mission quits early
    }

    public void resetChar()
    {
        // clears player data pref
    }

    public void quit()
    {
        // closes game
    }

    public void randomiseSticker()
    {
        int choice = Random.Range(0, 9);

        sticker.sprite = sprites[choice];
    }
}

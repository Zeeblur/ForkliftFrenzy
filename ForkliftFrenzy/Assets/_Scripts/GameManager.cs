using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    private bool inPlay = false;
    private float timeLeft = 30;

    private Text timer;

	// Use this for initialization
	void Start () {
        timer = GameObject.Find("Timer").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!inPlay && Input.GetKeyDown(KeyCode.P))
        {
            inPlay = true;
            Debug.Log("Start Mission");
        }
	
        if (inPlay)
        {
            UpdateTimer();
        }
	}

    private void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;

        // format time float 
        int val = (int)(timeLeft * 100.0f);
        int minutes = val / (60 * 100);
        int seconds = (val % (60 * 100)) / 100;
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

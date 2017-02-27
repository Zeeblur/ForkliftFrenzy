using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public struct ForkliftData
{
    public int speed;
    public int carryCap;
    public int price;
    public Sprite image;
}

public class ForkliftSelectionUI : MonoBehaviour {

    private Text stats;
    private ForkliftData[] forks = new ForkliftData[4];

    public Button buySelect; // reference to button

    private bool forkAvalible = false;

	void Awake ()
    {
        // On awake load in resource files and data for images of forklifts.

        // add event handler for button
        buySelect.onClick.AddListener(BuySelect);
	}

    private void UpdateForkliftShown(ForkLift choice)
    {
        // update text and image
        stats.text = stats.text.Replace("dolla", forks[(int)choice].price.ToString());
        stats.text = stats.text.Replace("flspeed", forks[(int)choice].speed.ToString());
        stats.text = stats.text.Replace("cc", forks[(int)choice].carryCap.ToString());

        // update imagefork
        // get player pref see if avalible to update bool
    }

    public void BuySelect()
    {
        if (forkAvalible)
        {
            // allow user to select on click
            //GM change forklift to current choice

        }
        else
        {
            // buy forklift
            // update player pref and UI
        }
    }
}

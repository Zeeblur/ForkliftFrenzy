using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ForkliftSelectionUI : MonoBehaviour {

    public Text stats;

    public Button buySelect; // reference to buttons
    public Button next;
    public Button back;

    private bool forkAvalible = false;

    private ForkliftLoader loader;

    private Transform playerImage;

    private int currentSelection;

    public Image logo;

    public Sprite[] forkliftLogos;

    private string statsInit;

	void Awake ()
    {
        // On awake load in resource files and data for images of forklifts.
        loader = GetComponent<ForkliftLoader>();

        // add event handler for button
        buySelect.onClick.AddListener(BuySelect);
        next.onClick.AddListener(Next);
        back.onClick.AddListener(Back);

        playerImage = GameObject.FindGameObjectWithTag("forkliftSelect").transform;

        statsInit = stats.text;
	}

    private void Start()
    {
        UpdateForkliftShown((int)ForkLift.SPEEDY);
    }

    private void UpdateForkliftShown(int choice)
    {
        // check for null, delete if something is there
        if (playerImage.childCount > 0)
           DestroyImmediate(playerImage.GetChild(0).gameObject);

        // reset to unparsed string
        stats.text = statsInit;

         // update text and image
        stats.text = stats.text.Replace("dolla", loader.forkliftList[choice].price.ToString());
        stats.text = stats.text.Replace("flspeed", loader.forkliftList[choice].speed.ToString());
        stats.text = stats.text.Replace("cc", loader.forkliftList[choice].carryCap.ToString());

        // update imagefork
        // get player pref see if avalible to update bool
        GameObject forkPref = Instantiate(loader.forkliftList[choice].forkModel) as GameObject;

        forkPref.transform.SetParent(playerImage, false);

        logo.sprite = forkliftLogos[choice];
        currentSelection = choice;

        forkAvalible = loader.forkliftList[currentSelection].unlocked;
        buySelect.GetComponentInChildren<Text>().text = forkAvalible ? "SELECT" : "BUY";
    }

    //  Buy/select button handler
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

    // Switch to next model
    public void Next()
    {
        currentSelection = (currentSelection == loader.forkliftList.Count -1) ? 0 : currentSelection + 1;
        UpdateForkliftShown(currentSelection);
    }

    // Switch to last model
    public void Back()
    {
        currentSelection = (currentSelection == 0) ? loader.forkliftList.Count -1 : currentSelection - 1;
        UpdateForkliftShown(currentSelection);
    }


    // Rotate the model
    private void Update()
    {
        playerImage.Rotate(new Vector3(0, 1.0f, 0), 1);
    }
}

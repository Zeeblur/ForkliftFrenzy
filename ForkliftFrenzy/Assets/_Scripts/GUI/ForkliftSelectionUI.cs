using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class ForkliftSelectionUI : MonoBehaviour {

    public Text stats;
    public Text funds;

    public Button buySelect; // reference to buttons
    public Button next;
    public Button back;

    private bool forkAvalible = false;

    private Transform playerImage;

    private int currentSelection;

    public Image logo;

    public Sprite[] forkliftLogos;

    private string statsInit;

    public GameManager gameMan;

    private ForkliftData[] forkData;

    private Vector3 originalPosition;

	void Awake ()
    {
        // add event handler for button
        buySelect.onClick.AddListener(BuySelect);
        next.onClick.AddListener(Next);
        back.onClick.AddListener(Back);

        playerImage = GameObject.FindGameObjectWithTag("forkliftSelect").transform;

        statsInit = stats.text;

        // populate current forklift data
        forkData = gameMan.GetForklifts().ToArray();

        originalPosition = playerImage.position;
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

        // adjust for larger forklift
        Vector3 adjustment = new Vector3(0.0f, 1.2f, 0.0f);

        if (choice == (int)ForkLift.TANK)
            playerImage.position = originalPosition - adjustment;
        else if (choice == (int)ForkLift.SPEEDY)
            playerImage.position = originalPosition - (adjustment * 1.2f);
        else
            playerImage.position = originalPosition;

            // reset to unparsed string
            stats.text = statsInit;

         // update text and image
        stats.text = stats.text.Replace("dolla", forkData[choice].price.ToString());
        stats.text = stats.text.Replace("flspeed", forkData[choice].speed.ToString());
        stats.text = stats.text.Replace("cc", forkData[choice].carryCap.ToString());

        funds.text = "Funds £" + PlayerPrefs.GetInt("Money");

        // update imagefork
        // get player pref see if avalible to update bool
        GameObject forkPref = Instantiate(forkData[choice].forkModel) as GameObject;

        forkPref.transform.SetParent(playerImage, false);

        logo.sprite = forkliftLogos[choice];
        currentSelection = choice;

        forkAvalible = forkData[currentSelection].unlocked;
        buySelect.GetComponentInChildren<Text>().text = forkAvalible ? "SELECT" : "BUY";
    }

    //  Buy/select button handler
    public void BuySelect()
    {
        if (forkAvalible)
        {
            // allow user to select on click
            //GM change forklift to current choice
            gameMan.NewForkliftSelection(currentSelection);
        }
        else
        {
            // buy forklift
            // update player pref and UI

            // bool if true. forklift has been purchased
            if (gameMan.BuyForklift(currentSelection))
            {
                // change local forklist data
                forkData[currentSelection].unlocked = true;

                // update with buy/select and new player cash
                UpdateForkliftShown(currentSelection);
            }
            else
            {
                funds.text = "Insufficient funds";
            }
            // Pop-up spending cash? noise?
            // Or insufficent funds


        }
    }

    // Switch to next model
    public void Next()
    {
        currentSelection = (currentSelection == forkData.Length -1) ? 0 : currentSelection + 1;
        UpdateForkliftShown(currentSelection);
    }

    // Switch to last model
    public void Back()
    {
        currentSelection = (currentSelection == 0) ? forkData.Length -1 : currentSelection - 1;
        UpdateForkliftShown(currentSelection);
    }


    // Rotate the model
    private void Update()
    {
        playerImage.Rotate(new Vector3(0, 1.0f, 0), 1);
    }
}

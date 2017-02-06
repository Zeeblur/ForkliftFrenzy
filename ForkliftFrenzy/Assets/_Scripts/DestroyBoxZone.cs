using UnityEngine;
using System.Collections;

public class DestroyBoxZone : MonoBehaviour {

	void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Pick-Up")
        {
            // Send info to GM (how to access properly?)
            // GM.SendBox(col.gameObject);

            // Destroy box object
            Destroy(col.gameObject);
        }
    }
}

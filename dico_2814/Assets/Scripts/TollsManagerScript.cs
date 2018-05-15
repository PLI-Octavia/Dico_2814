using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manage tolls
public class TollsManagerScript : MonoBehaviour {

    // GameObjects
    private GameObject leftBadToll;
    private GameObject leftGoodToll;
    private GameObject rightBadToll;
    private GameObject rightGoodToll;

	// Initial state of tolls
	void Start () {
        leftBadToll = GameObject.FindWithTag("LeftBadToll");
        leftGoodToll = GameObject.FindWithTag("LeftGoodToll");
        rightBadToll = GameObject.FindWithTag("RightBadToll");
        rightGoodToll = GameObject.FindWithTag("RightGoodToll");

        // Hide left toll
        leftBadToll.SetActive(false);
        rightGoodToll.SetActive(false);

        //leftBadToll.GetComponent<Renderer>().enabled = false;
        //rightGoodToll.GetComponent<Renderer>().enabled = false;
	}

    // Handle the switch of tolls
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            // Reverse position
            leftBadToll.GetComponent<Renderer>().enabled = !leftBadToll.GetComponent<Renderer>().enabled;
            leftGoodToll.GetComponent<Renderer>().enabled = !leftGoodToll.GetComponent<Renderer>().enabled;

            rightBadToll.GetComponent<Renderer>().enabled = !rightBadToll.GetComponent<Renderer>().enabled;
            rightGoodToll.GetComponent<Renderer>().enabled = !rightGoodToll.GetComponent<Renderer>().enabled;
        }
    }
}

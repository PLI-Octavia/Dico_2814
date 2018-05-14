using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TollsManagerScript : MonoBehaviour {
    
    private GameObject left_toll;
    private GameObject right_toll;

	// Use this for initialization
	void Start () {
        left_toll = GameObject.FindWithTag("left_toll");
        right_toll = GameObject.FindWithTag("right_toll");

        left_toll.GetComponent<Renderer>().enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            left_toll.GetComponent<Renderer>().enabled = !left_toll.GetComponent<Renderer>().enabled;
            right_toll.GetComponent<Renderer>().enabled = !right_toll.GetComponent<Renderer>().enabled;
        }
    }
}

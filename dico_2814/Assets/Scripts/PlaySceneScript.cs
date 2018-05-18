using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaySceneScript : MonoBehaviour {
    
	void Start () {
        // Add listener on the play button.
        gameObject.GetComponent<Button>().onClick.AddListener(toPlayScene);
	}

    void toPlayScene() {
        // Send to game scene.
        SceneManager.LoadScene("DicoGame", LoadSceneMode.Single);
    }
}

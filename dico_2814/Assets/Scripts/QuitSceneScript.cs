using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class QuitSceneScript : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void QuitGame();
    // Use this for initialization
    void Start()
    {
        // Add listener on the play button.
        gameObject.GetComponent<Button>().onClick.AddListener(toQuitScene);
    }

    void toQuitScene()
    {
        QuitGame();
    }
}

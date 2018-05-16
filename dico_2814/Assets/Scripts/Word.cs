using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class save a WordObject in a word GameObject
public class Word : MonoBehaviour {
    public WordObject word;

    void OnBecameInvisible()
    {
        Destroy(gameObject, 2.0f);
    }

}

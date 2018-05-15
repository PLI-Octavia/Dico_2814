using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCollisionScript : MonoBehaviour {

    // Handle collision between words and tolls
    void OnCollisionEnter(Collision col)
    {
        WordObject word = col.gameObject.GetComponent<Word>().word;
        Debug.Log(word.correction);

        Destroy(col.gameObject);
    }
}

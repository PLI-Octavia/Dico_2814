using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCollisionScript : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        //WordContent wordContent = col.gameObject.GetComponent<WordContent>();

        Destroy(col.gameObject);
    }
}

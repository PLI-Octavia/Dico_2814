using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCollisionScript : MonoBehaviour {

    private GameObject score;

	private void Awake()
	{
        score = GameObject.FindWithTag("Score");
	}

	// Handle collision between words and tolls
	public void OnCollisionEnter(Collision col)
    {
        Destroy(col.gameObject);
        GameManager.Instance.score += 150;

        WordObject word = col.gameObject.GetComponent<Word>().word;

        score.GetComponentInChildren<TextMesh>().text = "Score : " + GameManager.Instance.score;
    }
}

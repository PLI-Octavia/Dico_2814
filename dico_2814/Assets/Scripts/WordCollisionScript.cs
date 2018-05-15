using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCollisionScript : MonoBehaviour {

    private GameObject score;
    private GameObject wordsLeft;
    static int numberOfCollision;

	private void Awake()
	{
        score = GameObject.FindWithTag("Score");
        wordsLeft = GameObject.FindWithTag("WordsLeft");

        // Initialise the display of number of words left
        wordsLeft.GetComponentInChildren<TextMesh>().text = numberOfCollision + "/" + GameManager.Instance.numberOfWords;;
	}

	// Handle collision between words and tolls
	public void OnCollisionEnter(Collision col)
    {
        WordObject word = col.gameObject.GetComponent<Word>().word;
        numberOfCollision++;

        // If the word is correct
        if (word.word == word.correction) 
        {
            // If the correct word has collided with a good toll
            if (gameObject.tag == "LeftGoodToll" || gameObject.tag == "RightGoodToll") 
            {
                GameManager.Instance.score += 150;
                Destroy(col.gameObject);
            } 
            // If the correct word has collided with a bad toll
            else 
            {
                float direction = gameObject.tag == "RightBadToll" ? 1f : -1f;
                col.rigidbody.AddForce(new Vector3(direction, 1f, 0f) * 3000);
                Destroy(col.collider);
            }
        } 
        // If the word is incorrect
        else 
        {
            // If the incorrect word has colide with a bad toll
            if (gameObject.tag == "LeftBadToll" || gameObject.tag == "RightBadToll")
            {
                GameManager.Instance.score += 150;
                Destroy(col.gameObject);
            }
            // If the incorrect word has colide with a good toll
            else
            {
                float direction = gameObject.tag == "RightGoodToll" ? 1f : -1f;
                col.rigidbody.AddForce(new Vector3(direction, 1f, 0f) * 3000);
                Destroy(col.collider);
            }
        }

        // Update score
        score.GetComponentInChildren<TextMesh>().text = "Score\n" + GameManager.Instance.score;

        // Update words left
        wordsLeft.GetComponentInChildren<TextMesh>().text = numberOfCollision + "/" + GameManager.Instance.numberOfWords;
    }
}

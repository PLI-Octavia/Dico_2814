using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordCollisionScript : MonoBehaviour {

    public int correctionDuration = 3;

    private GameObject score;
    private GameObject wordsLeft;
    private GameObject correction;
    static int numberOfCollision;
    static int numberOfCorrectWords;

	private void Awake()
	{
        score = GameObject.FindWithTag("Score");
        wordsLeft = GameObject.FindWithTag("WordsLeft");
        correction = GameObject.FindWithTag("Correction");

        // Hide the correction at launch
        correction.GetComponent<Renderer>().enabled = false;
        correction.gameObject.transform.Find("Title").GetComponent<Renderer>().enabled = false;

        // Initialise the display of number of words left
        wordsLeft.GetComponentInChildren<TextMesh>().text = numberOfCollision + "/" + GameManager.Instance.numberOfWords;;
	}

	public void Update()
	{
        // At the end, display text.
        if (numberOfCollision == GameManager.Instance.numberOfWords) {
            correction.gameObject.transform.Find("Title").GetComponentInChildren<TextMesh>().text = "Appuie sur M\npour revenir au menu";
            correction.gameObject.transform.Find("IncorrectWord").GetComponentInChildren<TextMesh>().text = "";
            correction.gameObject.transform.Find("CorrectWord").GetComponentInChildren<TextMesh>().text = "";

            correction.SetActive(true);

            // Send to menu
            if (Input.GetKeyDown(KeyCode.M)) {
                SceneManager.LoadScene("DicoMenu", LoadSceneMode.Single);    
            }
        }
	}

	// Handle collision between words and tolls
	public void OnCollisionEnter(Collision col)
    {
        WordObject word = col.gameObject.GetComponent<Word>().word;
        numberOfCollision++;

        // Show and Disable the correction
        correction.GetComponent<Renderer>().enabled = true;
        correction.gameObject.transform.Find("Title").GetComponent<Renderer>().enabled = true;
        correction.SetActive(false);

        // If the word is correct
        if (word.word == word.correction) 
        {
            // If the correct word has collided with a good toll.
            if (gameObject.tag == "LeftGoodToll" || gameObject.tag == "RightGoodToll") 
            {
                // Increment the score.
                GameManager.Instance.score += 150;

                // Increment the number of correct words.
                numberOfCorrectWords++;

                // Play the teleport sound.
                col.gameObject.transform.Find("TeleportSound").GetComponent<AudioSource>().Play();

                // Play teleportation animation.
                col.gameObject.transform.Find("Teleportation").gameObject.SetActive(true);
                col.gameObject.transform.Find("Word").gameObject.SetActive(false);
                col.gameObject.GetComponent<Renderer>().enabled = false;
                Destroy(col.collider);
            } 
            // If the correct word has collided with a bad toll.
            else 
            {
                // Play the bounce sound.
                col.gameObject.transform.Find("BounceSound").GetComponent<AudioSource>().Play();

                // Eject bad words to right or left
                float direction = gameObject.tag == "RightBadToll" ? 1f : -1f;
                col.rigidbody.AddForce(new Vector3(direction, 1f, 0f) * 3000);
                Destroy(col.collider);

                // Pause the game after 0.5 seconds and display the correction GUI
                displayCorrection(word);
                Invoke("pause", 0.5f);
            }
        } 
        // If the word is incorrect.
        else 
        {
            // If the incorrect word has colide with a bad toll.
            if (gameObject.tag == "LeftBadToll" || gameObject.tag == "RightBadToll")
            {
                // Increment the score.
                GameManager.Instance.score += 150;

                // Increment the number of correct words.
                numberOfCorrectWords++;

                // Play the teleport sound.
                col.gameObject.transform.Find("TeleportSound").GetComponent<AudioSource>().Play();

                // Play teleportation animation.
                col.gameObject.transform.Find("Teleportation").gameObject.SetActive(true);
                col.gameObject.transform.Find("Word").gameObject.SetActive(false);
                col.gameObject.GetComponent<Renderer>().enabled = false;
                Destroy(col.collider);
            }
            // If the incorrect word has colide with a good toll.
            else
            {
                // Play the bounce sound.
                col.gameObject.transform.Find("BounceSound").GetComponent<AudioSource>().Play();

                // Eject bad words to right or left.
                float direction = gameObject.tag == "RightGoodToll" ? 1f : -1f;
                col.rigidbody.AddForce(new Vector3(direction, 1f, 0f) * 3000);
                Destroy(col.collider);

                // Pause the game after 0.5 seconds and display the correction GUI.
                displayCorrection(word);
                Invoke("pause", 0.5f);
            }
        }

        // Update score.
        score.GetComponentInChildren<TextMesh>().text = "Score\n" + GameManager.Instance.score;

        // Update words left.
        wordsLeft.GetComponentInChildren<TextMesh>().text = numberOfCorrectWords + "/" + GameManager.Instance.numberOfWords;
    }

    // Pause the game for a certain amount of time;
    void pause()
    {
        Time.timeScale = 0.1f;
        float pauseEndTime = Time.realtimeSinceStartup + correctionDuration;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            //yield return 0;
        }
        Time.timeScale = 1;
        correction.SetActive(false);
    }

    // Display something when player is wrong.
    void displayCorrection(WordObject word) {

        // If the word was incorrect.
        if (word.word != word.correction) {
            correction.gameObject.transform.Find("Title").GetComponentInChildren<TextMesh>().text = "Le mot était mauvais !";
            correction.gameObject.transform.Find("IncorrectWord").GetComponentInChildren<TextMesh>().text = word.word;
            correction.gameObject.transform.Find("CorrectWord").GetComponentInChildren<TextMesh>().text = word.correction;    
        // If the word was correct.
        } else {
            correction.gameObject.transform.Find("Title").GetComponentInChildren<TextMesh>().text = "Le mot était bon !";
            correction.gameObject.transform.Find("IncorrectWord").GetComponentInChildren<TextMesh>().text = "";
            correction.gameObject.transform.Find("CorrectWord").GetComponentInChildren<TextMesh>().text = word.word;
        }

        // Display the text.
        correction.SetActive(true);
    }
}

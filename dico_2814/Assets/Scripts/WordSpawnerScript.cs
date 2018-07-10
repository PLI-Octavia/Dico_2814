using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class WordSpawnerScript : MonoBehaviour {

    // Public
    public GameObject word;

    // Private
    private float[] wordPositions = {-6.5f, -3.5f, 3.5f, 6.5f};
    private WordObject[] words;
    private int numberOfWords;

    [DllImport("__Internal")]
    private static extern string GetConfig();

    // Keep this line for tests
    //private string json = "{\"words\":[{\"word\":\"Bonjours\",\"correction\":\"Bonjour\"},{\"word\":\"Pomme\",\"correction\":\"Pomme\"},{\"word\":\"Chateau\",\"correction\":\"Chatau\"},{\"word\":\"vraix\",\"correction\":\"vrai\"},{\"word\":\"toujour\",\"correction\":\"toujours\"}],\"limit\":\"2\"}";
 
    // Load words from external json and spawn them repeatedly.
    void Awake()
    {
        // TODO: Load from jsonConfig.
        string json = GetConfig();
        Debug.Log(GetConfig());
        words = JsonHelper.FromJson<WordObject>(json);

        // Get the limit send by the json and save it in GameManager
        GameManager.Instance.numberOfWords = int.Parse(JsonUtility.FromJson<Limit>(json).limit);

        // Spawn words repeatedly.
        InvokeRepeating("SpawnWord", 0.5f, 3f);

        // Store the number of words needed locally.
        numberOfWords = GameManager.Instance.numberOfWords;
    }

	private void Update()
	{
        // Stop spawning words if the number of words is reached.
        if (numberOfWords == 0) {
            CancelInvoke();
        }
	}

	// Spawn a random word at a random position and save the word inside the object.
	void SpawnWord()
    {
        // Randoms.
        int randomPosition = Random.Range(0, 4);
        int randomRange = Random.Range(0, words.Length);

        // Objects.
        GameObject newWord = Instantiate(word);
        WordObject randomWord = words[randomRange];
        Word wordContent = newWord.GetComponent<Word>();

        // Position the new word.
        newWord.transform.position = new Vector3(wordPositions[randomPosition], 
                                                 5.5f, 0);

        // Save the wordObject inside the object.
        wordContent.word = randomWord;

        // Display text.
        newWord.GetComponentInChildren<TextMesh>().text = randomWord.word;

        numberOfWords -= 1;
    }
}

// This class is used to serialize a json array, which is not possible with the native JsonUtility class.
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.words;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.words = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.words = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] words;
    }
}
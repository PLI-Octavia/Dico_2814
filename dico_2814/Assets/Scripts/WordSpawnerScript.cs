using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class WordSpawnerScript : MonoBehaviour {

    public GameObject word;

    private float[] wordPositions = {-6.5f, -3.5f, 3.5f, 6.5f};
    private Word[] words;

    void Awake()
    {
        InvokeRepeating("SpawnNext", 0.5f, 3f);

        string path = "Assets/Data/words.json";

        StreamReader reader = new StreamReader(path);
        string jsonString = reader.ReadToEnd();
        reader.Close();

        words = JsonHelper.FromJson<Word>("{\"Items\":" + jsonString + "}");
        //words = JsonUtility.FromJson<WordContent>(jsonString);

        Debug.Log(words[0].correction);
    }

    void SpawnNext()
    {
        GameObject newWord = Instantiate(word);
        int randomPosition = Random.Range(0, 4);

        newWord.transform.position = new Vector3(wordPositions[randomPosition], 
                                                 5.5f, 0);

        //WordContent wordContent = newWord.GetComponent<WordContent>();

        //wordContent.id = Random.Range(0, 100);
        //wordContent.word = "hellu";
        //wordContent.correction = "hello";
        //wordContent.isCorrect = false;

        //newWord.GetComponentInChildren<TextMesh>().text = wordContent.word;
    }
}


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawnerScript : MonoBehaviour {

    public GameObject word;

    private float[] wordPositions = {-6.5f, -3.5f, 3.5f, 6.5f};

    void Awake()
    {
        InvokeRepeating("SpawnNext", 0.5f, 0.5f);
    }

    void SpawnNext()
    {
        GameObject newWord = Instantiate(word);
        int random = Random.Range(0, 4);
        Debug.Log(random);
        newWord.transform.position = new Vector3(wordPositions[random], 5.5f, 0);
    }
}

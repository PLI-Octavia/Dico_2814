using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // This field can be accessed through our singleton instance,
    // but it can't be set in the inspector, because we use lazy instantiation
    public int life = 3;
    public int score = 0;
    public int numberOfWords = 1;

    // Static singleton instance
    private static GameManager instance;

    // Static singleton property
    public static GameManager Instance
    {
        // Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
        // otherwise we assign instance to a new component and return that
        get { return instance ?? (instance = new GameObject("Singleton").AddComponent<GameManager>()); }
    }
}

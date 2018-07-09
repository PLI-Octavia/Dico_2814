using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WordObject to serialize the json.
[System.Serializable]
public class WordObject
{
    public string word;
    public string correction;
}

// Simple object to parse the limit (number of words) from the Json
[System.Serializable]
public class Limit
{
    public string limit;
}
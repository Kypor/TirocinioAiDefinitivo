using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JapaneseIdeoArray", menuName = "Scriptable Objects/JapaneseIdeoArray")]
public class JapaneseIdeoArray : ScriptableObject
{
    public List<ListWrapper> ideos;
    public List<Sprite> numbers;
}
[System.Serializable]
public class ListWrapper  
{
    public string word;
    public List<Sprite> ideosInWord;
}

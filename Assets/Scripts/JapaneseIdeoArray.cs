using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JapaneseIdeoArray", menuName = "Scriptable Objects/JapaneseIdeoArray")]
public class JapaneseIdeoArray : ScriptableObject
{
    public List<ListWrapper> ideos;
     public List<Sprite> ideosPartitions;
}
[System.Serializable]
public class ListWrapper  
{
    public List<Sprite> ideosPartitions;
}

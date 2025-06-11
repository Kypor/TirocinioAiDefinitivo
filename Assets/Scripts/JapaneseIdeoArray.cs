using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JapaneseIdeoArray", menuName = "Scriptable Objects/JapaneseIdeoArray")]
[System.Serializable]
public class ListWrapper
{
    public List<Sprite> ideosPartitions;
}
public class JapaneseIdeoArray : ScriptableObject
{
    public List<ListWrapper> ideos;

}

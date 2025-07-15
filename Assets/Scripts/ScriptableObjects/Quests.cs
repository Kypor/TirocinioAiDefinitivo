using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Scriptable Objects/Quests")]
public class Quests : ScriptableObject
{
    public List<Quest> quests = new List<Quest>();
}
[System.Serializable]
public class Quest
{
    public string questID;
    public string Description;
}

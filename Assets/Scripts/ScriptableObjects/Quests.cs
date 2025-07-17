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
    public int questID;
    public string description;

    public string requiredVerb;
    public string requiredNoun;

    public bool isCompleted;

    public bool CheckCondition(string currentVerb, string currentNoun)
    {
        // Se il noun non è richiesto, controlla solo il verbo
        if (string.IsNullOrEmpty(requiredNoun))
        {
            return !isCompleted && currentVerb == requiredVerb;
        }
        else
        {
            return !isCompleted && currentVerb == requiredVerb && currentNoun == requiredNoun;
        }
    }
}

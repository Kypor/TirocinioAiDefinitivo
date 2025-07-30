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
        
        
            if (string.IsNullOrEmpty(requiredNoun))
            {
                // Se il noun e il numero non sono richiesti, controlla solo il verbo
                Debug.Log("caso solo verbo");
                return !isCompleted && currentVerb == requiredVerb;
            }
            else
            {
                Debug.Log("caso  verbo e noun");

                return !isCompleted && currentVerb == requiredVerb && currentNoun == requiredNoun;
            }
        
        
        
    
        
    }
}

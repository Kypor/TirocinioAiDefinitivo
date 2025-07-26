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
    public string requiredNumber;
    public bool isCompleted;

    public bool CheckCondition(string currentVerb, string currentNoun, string currentNumber)
    {
        if (string.IsNullOrEmpty(requiredNumber))
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
        else
        {
            if (string.IsNullOrEmpty(requiredNoun))
            {
                // Se il noun non è richiesto, controlla verbo e numero 
                Debug.Log("caso  verbo e numero");

                return !isCompleted && currentVerb == requiredVerb && currentNumber == requiredNumber;
            }
                            Debug.Log("caso  tutto");

            return !isCompleted && currentVerb == requiredVerb && currentNoun == requiredNoun && currentNumber == requiredNumber;
        }
    }
}

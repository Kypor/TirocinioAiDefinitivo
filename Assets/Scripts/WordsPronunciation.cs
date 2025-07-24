using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordsPronunciation", menuName = "Scriptable Objects/WordsPronunciation")]
public class WordsPronunciation : ScriptableObject
{
    public List<AudioClip> pronunciation;
}

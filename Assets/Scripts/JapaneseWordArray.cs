using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JapaneseWordArray", menuName = "Scriptable Objects/JapaneseWordArray")]
public class JapaneseWordArray : ScriptableObject
{
    public List<PronunciaEntry> paroleConPronunce;
}

[System.Serializable]
public class PronunciaEntry
{
    public List<string> pronunce; // Max 3 elementi
}

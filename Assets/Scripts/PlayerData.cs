using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor.Rendering;
using UnityEngine;

[Serializable]
public class ErrorRatios
{
    // da vedere se mettere float (motivi di grafico)
    public float lv1To1;
    public float lv1To2;
    public float lv2To1;
    public float lv2To2;
    public float lv3To1;
    public float lv3To2;

}

[Serializable]
public class ErrorCounts
{
    public Dictionary<string, int> wordsErrorCountT1 = new();
    public Dictionary<string, int> wordsErrorCountT2 = new();
    public Dictionary<string, int> ideoErrorCountT1 = new();
    public Dictionary<string, int> ideoErrorCountT2 = new();
    public Dictionary<string, int> questErrorCountT1 = new();
    public Dictionary<string, int> questErrorCountT2 = new();

}


[Serializable]
public class PlayerDataList
{
    public List<PlayerData> players = new();
}


[Serializable]
public class PlayerData
{
    public readonly string pcId;
    public string name;
    public float pointsLv1To1 = 0f;
    public float pointsLv1to2 = 0f;
    public float pointsLv2To1 = 0f;
    public float pointsLv2To2 = 0f;
    public ErrorRatios errorRatios;
    public ErrorCounts errorCounts;

    public PlayerData(string text)
    {
        pcId = Environment.MachineName;
        name = text;
        errorRatios = new();
        errorCounts = new();

    }
}

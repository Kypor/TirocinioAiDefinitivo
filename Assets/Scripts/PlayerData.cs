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
    public List<float> lv1To1 = new();
    public List<float> lv1To2 = new();
    public List<float> lv2To1 = new();
    public List<float> lv2To2 = new();
    public List<float> lv3To1 = new();
    public List<float> lv3To2 = new();

}

[Serializable]
public class ErrorCounts
{
    public Dictionary<string, List<int>> wordsErrorCountT1 = new();
    public Dictionary<string, List<int>> wordsErrorCountT2 = new();
    public Dictionary<string, List<int>> ideoErrorCountT1 = new();
    public Dictionary<string, List<int>> ideoErrorCountT2 = new();
    public Dictionary<string, List<int>> questErrorCountT1 = new();
    public Dictionary<string, List<int>> questErrorCountT2 = new();

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

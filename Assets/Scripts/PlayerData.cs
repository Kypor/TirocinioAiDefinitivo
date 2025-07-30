using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Rendering;
using UnityEngine;
[Serializable]
public class ErrorRatios
{
    public List<float> lv1To1 = new List<float>();
    public List<float> lv1To2 = new List<float>();
    public List<float> lv2To1 = new List<float>();
    public List<float> lv2To2 = new List<float>();
    public List<float> lv3To1 = new List<float>();
    public List<float> lv3To2 = new List<float>();

}
[Serializable]
public class PlayerDataList
{
    public List<PlayerData> players = new List<PlayerData>();
}
[Serializable]
public class PlayerData
{
    public string name;
    public ErrorRatios errorRatios;

    public PlayerData(string text)
    {
        this.name = text;
    }

    public void AddRatio(int level, int topic, float ratio)
    {
        if (level == 1 && topic == 1) errorRatios.lv1To1.Add(ratio);
        else if (level == 1 && topic == 2) errorRatios.lv1To2.Add(ratio);
        else if (level == 2 && topic == 1) errorRatios.lv2To1.Add(ratio);
        else if (level == 2 && topic == 2) errorRatios.lv2To2.Add(ratio);
        else if (level == 3 && topic == 1) errorRatios.lv3To1.Add(ratio);
        else if (level == 3 && topic == 2) errorRatios.lv3To2.Add(ratio);
        Debug.Log("ratio addedd");
    }
}

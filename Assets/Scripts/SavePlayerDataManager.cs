using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;

public class SavePlayerDataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerData currentPlayerData;
    [SerializeField]
    private TMP_InputField tMP_InputField;
    [SerializeField]
    private TMP_Dropdown savesDropdown;
    private static PlayerDataList playersList;
    private List<string> nameSaved = new();
    [SerializeField]
    private TextMeshProUGUI debugText;
    private static readonly string path = "C:/Users/diego/Desktop/build tirocinio" + "/playerDataList.json";
    void Start()
    {
        tMP_InputField.onEndEdit.AddListener(delegate { AddNewPlayer(); });
        savesDropdown.ClearOptions();
        savesDropdown.AddOptions(LoadSaveDataNames());
        savesDropdown.onValueChanged.AddListener(ChangeSave);

    }
    List<string> LoadSaveDataNames()
    {
        //string path = Application.persistentDataPath + "/playerDataList.json";
        //string path = "C:/Users/diego/Desktop/build tirocinio" + "/playerDataList.json";
        if (!File.Exists(path))
        {
            string text = "{ \"players\": [] }";
            File.Create(path).Dispose();
            File.WriteAllText(path, text);
        }
        string json = File.ReadAllText(path);
        playersList = JsonConvert.DeserializeObject<PlayerDataList>(json);
        nameSaved.Clear();
        foreach (var player in playersList.players)
        {
            nameSaved.Add(player.name);
        }
        if (nameSaved.Count == 0)
        {
            Debug.LogWarning("Nessun file di salvataggio trovato.");
            debugText.text = "Nessun file di salvataggio trovato.";
        }

        return nameSaved;
    }

    public void ChangeSave(int index)
    {
        currentPlayerData = playersList.players[index];
        Debug.Log("✅ Salvataggio corrente impostato: " + currentPlayerData.name);
        debugText.text = "Salvataggio corrente impostato: " + currentPlayerData.name;
    }
    public void AddNewPlayer()
    {
        if (tMP_InputField.text == "")
        {
            return;
        }
        //string path = Application.persistentDataPath + "/playerDataList.json";
        //string path = "C:/Users/diego/Desktop/build tirocinio" + "/playerDataList.json";
        // Se il file esiste, carica i dati esistenti
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playersList = JsonConvert.DeserializeObject<PlayerDataList>(json);
            bool existingName = playersList.players.Exists(p => p.name == tMP_InputField.text);
            if (existingName)
            {
                Debug.LogWarning(" Nome già esistente: " + tMP_InputField.text);
                debugText.text = " Nome già esistente: " + tMP_InputField.text;
                return;
            }
        }
        // Controllo duplicato

        // Aggiungi nuovo player e salva
        PlayerData NewPlayer = new(tMP_InputField.text);
        playersList.players.Add(NewPlayer);
        string nuovoJson = JsonConvert.SerializeObject(playersList, Formatting.Indented); ;
        File.WriteAllText(path, nuovoJson);
        Debug.Log(" Player salvato con successo: " + tMP_InputField.text);
        debugText.text = " Player salvato con successo: " + tMP_InputField.text;
        savesDropdown.ClearOptions();
        savesDropdown.AddOptions(LoadSaveDataNames());
        ChangeSave(nameSaved.Count - 1);
    }
    public void DeleteSave()
    {
        //string path = Application.persistentDataPath + "/playerDataList.json";
        //string path = "C:/Users/diego/Desktop/build tirocinio" + "/playerDataList.json";
        if (File.Exists(path))
        {
            if (savesDropdown.value >= 0 && savesDropdown.value < playersList.players.Count)
            {
                Debug.Log("Eliminato: " + playersList.players[savesDropdown.value].name);
                debugText.text = "Eliminato: " + playersList.players[savesDropdown.value].name;
                playersList.players.RemoveAt(savesDropdown.value);

                string nuovoJson = JsonConvert.SerializeObject(playersList, Formatting.Indented);
                File.WriteAllText(path, nuovoJson);

                // Aggiorna il dropdown
                savesDropdown.ClearOptions();
                savesDropdown.AddOptions(LoadSaveDataNames());
            }
            else
            {
                Debug.LogWarning("Indice non valido.");

            }
        }
    }
    /// <summary>
    /// funzione che aggiunge l'error ratio al salvataggio corrente 
    /// </summary>
    /// <param name="level">numero livello da 1 a 3</param>
    /// <param name="topic">numero topic da 1 a 2</param>
    /// <param name="ratio">valore di errore calcolato</param>
    public static void AddRatio(int level, int topic, float ratio)
    {
        if (level == 1 && topic == 1) currentPlayerData.errorRatios.lv1To1.Add(ratio);
        else if (level == 1 && topic == 2) currentPlayerData.errorRatios.lv1To2.Add(ratio);
        else if (level == 2 && topic == 1) currentPlayerData.errorRatios.lv2To1.Add(ratio);
        else if (level == 2 && topic == 2) currentPlayerData.errorRatios.lv2To2.Add(ratio);
        else if (level == 3 && topic == 1) currentPlayerData.errorRatios.lv3To1.Add(ratio);
        else if (level == 3 && topic == 2) currentPlayerData.errorRatios.lv3To2.Add(ratio);
        Debug.Log("ratio addedd");
        string nuovoJson = JsonConvert.SerializeObject(playersList, Formatting.Indented);
        File.WriteAllText(path, nuovoJson);
    }
    /// <summary>
    /// funzione che aggiunge il contantore di volte in cui è stata sbagliata una determinata chiave al salvataggio corrente
    /// </summary>
    /// <param name="level">numero livello da 1 a 3</param>
    /// <param name="topic">numero topic da 1 a 2</param>
    /// <param name="key">chiave ossia la parola, ideogramma, o quest</param>
    /// <param name="count">numero di volte dello sbaglio della chiave</param>
    public static void AddErrorCount(int level, int topic, string key, int count)
    {
        List<int> targetList = null;

        if (level == 1 && topic == 1)
            targetList = GetOrCreate(currentPlayerData.errorCounts.wordsErrorCountT1, key);
        else if (level == 1 && topic == 2)
            targetList = GetOrCreate(currentPlayerData.errorCounts.wordsErrorCountT2, key);
        else if (level == 2 && topic == 1)
            targetList = GetOrCreate(currentPlayerData.errorCounts.ideoErrorCountT1, key);
        else if (level == 2 && topic == 2)
            targetList = GetOrCreate(currentPlayerData.errorCounts.ideoErrorCountT2, key);
        else if (level == 3 && topic == 1)
            targetList = GetOrCreate(currentPlayerData.errorCounts.questErrorCountT1, key);
        else if (level == 3 && topic == 2)
            targetList = GetOrCreate(currentPlayerData.errorCounts.questErrorCountT2, key);

        targetList?.Add(count);

        Debug.Log("error count added");

        string nuovoJson = JsonConvert.SerializeObject(playersList, Formatting.Indented);
        File.WriteAllText(path, nuovoJson);
    }

    // Helper per creare la lista se non esiste
    private static List<int> GetOrCreate(Dictionary<string, List<int>> diz, string key)
    {
        if (!diz.ContainsKey(key))
            diz[key] = new List<int>();
        return diz[key];
    }

}

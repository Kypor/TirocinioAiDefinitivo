using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SavePlayerDataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerData currentPlayerData;
    [SerializeField]
    private TMP_InputField tMP_InputField;
    [SerializeField]
    private TMP_Dropdown savesDropdown;
    private static PlayerDataList playersList;
    private  List<string> nameSaved = new();
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
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playersList = JsonUtility.FromJson<PlayerDataList>(json);
            nameSaved.Clear();
            foreach (var player in playersList.players)
            {
                nameSaved.Add(player.name);
            }
        }
        else
        {
            Debug.LogWarning("Nessun file di salvataggio trovato.");
            debugText.text = "Nessun file di salvataggio trovato.";
        }
        return nameSaved;
    }
    public void ChangeSave(int index)
    {

        SavePlayerDataManager.currentPlayerData = playersList.players[index];
        Debug.Log("✅ Salvataggio corrente impostato: " + SavePlayerDataManager.currentPlayerData.name);
        debugText.text = "Salvataggio corrente impostato: " + SavePlayerDataManager.currentPlayerData.name;
    }
    public void AddNewPlayer()
    {
        if (tMP_InputField.text == "")
        {
            return;
        }
        //string path = Application.persistentDataPath + "/playerDataList.json";
        //string path = "C:/Users/diego/Desktop/build tirocinio" + "/playerDataList.json";
        PlayerDataList listaGiocatori = new();

        // Se il file esiste, carica i dati esistenti
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playersList = JsonUtility.FromJson<PlayerDataList>(json);
        }

        // Controllo duplicato
        bool existingName = playersList.players.Exists(p => p.name == tMP_InputField.text);
        if (existingName)
        {
            Debug.LogWarning(" Nome già esistente: " + tMP_InputField.text);
            debugText.text = " Nome già esistente: " + tMP_InputField.text;
            return;
        }

        // Aggiungi nuovo player e salva
        PlayerData NewPlayer = new(tMP_InputField.text);
        playersList.players.Add(NewPlayer);
        string nuovoJson = JsonUtility.ToJson(playersList, true);
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

                string nuovoJson = JsonUtility.ToJson(playersList, true);
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
    public static void AddRatio(int level, int topic, float ratio)
    {
        if (level == 1 && topic == 1) currentPlayerData.errorRatios.lv1To1.Add(ratio);
        else if (level == 1 && topic == 2) currentPlayerData.errorRatios.lv1To2.Add(ratio);
        else if (level == 2 && topic == 1) currentPlayerData.errorRatios.lv2To1.Add(ratio);
        else if (level == 2 && topic == 2) currentPlayerData.errorRatios.lv2To2.Add(ratio);
        else if (level == 3 && topic == 1) currentPlayerData.errorRatios.lv3To1.Add(ratio);
        else if (level == 3 && topic == 2) currentPlayerData.errorRatios.lv3To2.Add(ratio);
        Debug.Log("ratio addedd");
        string nuovoJson = JsonUtility.ToJson(playersList, true);
        File.WriteAllText(path, nuovoJson);
    }
}

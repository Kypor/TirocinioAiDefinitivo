using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawRandomIdeo : MonoBehaviour
{
    [SerializeField]
    private JapaneseIdeoArray japaneseIdeoArray;
    public Image partitionIdeoImage, ideoImage;
    private ListWrapper currentIdeo;
    private int currentNumberIndex = 0, currentIdeoIndex = 0, wordsCount = 0, errorCount = 0;
    // public int numberOfWords = 1;
    public Image errorIndicator;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI ideoName;
    private CountdownTimer timer;
    private PointsManagerScript pointsManagerScript;

    void Start()
    {
        timer = GetComponent<CountdownTimer>();
        ideoImage.sprite = GetRandomIdeo();
        pointsManagerScript = GetComponent<PointsManagerScript>();
    }

    // private Sprite GetRandomIdeo()
    // {
    //     if (wordsCount < numberOfWords)
    //     {
    //         int randomWord = Random.Range(0, japaneseIdeoArray.ideos.Count);
    //         currentIdeo = japaneseIdeoArray.ideos[randomWord];

    //         japaneseIdeoArray.ideos.RemoveAt(randomWord);

    //         text.text = "Write the word : " + currentIdeo.word;
    //         return currentIdeo.ideosInWord[0];
    //     }
    //     timer.StopTimer();
    //     return null;
    // }

    private Sprite GetRandomIdeo()
    {
        if (wordsCount < japaneseIdeoArray.ideos.Count)
        {
            //int randomIndex = UnityEngine.Random.Range(0, availableIdeos.Count);
            currentIdeo = japaneseIdeoArray.ideos[wordsCount];

            //ideoName.text = currentIdeo.ideosInWord[currentNumberIndex].name;
            ideoName.text = currentIdeo.ideosInWord[0].name.Split("-")[1];

            text.text = "Write the word: " + "'" + currentIdeo.word + "'" + "\n in japanese " + "\nTranslation: " + currentIdeo.traduzione;

            return currentIdeo.ideosInWord[0];
        }

        timer.StopTimer();
        //pointsManagerScript.AddPoints(2);
        StartCoroutine(pointsManagerScript.ShowResults());

        return null;
    }

    public void ToNextIdeosPartition(int recognizedIndex)
    {
        if (CheckIdeo(recognizedIndex))
        {
            if (currentNumberIndex < currentIdeo.ideosInWord.Count - 1)
            {
                currentNumberIndex++;
                ideoImage.sprite = currentIdeo.ideosInWord[currentNumberIndex];
                //ideoName.text = currentIdeo.ideosInWord[currentNumberIndex].name;
                ideoName.text = currentIdeo.ideosInWord[currentNumberIndex].name.Split("-")[1];
                //Debug.Log("cambio");
                DrawWithMouse.DestroyLines();
            }
            else
            {
                ResetIdeo();
                DrawWithMouse.DestroyLines();
                Debug.Log("resettato");
            }
        }
    }

    private void ResetIdeo()
    {
        wordsCount++;
        ideoImage.sprite = GetRandomIdeo();
        currentNumberIndex = 0;
        pointsManagerScript.AddPoints(2);
    }

    private bool CheckIdeo(int recognizedIndex)
    {
        currentIdeoIndex = int.Parse(ideoImage.sprite.name[..2]);
        Debug.Log("indice immagine: " + currentIdeoIndex);
        if (currentIdeoIndex == recognizedIndex)
        {

            errorIndicator.color = new Color(0, 1, 0, 1f);
            SoundManager.instance.PlaySoundFX(1);
            Debug.Log("bravo");
            SavePlayerDataManager.AddErrorCount(2, MainMenuManager.topicChosen, ideoName.text, errorCount);
            errorCount = 0;
            return true;
        }
        errorIndicator.color = new Color(1, 0, 0, 1f);
        SoundManager.instance.PlaySoundFX(2);
        Debug.Log("sbagliato");
        pointsManagerScript.SubPoints(2);
        errorCount++;
        return false;
    }
}
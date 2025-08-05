using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawRandomIdeo : MonoBehaviour
{

    public JapaneseIdeoArray japaneseIdeoArray;
    public Image partitionIdeoImage, ideoImage;
    private ListWrapper currentIdeo;
    private int currentNumberIndex = 0, currentIdeoIndex = 0, wordsCount = 0, errorCount = 0, currentIdeoLength = 0;
    // public int numberOfWords = 1;
    public Image errorIndicator;
    [SerializeField]
    private TextMeshProUGUI wordText, translationText;
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

            wordText.text = currentIdeo.word;
            translationText.text = "Translation: " + currentIdeo.traduzione;

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
                currentIdeoLength += ideoName.text.Length;
                string newText = currentIdeo.word[..(currentIdeoLength-1)];
                string[] fullWord = currentIdeo.word.Split(newText, 2, System.StringSplitOptions.None);
                Debug.Log(currentIdeoLength);
                Debug.Log(" new text " + newText);
                Debug.Log(fullWord[0]);
                Debug.Log(fullWord[1]);

                wordText.text = "<color=green>" + newText + "</color>" + fullWord[1];
                currentNumberIndex++;
                ideoImage.sprite = currentIdeo.ideosInWord[currentNumberIndex];

                ideoName.text = currentIdeo.ideosInWord[currentNumberIndex].name.Split("- ")[1];
                //Debug.Log("cambio");
                DrawWithMouse.DestroyLines();
            }
            else
            {
                currentIdeoLength = 0;
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
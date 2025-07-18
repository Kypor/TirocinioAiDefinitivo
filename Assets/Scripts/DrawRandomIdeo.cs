using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawRandomIdeo : MonoBehaviour
{
    [SerializeField]
    private JapaneseIdeoArray japaneseIdeoArray;
    public Image partitionIdeoImage, ideoImage;
    private ListWrapper currentIdeo;
    private int currentNumberIndex = 0, currentIdeoIndex = 0, WordsCount = 0;
    public int numberOfWords = 1;
    public Image errorIndicator;
    [SerializeField]
    private TextMeshProUGUI text;
    private CountdownTimer timer;
    private PointsManagerScript pointsManagerScript;

    void Start()
    {
        timer = GetComponent<CountdownTimer>();
        ideoImage.sprite = GetRandomIdeo();
        pointsManagerScript = GameObject.Find("GameManager").GetComponent<PointsManagerScript>();
    }

    private Sprite GetRandomIdeo()
    {
        if (WordsCount < numberOfWords)
        {
            int randomWord = Random.Range(0, japaneseIdeoArray.ideos.Count);
            currentIdeo = japaneseIdeoArray.ideos[randomWord];
            text.text = "Write the word : " + currentIdeo.word;
            return currentIdeo.ideosInWord[0];
        }
        timer.StopTimer();
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
                //Debug.Log("cambio");

            }
            else
            {
                ResetIdeo();
                Debug.Log("resettato");
            }
        }
    }

    private void ResetIdeo()
    {
        WordsCount++;
        ideoImage.sprite = GetRandomIdeo();
        currentNumberIndex = 0;
        //add point;
        pointsManagerScript.AddPoints();
    }
    
    private bool CheckIdeo(int recognizedIndex)
    {
        currentIdeoIndex = int.Parse(ideoImage.sprite.name.Substring(0, 2));
        Debug.Log("indice immagine: " + currentIdeoIndex);
        if (currentIdeoIndex == recognizedIndex)
        {
            errorIndicator.color = new Color(0, 1, 0, 1f);
            Debug.Log("bravo");
            return true;
        }
        errorIndicator.color = new Color(1, 0, 0, 1f);
        Debug.Log("sbagliato");
        //sub points
        pointsManagerScript.SubPoints();
        return false;
    }
}
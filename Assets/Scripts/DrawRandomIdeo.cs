using UnityEngine;
using UnityEngine.UI;

public class DrawRandomIdeo : MonoBehaviour
{
    [SerializeField]
    private JapaneseIdeoArray japaneseIdeoArray;
    public Image partitionIdeoImage, fullIdeoImage;
    private ListWrapper currentIdeo;
    private int currentNumberIndex = 1, currentIdeoIndex = 0;
    public Image errorIndicator;
    void Start()
    {
        fullIdeoImage.sprite = GetRandomIdeo();
        partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentNumberIndex];
    }
    private Sprite GetRandomIdeo()
    {
        currentIdeoIndex = Random.Range(0, japaneseIdeoArray.ideos.Count);
        currentIdeo = japaneseIdeoArray.ideos[currentIdeoIndex];
        return currentIdeo.ideosPartitions[0];

    }
    public void ToNextIdeosPartition(int recognizedIndex)
    {
        if (currentNumberIndex < currentIdeo.ideosPartitions.Count - 1)
        {
            currentNumberIndex++;
            partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentNumberIndex];

        }
        else
        {
            if (CheckIdeo(recognizedIndex))
            {
                ResetIdeo();
            }
        }
    }
    private void ResetIdeo()
    {
        fullIdeoImage.sprite = GetRandomIdeo();
        currentNumberIndex = 1;
        partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentNumberIndex];

    }
    private bool CheckIdeo(int recognizedIndex)
    {
        if (currentIdeoIndex == recognizedIndex)
        {
            errorIndicator.color = new Color(0, 1, 0, 1f);
            Debug.Log("bravo");
            return true;
        }
        errorIndicator.color = new Color(1, 0, 0, 1f);
        Debug.Log("sbagliato");
        return false;
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawRandomIdeo : MonoBehaviour
{
    [SerializeField]
    private JapaneseIdeoArray japaneseIdeoArray;
    [SerializeField]
    public Image partitionIdeoImage, fullIdeoImage;
    private ListWrapper currentIdeo;
    private int currentNumberIndex = 0, currentRandomNumberIndex = 0;
    [SerializeField]
    TextMeshProUGUI text;
    public List<string> numbersIdeo = new List<string>();
    public List<int> uniqueRandomNumbers = new List<int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //fullIdeoImage.sprite = GetRandomIdeo();
        //partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentNumberIndex];
        numbersIdeo.Add("ゼロ");
        numbersIdeo.Add("一");
        numbersIdeo.Add("二");
        numbersIdeo.Add("三");
        numbersIdeo.Add("四");
        numbersIdeo.Add("五");
        numbersIdeo.Add("六 ");
        numbersIdeo.Add("七");
        numbersIdeo.Add("八");
        numbersIdeo.Add("九");
        text.text = PrintText();
        uniqueRandomNumbers = GenerateUniqueRandomNumbers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            text.text = PrintText();
        }
    }
    private Sprite GetRandomIdeo()
    {
        currentIdeo = japaneseIdeoArray.ideos[Random.Range(0, japaneseIdeoArray.ideos.Count)];
        return currentIdeo.ideosPartitions[0];

    }
    // return true se sono finiti tutte le partizioni fa il controllo e resetta con un nuovo ideogramma
    public bool ToNextIdeosPartition()
    {
        if (currentNumberIndex < currentIdeo.ideosPartitions.Count - 1)
        {
            currentNumberIndex++;
            partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentNumberIndex];
            return false;
        }
        else
        {
            //controllo se è l'ideogramma è giusto
            Debug.Log("controllo se è l'ideogramma è giusto");
            ResetIdeo();
            return true;

        }
    }
    private void ResetIdeo()
    {
        fullIdeoImage.sprite = GetRandomIdeo();
        currentNumberIndex = 1;
        partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentNumberIndex];

    }
    private string PrintText()
    {
        currentNumberIndex++;
        if (currentNumberIndex <= 10)
        {
            fullIdeoImage.sprite = japaneseIdeoArray.ideosPartitions[currentNumberIndex - 1];
            return "draw the number " + (currentNumberIndex - 1);

        }
        if (currentNumberIndex <= 20)
        {
            fullIdeoImage.sprite = japaneseIdeoArray.ideosPartitions[currentNumberIndex - 11];
            return "draw the number " + numbersIdeo[currentNumberIndex - 11];
        }
        currentRandomNumberIndex++;
        if (currentRandomNumberIndex <= 10)
        {
            return "draw the number " + numbersIdeo[uniqueRandomNumbers[currentRandomNumberIndex - 1]];

        }
        return "null";
    }
    public void ChangeTextCheck(int number)
    {

        if (number == currentNumberIndex - 1 || number == currentNumberIndex - 11)
        {
            text.text = PrintText();
            Debug.Log("testo cambiato");
        }
        else
        {
            if (currentRandomNumberIndex - 1 >= 0 && number == uniqueRandomNumbers[currentRandomNumberIndex - 1])
            {
                text.text = PrintText();
                Debug.Log("testo cambiato");
            }
            else
            {
                Debug.Log("number: "+ number);
                Debug.Log("random index: "+ currentRandomNumberIndex);
                Debug.Log("testo NON cambiato");
            }
        }
    }
    List<int> GenerateUniqueRandomNumbers()
    {
        List<int> pool = new List<int>();
        for (int i = 0; i <= 9; i++)
        {
            pool.Add(i);
        }

        List<int> result = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }

        return result;
    }

}


using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawNumberManager : MonoBehaviour
{
    [SerializeField]
    private JapaneseIdeoArray japaneseIdeoArray;
    [SerializeField]
    public Image fullIdeoImage;
    private int currentNumberIndex = 0, currentRandomNumberIndex = 0;
    [SerializeField]
    TextMeshProUGUI text;
    public List<string> numbersIdeo = new();
    public List<int> uniqueRandomNumbers = new();
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
                Debug.Log("number: " + number);
                Debug.Log("random index: " + currentRandomNumberIndex);
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

        List<int> result = new();
        for (int i = 0; i < 9; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }

        return result;
    }
}

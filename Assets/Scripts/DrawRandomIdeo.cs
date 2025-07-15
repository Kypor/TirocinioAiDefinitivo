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
    private int currentIndex = 0;
    [SerializeField]
    TextMeshProUGUI text;
    public List<string> numbersIdeo = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //fullIdeoImage.sprite = GetRandomIdeo();
        //partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentIndex];
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
        if (currentIndex < currentIdeo.ideosPartitions.Count - 1)
        {
            currentIndex++;
            partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentIndex];
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
        currentIndex = 1;
        partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentIndex];

    }
    private string PrintText()
    {
        currentIndex++;
        if (currentIndex <= 10)
        {
            fullIdeoImage.sprite = japaneseIdeoArray.ideosPartitions[currentIndex - 1];
            return "draw the number " + (currentIndex - 1);

        }
        if (currentIndex <= 20)
        {
            fullIdeoImage.sprite = japaneseIdeoArray.ideosPartitions[currentIndex - 11];
            return "draw the number " + numbersIdeo[currentIndex - 11];
        }
        return "null";
    }
    public void ChangeTextCheck(int number)
    {

        if (number == currentIndex - 1 || number == currentIndex - 11)
        {
            text.text = PrintText();
            Debug.Log("testo cambiato");
        }
        else
        {
            Debug.Log("testo NON cambiato");
        }
    }

}


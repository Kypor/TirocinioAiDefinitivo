using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawRandomIdeo : MonoBehaviour
{
    [SerializeField]
    private JapaneseIdeoArray japaneseIdeoArray;
    [SerializeField]
    public Image partitionIdeoImage, fullIdeoImage;
    private ListWrapper currentIdeo;
    private int currentIdex = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullIdeoImage.sprite = GetRandomIdeo();
        partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentIdex];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetIdeo();
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
        if (currentIdex < currentIdeo.ideosPartitions.Count - 1)
        {
            currentIdex++;
            partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentIdex];
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
        currentIdex = 1;
        partitionIdeoImage.sprite = currentIdeo.ideosPartitions[currentIdex];

    }

}


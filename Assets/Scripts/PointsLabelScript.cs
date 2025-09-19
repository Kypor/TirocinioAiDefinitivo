using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsLabelScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI firstLevelPoints, secondLevelPoints, thirdLevelPoints, fistLevelMinPoints, secondLevelMinPoints, thirdLevelMinPoints;
    [SerializeField]
    GameObject holderLevel1, holderLevel2, holderLevel3;
    public static int starsLv1 = 0, starsLv2 = 0, starsLv3 = 0;
    [SerializeField]
    Sprite fullStarSprite;

    void OnEnable()
    {
        MainMenuManager mainMenuManager = FindFirstObjectByType<MainMenuManager>().GetComponent<MainMenuManager>();
        FillStars();
        // switch (MainMenuManager.topicChosen)
        // {
        //     case 1:
        //         firstLevelPoints.text = SavePlayerDataManager.currentPlayerData.pointsLv1To1.ToString();
        //         //firstLevelPoints.text = PlayerPrefs.GetFloat("FirstTopicLevel1Points").ToString();
        //         if (SavePlayerDataManager.currentPlayerData.pointsLv1To1 >= mainMenuManager.minPointsLv1Top1)
        //         {
        //             firstLevelPoints.color = Color.green;
        //         }
        //         else
        //         {
        //             firstLevelPoints.color = Color.white;
        //         }
        //         fistLevelMinPoints.text = "/" + mainMenuManager.minPointsLv1Top1;
        //         secondLevelPoints.text = SavePlayerDataManager.currentPlayerData.pointsLv2To1.ToString();
        //         //secondLevelPoints.text = PlayerPrefs.GetFloat("FirstTopicLevel2Points").ToString();
        //         if (SavePlayerDataManager.currentPlayerData.pointsLv2To1 >= mainMenuManager.minPointsLv2Top1)
        //         {
        //             secondLevelPoints.color = Color.green;
        //         }
        //         else
        //         {
        //             secondLevelPoints.color = Color.white;
        //         }
        //         secondLevelMinPoints.text = "/" + mainMenuManager.minPointsLv2Top1;
        //         thirdLevelPoints.text = SavePlayerDataManager.currentPlayerData.pointsLv3To1.ToString();
        //         if (SavePlayerDataManager.currentPlayerData.pointsLv3To1 >= mainMenuManager.minPointsLv3Top1)
        //         {
        //             thirdLevelPoints.color = Color.green;
        //         }
        //         else
        //         {
        //             thirdLevelPoints.color = Color.white;
        //         }
        //         thirdLevelMinPoints.text = "/" + mainMenuManager.minPointsLv3Top1;
        //         Debug.Log("caso primo topic punteggio");
        //         break;
        //     case 2:
        //         firstLevelPoints.text = SavePlayerDataManager.currentPlayerData.pointsLv1To2.ToString();
        //         //firstLevelPoints.text = PlayerPrefs.GetFloat("SecondTopicLevel1Points").ToString();
        //         if (SavePlayerDataManager.currentPlayerData.pointsLv1To2 >= mainMenuManager.minPointsLv1Top2)
        //         {
        //             firstLevelPoints.color = Color.green;
        //         }
        //         else
        //         {
        //             firstLevelPoints.color = Color.white;
        //         }
        //         fistLevelMinPoints.text = "/" + mainMenuManager.minPointsLv1Top2;
        //         secondLevelPoints.text = SavePlayerDataManager.currentPlayerData.pointsLv2To2.ToString();
        //         //secondLevelPoints.text = PlayerPrefs.GetFloat("SecondTopicLevel2Points").ToString();
        //         if (SavePlayerDataManager.currentPlayerData.pointsLv2To2 >= mainMenuManager.minPointsLv2Top2)
        //         {
        //             secondLevelPoints.color = Color.green;
        //         }
        //         else
        //         {
        //             secondLevelPoints.color = Color.white;
        //         }
        //         secondLevelMinPoints.text = "/" + mainMenuManager.minPointsLv2Top2;
        //         thirdLevelPoints.text = SavePlayerDataManager.currentPlayerData.pointsLv3To2.ToString();
        //         if (SavePlayerDataManager.currentPlayerData.pointsLv3To2 >= mainMenuManager.minPointsLv3Top2)
        //         {
        //             thirdLevelPoints.color = Color.green;
        //         }
        //         else
        //         {
        //             thirdLevelPoints.color = Color.white;
        //         }
        //         thirdLevelMinPoints.text = "/" + mainMenuManager.minPointsLv3Top2;
        //         Debug.Log("caso secondo topic punteggio");
        //         break;
        //     default:
        //         Debug.Log("no topic");
        //         break;
        // }
    }
    void FillStars()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("starsLv1"); i++)
        {
            holderLevel1.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("starsLv2"); i++)
        {
            holderLevel2.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("starsLv3"); i++)
        {
            holderLevel3.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
        }
    }
}

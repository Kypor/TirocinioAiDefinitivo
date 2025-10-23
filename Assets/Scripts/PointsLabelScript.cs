using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsLabelScript : MonoBehaviour
{
    [SerializeField]
    GameObject holderLevel1, holderLevel2, holderLevel3;
    [SerializeField]
    Sprite fullStarSprite, emptyStarSprite;

    void OnEnable()
    {
        //EmptyStars();
        FillStars();
    }
    void EmptyStars()
    {
        for (int i = 0; i < 3; i++)
        {
            holderLevel1.transform.GetChild(i).GetComponent<Image>().sprite = emptyStarSprite;
            holderLevel2.transform.GetChild(i).GetComponent<Image>().sprite = emptyStarSprite;
            holderLevel3.transform.GetChild(i).GetComponent<Image>().sprite = emptyStarSprite;
        }

    }
    void FillStars()
    {
        EmptyStars();
        MainMenuManager mainMenuManager = FindFirstObjectByType<MainMenuManager>().GetComponent<MainMenuManager>();
        switch (MainMenuManager.topicChosen)
        {
            case 1:
                for (int i = 0; i < mainMenuManager.AllStars[0]; i++)
                {
                    holderLevel1.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
                }
                for (int i = 0; i < mainMenuManager.AllStars[1]; i++)
                {
                    holderLevel2.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
                }
                for (int i = 0; i < mainMenuManager.AllStars[2]; i++)
                {
                    holderLevel3.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
                }
                break;
            case 2:
                for (int i = 0; i < mainMenuManager.AllStars[3]; i++)
                {
                    holderLevel1.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
                }
                for (int i = 0; i < mainMenuManager.AllStars[4]; i++)
                {
                    holderLevel2.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
                }
                for (int i = 0; i < mainMenuManager.AllStars[5]; i++)
                {
                    holderLevel3.transform.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
                }
                break;
            default:
                Debug.Log("no topic");
                break;
        }
    }
}

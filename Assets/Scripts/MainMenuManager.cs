using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainPanel,levelPanel;
    public float minPointsLevel1, minPointsLevel2;
    [SerializeField]
    TextMeshProUGUI thirdLevelText;
    public GameObject pointsLabel;

    void Start()
    {
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
        pointsLabel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.SetFloat("Level1Points",50f);
        }
    }
    public void ChoseLevel()
    {
        if (PlayerPrefs.GetFloat("Level1Points") < minPointsLevel1 || PlayerPrefs.GetFloat("Level2Points") < minPointsLevel2)
        {
            thirdLevelText.color = Color.gray;
        }
        else
        {
            thirdLevelText.color = Color.white;
        }
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);
    }
    public void Back()
    {
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
    }
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        if (PlayerPrefs.GetFloat("Level1Points") >= minPointsLevel1 && PlayerPrefs.GetFloat("Level2Points") >= minPointsLevel2)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}

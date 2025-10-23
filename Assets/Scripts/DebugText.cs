using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    public TMP_InputField tMP_InputField;
    private TextMeshProUGUI text;
    private RunMiniLM runMiniLM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        runMiniLM = FindAnyObjectByType<RunMiniLM>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = runMiniLM.isEnter.ToString();
    }
}

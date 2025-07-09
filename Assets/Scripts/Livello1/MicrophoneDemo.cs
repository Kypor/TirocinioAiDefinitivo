using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Whisper.Utils;
using Button = UnityEngine.UI.Button;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Whisper.Samples
{
    /// <summary>
    /// Record audio clip from microphone and make a transcription.
    /// </summary>
    public class MicrophoneDemo : MonoBehaviour
    {
        public WhisperManager whisper;
        public MicrophoneRecord microphoneRecord;
        public bool streamSegments = true;
        public bool printLanguage = true;
        [SerializeField]
        private JapaneseWordArray JapaneseWords;

        [Header("UI")]
        public Button button;
        public TextMeshProUGUI buttonText;
        public TextMeshProUGUI outputText;
        public TextMeshProUGUI randomWord;
        public TextMeshProUGUI totalPointsText, finalPointText;
        public Dropdown languageDropdown;
        public GameObject settingsPanel, finishGamePanel;
        private int randomWordIdex;
        private float totalPoints;
        [SerializeField]
        public float basePoints, penalityPercentage;
        private string _buffer;
        [SerializeField] private int numberOfWords, currentCorrectWords;
        [SerializeField] Sprite fullStarSprite, emptyStarSprite;
        [SerializeField] List<Image> starsImages = new List<Image>();

        private void Awake()
        {
            EmptyStars();
            currentCorrectWords = 0;
            totalPoints = 0f;
            totalPointsText.text = "Points : " + totalPoints.ToString();
            randomWord.text = GetRandomWord();
            whisper.OnNewSegment += OnNewSegment;
            //whisper.OnProgress += OnProgressHandler;
            microphoneRecord.OnRecordStop += OnRecordStop;

            //button.onClick.AddListener(OnButtonPressed);
            languageDropdown.value = languageDropdown.options
                .FindIndex(op => op.text == whisper.language);
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

            //translateToggle.isOn = whisper.translateToEnglish;
            //translateToggle.onValueChanged.AddListener(OnTranslateChanged);

            //vadToggle.isOn = microphoneRecord.vadStop;
            //vadToggle.onValueChanged.AddListener(OnVadChanged);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && finishGamePanel.GetComponent<CanvasGroup>().alpha == 0)
            {
                Settings();
            }
        }
        void Settings()
        {
            if (settingsPanel.GetComponent<CanvasGroup>().alpha == 0)
            {
                ShowSettings();
            }
            else
            {
                UnShowSettings();
            }
        }
        private void OnVadChanged(bool vadStop)
        {
            microphoneRecord.vadStop = vadStop;
        }

        public void OnButtonPressed()
        {
            if (!microphoneRecord.IsRecording)
            {
                microphoneRecord.StartRecord();
                buttonText.text = "Stop";
            }
            else
            {
                microphoneRecord.StopRecord();
                buttonText.text = "Record";
            }
        }

        private async void OnRecordStop(AudioChunk recordedAudio)
        {
            buttonText.text = "Record";
            _buffer = "";

            var sw = new Stopwatch();
            sw.Start();

            var res = await whisper.GetTextAsync(recordedAudio.Data, recordedAudio.Frequency, recordedAudio.Channels);
            if (res == null || !outputText)
                return;

            var time = sw.ElapsedMilliseconds;
            var rate = recordedAudio.Length / (time * 0.001f);
            //timeText.text = $"Time: {time} ms\nRate: {rate:F1}x";
            var text = res.Result;

            outputText.text = text;
            text = Regex.Replace(text, "[、，゠＝…‥。.,?! ]", "");
            bool found = false;
            for (int i = 0; i < 3; i++)
            {
                if (text.ToLower().Equals(JapaneseWords.paroleConPronunce[randomWordIdex].pronunce[i]))
                {
                    found = true;
                    StartCoroutine(RightWordCoroutine());
                    break;
                }
            }
            if (!found)
            {
                StartCoroutine(WrongWordCoroutine());
            }

        }

        private void OnLanguageChanged(int ind)
        {
            var opt = languageDropdown.options[ind];
            whisper.language = opt.text;
        }

        private void OnNewSegment(WhisperSegment segment)
        {
            if (!streamSegments || !outputText)
                return;

            _buffer += segment.Text;
            outputText.text = _buffer + "...";
        }

        private string GetRandomWord()
        {
            randomWordIdex = Random.Range(0, JapaneseWords.paroleConPronunce.Count);
            return JapaneseWords.paroleConPronunce[randomWordIdex].pronunce[0];

        }

        private IEnumerator RightWordCoroutine()
        {
            randomWord.color = Color.green;
            //float points = penalityPercentage / 100f * wrongAnswersCount * basePoints;
            //totalPoints += basePoints - points <= 10f ? 10f : basePoints - points;
            totalPoints += basePoints;
            totalPointsText.text = "Points : " + totalPoints.ToString();
            PlayerPrefs.SetFloat("Level1Points", totalPoints);
            yield return new WaitForSeconds(3f);
            randomWord.color = Color.white;
            randomWord.text = GetRandomWord();
            outputText.text = "";
            if (currentCorrectWords < numberOfWords - 1)
            {
                currentCorrectWords++;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ShowResults());

            }
        }
        private IEnumerator WrongWordCoroutine()
        {
            float points = penalityPercentage / 100f * basePoints;
            totalPoints -= points;
            totalPointsText.text = "Points : " + totalPoints.ToString();
            PlayerPrefs.SetFloat("Level1Points", totalPoints);
            randomWord.color = Color.red;
            yield return new WaitForSeconds(0.50f);
            randomWord.color = Color.white;
        }

        void ShowSettings()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(1, settingsPanel.GetComponent<CanvasGroup>()));
            settingsPanel.GetComponent<CanvasGroup>().interactable = true;
            settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        void UnShowSettings()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(0, settingsPanel.GetComponent<CanvasGroup>()));
            settingsPanel.GetComponent<CanvasGroup>().interactable = false;
            settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        public IEnumerator Fade(float end, CanvasGroup canvasGroup)
        {
            float elapsedTime = 0.0f;
            float start = canvasGroup.alpha;
            while (elapsedTime < 0.5f)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / 0.5f);
                yield return null;
            }
            canvasGroup.alpha = end;
        }
        void EmptyStars()
        {
            foreach (Image image in starsImages)
            {
                image.sprite = emptyStarSprite;
            }
        }
        IEnumerator ShowResults()
        {
            StartCoroutine(Fade(1, finishGamePanel.GetComponent<CanvasGroup>()));
            finishGamePanel.GetComponent<CanvasGroup>().interactable = true;
            finishGamePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            totalPointsText.gameObject.SetActive(false);
            finalPointText.text = totalPointsText.text;
            yield return new WaitForSeconds(1f);
            StartCoroutine(FillStars());
        }
        IEnumerator FillStars()
        {
            float performanceRatio = totalPoints / (numberOfWords * basePoints);
            int stars;
            if (performanceRatio >= 0.90f) stars = 3;
            else if (performanceRatio >= 0.66f) stars = 2;
            else if (performanceRatio >= 0.33f) stars = 1;
            else stars = 0;
           UnityEngine.Debug.Log(stars);
            for (int i = 0; i < stars; i++)
            {
                starsImages[i].sprite = fullStarSprite;
                yield return new WaitForSeconds(0.5f);
            }
        }
        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }

}
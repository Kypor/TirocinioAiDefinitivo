using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Whisper.Utils;
using Button = UnityEngine.UI.Button;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Whisper.Samples
{
    /// <summary>
    /// Record audio clip from microphone and make a transcription.
    /// </summary>
    public class MicrophoneDemo : MonoBehaviour
    {
        [SerializeField] PointsManagerScript pointsManagerScript;
        public WhisperManager whisper;
        public MicrophoneRecord microphoneRecord;
        public bool streamSegments = true;
        public bool printLanguage = true;
        [SerializeField]
        private List<JapaneseWordArray> JapaneseWords = new List<JapaneseWordArray>();

        [Header("UI")]
        public Button button;
        public TextMeshProUGUI buttonText;
        public TextMeshProUGUI outputText;
        public TextMeshProUGUI randomWord;
        public TextMeshProUGUI totalPointsText;
        public Dropdown languageDropdown;
        public GameObject settingsPanel;
        private int randomWordIdex;
        private float totalPoints;
        private string _buffer;
        public int numberOfWords, currentCorrectWords;

        private void Awake()
        {
            currentCorrectWords = 0;
            totalPointsText.text = "Points : " + totalPoints.ToString();
            randomWord.text = GetRandomWord();
            whisper.OnNewSegment += OnNewSegment;
            microphoneRecord.OnRecordStop += OnRecordStop;
            languageDropdown.value = languageDropdown.options
                .FindIndex(op => op.text == whisper.language);
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
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
            var text = res.Result;

            outputText.text = text;
            text = Regex.Replace(text, "[、，゠＝…‥。.,?! ]", "");
            bool found = false;
            for (int i = 0; i < 3; i++)
            {
                if (text.ToLower().Equals(JapaneseWords[MainMenuManager.topicChosen - 1].paroleConPronunce[randomWordIdex].pronunce[i]))
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
            randomWordIdex = Random.Range(0, JapaneseWords[MainMenuManager.topicChosen - 1].paroleConPronunce.Count);
            return JapaneseWords[MainMenuManager.topicChosen - 1].paroleConPronunce[randomWordIdex].pronunce[0];

        }

        private IEnumerator RightWordCoroutine()
        {
            SoundManager.instance.PlaySoundFX(1);
            randomWord.color = Color.green;
            pointsManagerScript.AddPoints();
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

                StartCoroutine(pointsManagerScript.ShowResults());

            }
        }

        private IEnumerator WrongWordCoroutine()
        {
            SoundManager.instance.PlaySoundFX(2);
            pointsManagerScript.SubPoints();
            randomWord.color = Color.red;
            yield return new WaitForSeconds(0.50f);
            randomWord.color = Color.white;
        }
    }
}
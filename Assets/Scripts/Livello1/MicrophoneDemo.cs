using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Whisper.Utils;
using Button = UnityEngine.UI.Button;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;

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
        public Text buttonText;
        public Text outputText;
        public TextMeshProUGUI randomWord;

        public Dropdown languageDropdown;

        private int randomWordIdex;
        private string _buffer;

        private void Awake()
        {
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
            // if (printLanguage)
            //     text += $"\n\nLanguage: {res.Language}";

            outputText.text = text;
            text = Regex.Replace(text, "[、，゠＝…‥。.,?! ]", "");
            bool found = false;
            for (int i = 0; i < 3; i++)
            {
                if (text.ToLower().Equals(JapaneseWords.paroleConPronunce[randomWordIdex].pronunce[i]))
                {
                    StartCoroutine(RightWordCoroutine());
                    found = true;
                    break;
                }
            }
            if(!found)
            {
                UnityEngine.Debug.Log(randomWord.text);
                UnityEngine.Debug.Log("stupido idiota");
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
            //UiUtils.ScrollDown(scroll);
        }

        private string GetRandomWord()
        {
            randomWordIdex = Random.Range(0, JapaneseWords.paroleConPronunce.Count);
            UnityEngine.Debug.Log(randomWordIdex);
            return JapaneseWords.paroleConPronunce[randomWordIdex].pronunce[0];

        }

        private IEnumerator RightWordCoroutine()
        {
            randomWord.color = Color.green;
            UnityEngine.Debug.Log("bravo coglione");
            yield return new WaitForSeconds(5f);
            randomWord.color = Color.white;
            randomWord.text = GetRandomWord();
            outputText.text = "";
        }
    }

}
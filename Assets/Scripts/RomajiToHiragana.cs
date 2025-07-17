using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RomajiToHiragana : MonoBehaviour
{
    public TMP_InputField inputField;
    public string romajiString;

    void Start()
    {
        // Inverti il dizionario all'avvio
        romajiToHiragana = new Dictionary<string, string>();
        foreach (var pair in HiraganaToRomaji)
        {
            // Evita duplicati (nel tuo caso, per esempio "shi" e "si")
            if (!romajiToHiragana.ContainsKey(pair.Value))
                romajiToHiragana[pair.Value] = pair.Key;
        }

        // Test: stringa in romaji
        string inputRomaji = "konnichiwa";
        string converted = ConvertRomajiToHiragana(inputRomaji);
        Debug.Log("Hiragana: " + converted); // こんにちは
    }

    string ConvertRomajiToHiragana(string input)
    {
        string result = "";
        int i = 0;
        int maxLen = 3; // Massima lunghezza di un'unità romaji (es. "kyo")

        while (i < input.Length)
        {
            bool matched = false;
            int len = Mathf.Min(maxLen, input.Length - i);

            // Provo da maxLen a 1 per trovare la combinazione più lunga possibile
            for (int j = len; j > 0; j--)
            {
                string chunk = input.Substring(i, j);
                if (romajiToHiragana.TryGetValue(chunk, out string hira))
                {
                    result += hira;
                    i += j;
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                // Se non corrisponde a nulla, lo aggiungo come carattere grezzo
                result += input[i];
                i++;
            }
        }

        return result;
    }

    private Dictionary<string, string> HiraganaToRomaji = new Dictionary<string, string>()
    {
                {"あ", "a"},
                {"い", "i"},
                {"う", "u"},
                {"え", "e"},
                {"お", "o"},


                {"か", "ka"},
                {"き", "ki"},
                {"く", "ku"},
                {"け", "ke"},
                {"こ", "ko"},

                {"さ", "sa"},
                {"し", "si"},
                {"す", "su"},
                {"せ", "se"},
                {"そ", "so"},

                {"た", "ta"},
                {"ち", "ti"},
                {"つ", "tu"},
                {"て", "te"},
                {"と", "to"},

                {"な", "na"},
                {"に", "ni"},
                {"ぬ", "nu"},
                {"ね", "ne"},
                {"の", "no"},
                
                // 直音-清音(ハ～ヲ)
                {"は", "ha"},
                {"ひ", "hi"},
                {"ふ", "hu"},
                {"へ", "he"},
                {"ほ", "ho"},

                {"ま", "ma"},
                {"み", "mi"},
                {"む", "mu"},
                {"め", "me"},
                {"も", "mo"},

                {"や", "ya"},
                {"ゆ", "yu"},
                {"よ", "yo"},

                {"ら", "ra"},
                {"り", "ri"},
                {"る", "ru"},
                {"れ", "re"},
                {"ろ", "ro"},

                {"わ", "wa"},
                {"ゐ", "wi"},
                {"ゑ", "we"},
                {"を", "wo"},

                // 直音-濁音(ガ～ボ)、半濁音(パ～ポ)
                {"が", "ga"},
                {"ぎ", "gi"},
                {"ぐ", "gu"},
                {"げ", "ge"},
                {"ご", "go"},

                {"ざ", "za"},
                {"じ", "zi"},
                {"ず", "zu"},
                {"ぜ", "ze"},
                {"ぞ", "zo"},

                {"だ", "da"},
                {"ぢ", "di"},
                {"づ", "du"},
                {"で", "de"},
                {"ど", "do"},

                {"ば", "ba"},
                {"び", "bi"},
                {"ぶ", "bu"},
                {"べ", "be"},
                {"ぼ", "bo"},

                {"ぱ", "pa"},
                {"ぴ", "pi"},
                {"ぷ", "pu"},
                {"ぺ", "pe"},
                {"ぽ", "po"},

                // 拗音-清音(キャ～リョ)
                {"きゃ", "kya"},
                {"きゅ", "kyu"},
                {"きょ", "kyo"},
                {"しゃ", "sya"},
                {"しゅ", "syu"},
                {"しょ", "syo"},
                {"ちゃ", "tya"},
                {"ちゅ", "tyu"},
                {"ちょ", "tyo"},
                {"にゃ", "nya"},
                {"にゅ", "nyu"},
                {"にょ", "nyo"},
                {"ひゃ", "hya"},
                {"ひゅ", "hyu"},
                {"ひょ", "hyo"},
                {"みゃ", "mya"},
                {"みゅ", "myu"},
                {"みょ", "myo"},
                {"りゃ", "rya"},
                {"りゅ", "ryu"},
                {"りょ", "ryo"},
               

                // 拗音-濁音(ギャ～ビョ)、半濁音(ピャ～ピョ)、合拗音(クヮ、グヮ)
                {"ぎゃ", "gya"},
                {"ぎゅ", "gyu"},
                {"ぎょ", "gyo"},
                {"じゃ", "zya"},
                {"じゅ", "zyu"},
                {"じょ", "zyo"},
                {"ぢゃ", "dya"},
                {"ぢゅ", "dyu"},
                {"ぢょ", "dyo"},
                {"びゃ", "bya"},
                {"びゅ", "byu"},
                {"びょ", "byo"},
                {"ぴゃ", "pya"},
                {"ぴゅ", "pyu"},
                {"ぴょ", "pyo"},
                {"くゎ", "kwa"},
                {"ぐゎ", "gwa"},

    };

    private Dictionary<string, string> romajiToHiragana;

}

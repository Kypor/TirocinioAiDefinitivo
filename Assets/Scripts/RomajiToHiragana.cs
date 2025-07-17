using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RomajiToHiragana : MonoBehaviour
{
    public TMP_InputField inputField;

    private Dictionary<string, string> romajiToHiragana = new Dictionary<string, string>()
    {
        // Vocali
        {"a", "あ"}, {"i", "い"}, {"u", "う"}, {"e", "え"}, {"o", "お"},

        // K
        {"ka", "か"}, {"ki", "き"}, {"ku", "く"}, {"ke", "け"}, {"ko", "こ"},
        {"kya", "きゃ"}, {"kyu", "きゅ"}, {"kyo", "きょ"},

        // S
        {"sa", "さ"}, {"shi", "し"}, {"su", "す"}, {"se", "せ"}, {"so", "そ"},
        {"sha", "しゃ"}, {"shu", "しゅ"}, {"sho", "しょ"},

        // T
        {"ta", "た"}, {"chi", "ち"}, {"tsu", "つ"}, {"te", "て"}, {"to", "と"},
        {"cha", "ちゃ"}, {"chu", "ちゅ"}, {"cho", "ちょ"},

        // N
        {"na", "な"}, {"ni", "に"}, {"nu", "ぬ"}, {"ne", "ね"}, {"no", "の"},
        {"nya", "にゃ"}, {"nyu", "にゅ"}, {"nyo", "にょ"},

        // H
        {"ha", "は"}, {"hi", "ひ"}, {"fu", "ふ"}, {"he", "へ"}, {"ho", "ほ"},
        {"hya", "ひゃ"}, {"hyu", "ひゅ"}, {"hyo", "ひょ"},

        // M
        {"ma", "ま"}, {"mi", "み"}, {"mu", "む"}, {"me", "め"}, {"mo", "も"},
        {"mya", "みゃ"}, {"myu", "みゅ"}, {"myo", "みょ"},

        // Y
        {"ya", "や"}, {"yu", "ゆ"}, {"yo", "よ"},

        // R
        {"ra", "ら"}, {"ri", "り"}, {"ru", "る"}, {"re", "れ"}, {"ro", "ろ"},
        {"rya", "りゃ"}, {"ryu", "りゅ"}, {"ryo", "りょ"},

        // W
        {"wa", "わ"}, {"wo", "を"},

        // G
        {"ga", "が"}, {"gi", "ぎ"}, {"gu", "ぐ"}, {"ge", "げ"}, {"go", "ご"},
        {"gya", "ぎゃ"}, {"gyu", "ぎゅ"}, {"gyo", "ぎょ"},

        // Z
        {"za", "ざ"}, {"ji", "じ"}, {"zu", "ず"}, {"ze", "ぜ"}, {"zo", "ぞ"},
        {"ja", "じゃ"}, {"ju", "じゅ"}, {"jo", "じょ"},

        // D
        {"da", "だ"}, {"de", "で"}, {"do", "ど"},

        // B
        {"ba", "ば"}, {"bi", "び"}, {"bu", "ぶ"}, {"be", "べ"}, {"bo", "ぼ"},
        {"bya", "びゃ"}, {"byu", "びゅ"}, {"byo", "びょ"},

        // P
        {"pa", "ぱ"}, {"pi", "ぴ"}, {"pu", "ぷ"}, {"pe", "ぺ"}, {"po", "ぽ"},
        {"pya", "ぴゃ"}, {"pyu", "ぴゅ"}, {"pyo", "ぴょ"}
    };

    private string romajiBuffer = "";
    bool ignoreCallback = false;

    void Start()
    {
        inputField.onValueChanged.AddListener(OnInputChanged);
    }

    void OnInputChanged(string text)
    {
        if (ignoreCallback) return;

        if (text.Length == 0)
        {
            romajiBuffer = "";
            return;
        }

        int caretPos = inputField.caretPosition;
        if (caretPos == 0) return;

        char lastChar = text[caretPos - 1];
        
        // Se non è una lettera, gestisci eventuali "n" in sospeso
        if (!char.IsLetter(lastChar))
        {
            if (romajiBuffer == "n")
            {
                ReplaceBufferWithKana("ん", 1);
            }
            romajiBuffer = "";
            return;
        }

        romajiBuffer += lastChar.ToString().ToLower();

        // Caso speciale: doppia "nn" → sempre "ん"
        if (romajiBuffer.EndsWith("nn"))
        {
            ReplaceBufferWithKana("ん", 2);
            romajiBuffer = "";
            return;
        }

        // Caso: "n" seguita da consonante che non può formare sillaba con "n"
        if (romajiBuffer.Length >= 2 && romajiBuffer[romajiBuffer.Length - 2] == 'n')
        {
            char nextChar = romajiBuffer[romajiBuffer.Length - 1];
            
            // Se "n" è seguita da consonante che NON può formare sillaba con "n"
            if (ShouldConvertNToKana(nextChar))
            {
                // Converti "n" in "ん", mantieni la consonante nel buffer
                string remainingChar = nextChar.ToString();
                ReplaceBufferWithKana("ん", 1);
                romajiBuffer = remainingChar; // Mantieni la consonante per la prossima iterazione
                return;
            }
        }

        // Cerca match nel dizionario, partendo dalle combinazioni più lunghe
        for (int len = Mathf.Min(3, romajiBuffer.Length); len > 0; len--)
        {
            string slice = romajiBuffer.Substring(romajiBuffer.Length - len, len);
            if (romajiToHiragana.TryGetValue(slice, out string kana))
            {
                ReplaceBufferWithKana(kana, len);
                romajiBuffer = "";
                return;
            }
        }

        // Se il buffer è troppo lungo senza match, potrebbero esserci "n" da convertire
        if (romajiBuffer.Length > 3)
        {
            // Cerca "n" isolate all'inizio del buffer
            for (int i = 0; i < romajiBuffer.Length - 1; i++)
            {
                if (romajiBuffer[i] == 'n' && ShouldConvertNToKana(romajiBuffer[i + 1]))
                {
                    // Converti la "n" a posizione i
                    int caretPosition = inputField.caretPosition;
                    int nPosition = caretPosition - romajiBuffer.Length + i;
                    
                    string currentText = inputField.text;
                    string newText = currentText.Substring(0, nPosition) + "ん" + currentText.Substring(nPosition + 1);
                    
                    ignoreCallback = true;
                    inputField.text = newText;
                    inputField.caretPosition = caretPosition;
                    ignoreCallback = false;
                    
                    romajiBuffer = romajiBuffer.Substring(i + 1);
                    break;
                }
            }
        }
    }

    bool ShouldConvertNToKana(char nextChar)
    {
        // "n" diventa "ん" se seguita da:
        // - consonanti che non possono combinarsi con "n" (escluse a, e, i, o, u, y)
        // - oppure consonanti che formano sillabe specifiche ma non con "n"
        
        // Consonanti che NON possono seguire "n" per formare sillabe:
        // b, c, d, f, g, j, k, m, p, q, r, s, t, v, w, x, z
        
        if (IsVowel(nextChar)) return false; // na, ne, ni, no, nu sono valide
        if (nextChar == 'y') return false;   // nya, nyu, nyo sono valide
        
        return true; // tutte le altre consonanti
    }

    bool IsVowel(char c)
    {
        return "aeiou".Contains(c);
    }

    void ReplaceBufferWithKana(string kana, int romajiLength, int removeFromBuffer = 0)
    {
        int caretPos = inputField.caretPosition;
        int replaceStart = caretPos - romajiLength;

        string text = inputField.text;
        string newText = text.Substring(0, replaceStart) + kana + text.Substring(caretPos);

        ignoreCallback = true;
        inputField.text = newText;
        inputField.caretPosition = replaceStart + kana.Length;
        ignoreCallback = false;

        if (removeFromBuffer > 0 && romajiBuffer.Length >= removeFromBuffer)
            romajiBuffer = romajiBuffer.Substring(0, romajiBuffer.Length - removeFromBuffer);
    }
}
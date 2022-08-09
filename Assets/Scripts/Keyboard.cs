using TMPro;
using UnityEngine;

public class Keyboard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreValueText;
    [SerializeField] private TextMeshProUGUI scoreInRowValueText;
    [SerializeField] private TextMeshProUGUI nextKeyText;

    private int numberOfGeneratedLetters;
    private int scoreOverall;
    private int scoreInRow;
    private string currentKey;
    private bool wasKeyPress;

    private readonly string[] keys = new string[] {
        "q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
        "a", "s", "d", "f", "g", "h", "j", "k", "l",
        "z", "x", "c", "v", "b", "n", "m"
    };

    private void Start() {
        GenerateNextKey();
    }

    private void Update() {
        CheckKeyboard();
    }

    private void GenerateNextKey() {
        int randomNumber = Random.Range(0, keys.Length);
        currentKey = keys[randomNumber];
        nextKeyText.text = currentKey;
    }

    private void CheckKeyboard() {
        bool isCurrentKeyPress = Input.anyKey;
        if (!isCurrentKeyPress && wasKeyPress) {
            wasKeyPress = false;
            GenerateNextKey();
            return;
        }
        
        if (!isCurrentKeyPress || wasKeyPress) {
            return;
        }

        numberOfGeneratedLetters++;
        wasKeyPress = true;
        bool isCorrectKey = Input.GetKeyDown(currentKey);
        if (isCorrectKey) {
            scoreOverall++;
            scoreInRow++;
        } else {
            scoreInRow = 0;
        }

        scoreValueText.text = $"{scoreOverall} / {numberOfGeneratedLetters}";
        scoreInRowValueText.text = scoreInRow.ToString();
    }
}
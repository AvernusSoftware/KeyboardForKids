using TMPro;
using UnityEngine;

public class Keyboard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nextKeyText;

    private int score;
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
        if (!isCurrentKeyPress) {
            return;
        }

        wasKeyPress = true;
        bool isCorrectKey = Input.GetKeyDown(currentKey);
        if (isCorrectKey) {
            score++;
            scoreText.text = score.ToString();
        }
    }
}
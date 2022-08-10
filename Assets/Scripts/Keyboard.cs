using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI nextKeyText;

    private Score score;
    private string currentKey;
    private bool wasKeyPress;

    private readonly string[] keys = new string[] {
        "q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
        "a", "s", "d", "f", "g", "h", "j", "k", "l",
        "z", "x", "c", "v", "b", "n", "m"
    };

    private Dictionary<string, Image> keysObjects;

    private void Start() {
        ConfigureObjects();
        FindKeysObjects();
        GenerateNextKey();
    }

    private void Update() {
        CheckKeyboard();
    }

    private void ConfigureObjects() {
        score = FindObjectOfType<Score>();
    }

    private void FindKeysObjects() {
        keysObjects = new Dictionary<string, Image>();
        GameObject[] keysGameObjects = GameObject.FindGameObjectsWithTag("Key");

        foreach (GameObject keyGameObject in keysGameObjects) {
            string name = keyGameObject.name;
            Transform keyTransform = keyGameObject.transform;
            Transform imageTransform = keyTransform.Find("Image");
            GameObject imageGameObject = imageTransform.gameObject;
            Image image = imageGameObject.GetComponent<Image>();

            if (name != string.Empty && !keysObjects.ContainsKey(name)) {
                keysObjects.Add(name.ToLower(), image);
            }
        }
    }

    private void GenerateNextKey() {
        if (!string.IsNullOrEmpty(currentKey)) {
            keysObjects[currentKey].color = Color.white;
        }

        int randomNumber = Random.Range(0, keys.Length);
        currentKey = keys[randomNumber];
        nextKeyText.text = currentKey;
        keysObjects[currentKey].color = Color.yellow;
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

        wasKeyPress = true;
        bool isCorrectKey = Input.GetKeyDown(currentKey);
        if (isCorrectKey) {
            score.GoodAnswer();
        } else {
            score.BadAnswer();
        }
    }
}
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

    private Dictionary<string, KeyObject> keysObjects;

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
        keysObjects = new Dictionary<string, KeyObject>();
        GameObject[] keysGameObjects = GameObject.FindGameObjectsWithTag("Key");

        foreach (GameObject keyGameObject in keysGameObjects) {
            AddKeyObject(keyGameObject);
        }
    }

    private void AddKeyObject(GameObject keyGameObject) {
        string name = keyGameObject.name;
        KeyObject keyObject = keyGameObject.GetComponent<KeyObject>();
        if (name != string.Empty && !keysObjects.ContainsKey(name)) {
            keysObjects.Add(name.ToLower(), keyObject);
        }
    }

    private void GenerateNextKey() {
        if (!string.IsNullOrEmpty(currentKey)) {
            Image PreviousImageComponent = keysObjects[currentKey].ImageComponent;
            PreviousImageComponent.color = Color.white;
        }

        int randomNumber = Random.Range(0, keys.Length);
        currentKey = keys[randomNumber];
        nextKeyText.text = currentKey;
        Image currentImageComponent = keysObjects[currentKey].ImageComponent;
        currentImageComponent.color = Color.yellow;
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
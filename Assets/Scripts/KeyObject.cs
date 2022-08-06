using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyObject : MonoBehaviour {
    [SerializeField] private Image graphics;
    [SerializeField] private TextMeshProUGUI textComponent;

    public void Generate(string text) {
        if (text.Length == 0) {
            graphics.enabled = false;
            textComponent.enabled = false;
        }

        if (textComponent != null) {
            textComponent.text = text;
        }
    }
}
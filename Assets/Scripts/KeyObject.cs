using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyObject : MonoBehaviour {
    [SerializeField] private Image imageComponent;
    [SerializeField] private TextMeshProUGUI textComponent;

    public Image ImageComponent { get => imageComponent; }
    public TextMeshProUGUI TextComponent { get => textComponent; }

    public void Generate(string text) {
        if (text.Length == 0) {
            imageComponent.enabled = false;
            textComponent.enabled = false;
        }

        if (textComponent != null) {
            textComponent.text = text;
        }
    }
}
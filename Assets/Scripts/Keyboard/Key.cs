using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {
    [SerializeField] private Image imageComponent;
    [SerializeField] private TextMeshProUGUI textComponent;

    public Image ImageComponent { get => imageComponent; }
    public TextMeshProUGUI TextComponent { get => textComponent; }
}
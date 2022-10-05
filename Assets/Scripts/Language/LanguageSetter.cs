using TMPro;
using UnityEngine;

public class LanguageSetter : MonoBehaviour {

    [SerializeField]
    public string translateIdn;

    private LanguageManager LanguageManager { get; set; }

    private TextMeshProUGUI TextMeshProUGUI { get; set; }

    public void Start() {
        SetupManagers();
        ScanForLanguageComponents();
        SetTranslation();
    }

    public void SetTranslation() {
        string translation = LanguageManager.GetTranslation(translateIdn);

        if (TextMeshProUGUI) {
            TextMeshProUGUI.text = translation;
        }
    }

    private void SetupManagers() {
        LanguageManager = FindObjectOfType<LanguageManager>();
    }

    private void ScanForLanguageComponents() {
        TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
}
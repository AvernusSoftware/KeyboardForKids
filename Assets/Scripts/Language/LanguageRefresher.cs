using UnityEngine;

public class LanguageRefresher : MonoBehaviour {
    private LanguageManager LanguageManager { get; set; }
    private LanguageSetter[] LanguagesComponents { get; set; }

    private void Start() {
        SetupManagers();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Refresh();
        }
    }

    private void SetupManagers() {
        LanguageManager = FindObjectOfType<LanguageManager>();
        LanguagesComponents = FindObjectsOfType<LanguageSetter>();
    }

    private void Refresh() {
        LanguageName languageName = LanguageManager.GetNextLanguageName();
        LanguageManager.SetLanguage(languageName);

        foreach (LanguageSetter languageComponent in LanguagesComponents) {
            languageComponent.SetTranslation();
        }
    }
}
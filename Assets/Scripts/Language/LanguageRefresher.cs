using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageRefresher : MonoBehaviour, IPointerClickHandler {
    private LanguageManager LanguageManager { get; set; }
    private LanguageSetter[] LanguagesComponents { get; set; }

    private void Start() {
        SetupManagers();
    }

    public void OnPointerClick(PointerEventData eventData) {
        Refresh();
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
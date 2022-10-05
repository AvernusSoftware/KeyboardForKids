using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

public class LanguageManager : MonoBehaviour {

    [SerializeField]
    private TextAsset languageFileContent;

    [SerializeField]
    private List<KeyboardSoundsConfig> keyboardSoundsConfig;

    private Dictionary<string, Dictionary<string, string>> Languages { get; set; }
    private List<string> LanguagesIdentifier { get; set; }
    private Dictionary<string, string> CurrentLanguage { get; set; }

    private string CurrentLanguageIdentifier { get; set; }

    private const string defaultLanguage = "English";
    private const string languageIdentifier = "languageIdentifier";
    private const string twoletterisolanguagename = "twoletterisolanguagename";
    private const string noTranslationFound = "No translation definition for [{0}].";

    public void Start() {
        LoadLanguages();
        SetLanguage("?");
    }

    public void SetLanguage(string languageIdentifier) {
        if (languageIdentifier == "?") {
            CurrentLanguageIdentifier = DetectLanguage();
            CurrentLanguage = Languages[CurrentLanguageIdentifier];
        } else if (Languages.ContainsKey(CurrentLanguageIdentifier)) {
            CurrentLanguage = Languages[CurrentLanguageIdentifier];
        } else {
            CurrentLanguage = Languages[defaultLanguage];
        }
    }

    public string GetTranslation(string translationIdentifier) {
        string pureTranslation = GetPureTranslation(translationIdentifier);
        string translation = pureTranslation.Replace("[br]", Environment.NewLine);
        string translationWithVariables = ReplaceVariables(translation);

        return translationWithVariables;
    }

    public List<string> GetAllLanguages() {
        return LanguagesIdentifier;
    }

    public string DetectLanguage() {
        CultureInfo cultureInfo = CultureInfo.InstalledUICulture;
        string systemLanguageIdentifier = Languages.FirstOrDefault(x => x.Value[twoletterisolanguagename] == cultureInfo.TwoLetterISOLanguageName).Key;
        return systemLanguageIdentifier ?? defaultLanguage;
    }

    public string ReplaceVariables(string translation) {
        if (!translation.Contains("{") || !translation.Contains("}")) {
            return translation;
        }

        int start = translation.IndexOf("{");
        int end = translation.IndexOf("}", start);
        string identifier = translation.Substring(start + 1, end - start - 1);
        string identifierToReplace = new StringBuilder()
            .Append("{")
            .Append(identifier)
            .Append("}")
            .ToString();

        string variableTranslation = GetTranslation(identifier);
        translation = translation.Replace(identifierToReplace, variableTranslation);

        translation = ReplaceVariables(translation);

        return translation;
    }

    private void LoadLanguages() {
        Languages = new Dictionary<string, Dictionary<string, string>>();
        LanguagesIdentifier = new List<string>();

        foreach (string line in languageFileContent.text.Split('\n')) {
            LoadRecord(line);
        }
    }

    private void LoadRecord(string line) {
        string[] values = line.Split(';');

        if (LanguagesIdentifier.Count == 0) {
            AddLanguagesIdentifier(values);
        } else {
            AddTranslationForLanguages(values);
        }
    }

    private void AddLanguagesIdentifier(string[] identifiers) {
        for (int i = 1; i < identifiers.Length; i++) {
            string identifier = identifiers[i].Trim();
            LanguagesIdentifier.Add(identifier);
            Languages.Add(identifier, new Dictionary<string, string>() { { languageIdentifier, identifier } });
        }
    }

    private void AddTranslationForLanguages(string[] translationValues) {
        string translationIdentifier = translationValues[0];
        for (int i = 0; i < LanguagesIdentifier.Count; i++) {
            string languageIdentifier = LanguagesIdentifier[i];
            string translationValue = translationValues[i + 1].Trim();
            Languages[languageIdentifier].Add(translationIdentifier, translationValue);
        }
    }

    private string GetPureTranslation(string translationIdentifier) {
        if (CurrentLanguage.ContainsKey(translationIdentifier.Trim())) {
            return CurrentLanguage[translationIdentifier];
        } else {
            return string.Format(noTranslationFound, translationIdentifier);
        }
    }
}
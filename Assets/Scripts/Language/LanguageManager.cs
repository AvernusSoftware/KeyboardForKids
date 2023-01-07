using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour {

    [SerializeField]
    private TextAsset languageFileContent;

    [SerializeField]
    private Image flagImage;

    public LanguageName CurrentLanguageName { get; private set; }

    private SoundManager SoundManager { get; set; }
    private Dictionary<LanguageName, Dictionary<string, string>> Languages { get; set; }
    private List<LanguageName> LanguageNames { get; set; }
    private Dictionary<string, string> CurrentLanguage { get; set; }

    private const LanguageName defaultLanguage = LanguageName.English;
    private const string languageIdentifier = "languageIdentifier";
    private const string twoletterisolanguagename = "twoletterisolanguagename";
    private const string noTranslationFound = "No translation definition for [{0}].";

    private void Start() {
        SetupObjects();
        LoadLanguages();
        SetLanguage(LanguageName.unknown);
    }

    public void SetLanguage(LanguageName languageName) {
        if (languageName == LanguageName.unknown) {
            CurrentLanguageName = DetectLanguage();
        } else if (Languages.ContainsKey(CurrentLanguageName)) {
            CurrentLanguageName = languageName;
        } else {
            CurrentLanguageName = defaultLanguage;
        }

        CurrentLanguage = Languages[CurrentLanguageName];
        SoundManager.SetupKeyboardSoundConfig(CurrentLanguageName);
        flagImage.sprite = SoundManager.KeyboardSoundsConfig.FlagGraphic;
    }

    public string GetTranslation(string translationIdentifier) {
        string pureTranslation = GetPureTranslation(translationIdentifier);
        string translation = pureTranslation.Replace("[br]", Environment.NewLine);
        string translationWithVariables = ReplaceVariables(translation);

        return translationWithVariables;
    }

    public List<LanguageName> GetAllLanguagesTypes() {
        return LanguageNames;
    }

    public LanguageName GetNextLanguageName() {
        LanguageName[] languagesNames = (LanguageName[])Enum.GetValues(typeof(LanguageName));
        int i = Array.IndexOf(languagesNames, CurrentLanguageName);

        do {
            i = (i + 1) % languagesNames.Length;
        }
        while ((int)languagesNames[i] < 0);

        return languagesNames[i];
    }

    public LanguageName DetectLanguage() {
        CultureInfo cultureInfo = CultureInfo.InstalledUICulture;
        KeyValuePair<LanguageName, Dictionary<string, string>> result = Languages.FirstOrDefault(x => x.Value[twoletterisolanguagename] == cultureInfo.TwoLetterISOLanguageName);

        if (result.Equals(default(KeyValuePair<LanguageName, Dictionary<string, string>>))) {
            return defaultLanguage;
        }

        return result.Key;
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

    private void SetupObjects() {
        SoundManager = FindObjectOfType<SoundManager>();
    }

    private void LoadLanguages() {
        Languages = new Dictionary<LanguageName, Dictionary<string, string>>();
        LanguageNames = new List<LanguageName>();

        foreach (string line in languageFileContent.text.Split('\n')) {
            LoadRecord(line);
        }
    }

    private void LoadRecord(string line) {
        string[] values = line.Split(';');

        if (LanguageNames.Count == 0) {
            AddLanguagesIdentifier(values);
        } else {
            AddTranslationForLanguages(values);
        }
    }

    private void AddLanguagesIdentifier(string[] identifiers) {
        for (int i = 1; i < identifiers.Length; i++) {
            string identifier = identifiers[i].Trim();
            LanguageName languageType = (LanguageName)Enum.Parse(typeof(LanguageName), identifier);
            LanguageNames.Add(languageType);
            Languages.Add(languageType, new Dictionary<string, string>() { { languageIdentifier, identifier } });
        }
    }

    private void AddTranslationForLanguages(string[] translationValues) {
        string translationIdentifier = translationValues[0];
        for (int i = 0; i < LanguageNames.Count; i++) {
            LanguageName languageIdentifier = LanguageNames[i];
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
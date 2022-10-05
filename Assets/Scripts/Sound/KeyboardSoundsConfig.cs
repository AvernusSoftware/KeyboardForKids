using UnityEngine;

[CreateAssetMenu(fileName = "KeyboardSoundsConfig", menuName = "Keyboard sounds config", order = 2000)]
public class KeyboardSoundsConfig : ScriptableObject {

    [SerializeField]
    private LanguageName language;

    [SerializeField]
    private LetterSoundConfig[] letterSounds = new LetterSoundConfig[26];

    private int AsciiCodeLetterA = 97;
    private int lettersCount = 26;

    private KeyboardSoundsConfig() {
        for (int i = 0; i < lettersCount; i++) {
            char key = (char)(i + AsciiCodeLetterA);
            letterSounds.SetValue(new LetterSoundConfig(key.ToString()), i);
        }
    }

    public LanguageName Language { get => language; }
}
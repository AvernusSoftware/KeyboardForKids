using UnityEngine;

[CreateAssetMenu(fileName = "KeyboardSoundsConfig", menuName = "Keyboard sounds config", order = 2000)]
public class KeyboardSoundsConfig : ScriptableObject {

    [SerializeField]
    private LanguageName language;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private LetterSoundConfig[] letterSounds = new LetterSoundConfig[26];

    private readonly int asciiCodeLetterA = 97;
    private readonly int lettersCount = 26;

    private KeyboardSoundsConfig() {
        for (int i = 0; i < lettersCount; i++) {
            char key = (char)(i + asciiCodeLetterA);
            string keyString = key.ToString();
            letterSounds.SetValue(new LetterSoundConfig(keyString, 0, 0), i);
        }
    }

    public LetterSoundConfig GetLetterConfig(string key) {
        foreach (LetterSoundConfig letterSound in letterSounds) {
            if (key == letterSound.Letter) {
                return letterSound;
            }
        }

        return null;
    }

    public LanguageName Language { get => language; }
    public AudioClip AudioClip { get => audioClip; }
}
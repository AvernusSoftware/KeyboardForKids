using UnityEngine;

[CreateAssetMenu(fileName = "KeyboardSoundConfig", menuName = "Keyboard sound config", order = 2000)]
public class KeyboardSoundConfig : ScriptableObject {

    [SerializeField]
    private LetterSoundConfig[] letterSounds = new LetterSoundConfig[26];

    private int AsciiCodeLetterA = 97;
    private int lettersCount = 26;

    private KeyboardSoundConfig() {
        for (int i = 0; i < lettersCount; i++) {
            char key = (char)(i + AsciiCodeLetterA);
            letterSounds.SetValue(new LetterSoundConfig(key.ToString()), i);
        }
    }
}
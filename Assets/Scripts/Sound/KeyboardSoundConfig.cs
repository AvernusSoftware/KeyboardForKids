using UnityEngine;

public class KeyboardSoundConfig : MonoBehaviour {
    public LanguageName languageType;
    public KeyboardSoundsConfig keyboardSoundsConfig;

    public KeyboardSoundConfig(KeyboardSoundsConfig keyboardSoundsConfig) {
        this.keyboardSoundsConfig = keyboardSoundsConfig;
    }
}
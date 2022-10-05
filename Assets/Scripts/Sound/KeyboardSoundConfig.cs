using UnityEngine;

public class KeyboardSoundConfig : MonoBehaviour {
    public LanguageType languageType;
    public KeyboardSoundsConfig keyboardSoundsConfig;

    public KeyboardSoundConfig(KeyboardSoundsConfig keyboardSoundsConfig) {
        this.keyboardSoundsConfig = keyboardSoundsConfig;
    }
}
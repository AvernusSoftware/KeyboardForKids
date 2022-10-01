using System;
using UnityEngine;

[Serializable]
public class LetterSoundConfig {
    public string letter;
    public AudioClip audio;

    public LetterSoundConfig(string letter) {
        this.letter = letter;
    }
}
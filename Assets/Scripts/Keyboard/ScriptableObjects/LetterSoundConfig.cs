using System;
using UnityEngine;

[Serializable]
public class LetterSoundConfig {

    [SerializeField]
    private string letter;

    [SerializeField]
    private float time;

    [SerializeField]
    private float length;

    public string Letter { get => letter; }

    public float Time { get => time; }

    public float Length { get => length; }

    public LetterSoundConfig(string letter, float time, float length) {
        this.letter = letter;
        this.time = time;
        this.length = length;
    }
}
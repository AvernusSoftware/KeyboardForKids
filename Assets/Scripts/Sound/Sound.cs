using UnityEngine;

public class Sound {
    public AudioClip AudioClip { get; private set; }
    public Vector3 PositionOnScene { get; private set; }
    public SoundType SoundType { get; private set; }
    public float StartTime { get; private set; }
    public float Length { get; private set; }

    public Sound(AudioClip audioClip, SoundType soundType) {
        AudioClip = audioClip;
        SoundType = soundType;
        Length = audioClip.length;
    }

    public Sound(AudioClip audioClip, SoundType soundType, Vector3 positionOnScene) : this(audioClip, soundType) {
        PositionOnScene = positionOnScene;
    }

    public Sound(AudioClip audioClip, SoundType soundType, Vector3 positionOnScene, float startTime, float length) : this(audioClip, soundType, positionOnScene) {
        StartTime = startTime;
        Length = length;
    }
}
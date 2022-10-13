using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [HideInInspector]
    public MusicManager musicManager;

    [SerializeField]
    private List<AudioClip> musicList;

    [SerializeField]
    private int musicBuforSize;

    [SerializeField]
    private float fadeInTime;

    [SerializeField]
    private float fadeOutTime;

    private List<AudioClip> LastMusicList { get; set; }
    private AudioSource AudioSource { get; set; }
    private float MusicVolume { get; set; }
    private bool PrevApplicationFocused { get; set; }
    private float FadeInStartTime { get; set; }
    private float FadeOutStartTime { get; set; }
    private MusicState MusicState { get; set; }
    private MusicState ForceFade { get; set; }

    public void Awake() {
        AudioSource = GetComponent<AudioSource>();
        MusicVolume = 0.3f;

        if (musicManager == null) {
            musicManager = this;
        } else if (musicManager != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LastMusicList = new List<AudioClip>();
        MusicState = MusicState.Normal;
        ForceFade = MusicState.FadeIn;
    }

    public void Update() {
        if (!Application.isFocused) {
            AudioSource.Pause();
            PrevApplicationFocused = Application.isFocused;
            return;
        }

        if (!PrevApplicationFocused && Application.isFocused) {
            AudioSource.volume = MusicVolume;
            AudioSource.Play();
        }

        if (AudioSource.isPlaying) {
            CheckMusicFadeInFadeOut();
            PrevApplicationFocused = Application.isFocused;
            return;
        }

        GetRandomMusic();
        PrevApplicationFocused = Application.isFocused;
    }

    public void SetVolume(float value) {
        MusicVolume = value;
        AudioSource.volume = MusicVolume;
    }

    private void CheckMusicFadeInFadeOut() {
        // fadeIn (pocz¹tek utworu lub wymuszony)
        if ((AudioSource.time < fadeInTime || ForceFade == MusicState.FadeIn) && MusicState != MusicState.FadeIn) {
            FadeInStartTime = AudioSource.time;
            MusicState = MusicState.FadeIn;
        }
        // fadeOut (koniec utworu lub wymuszony)
        else if ((AudioSource.time > AudioSource.clip.length - fadeOutTime || ForceFade == MusicState.FadeOut) && MusicState != MusicState.FadeOut) {
            FadeOutStartTime = AudioSource.time;
            MusicState = MusicState.FadeOut;
        }
        // fadeIn trwa wystarczaj¹co d³ugo, zatem mo¿na znieœæ wymuszenie
        else if (AudioSource.time - FadeInStartTime > fadeInTime && ForceFade == MusicState.FadeIn) {
            ForceFade = MusicState.Normal;
        }
        // fadeOut trwa wystarczaj¹co d³ugo, zatem zatrzymujê muzykê
        else if (AudioSource.time - FadeOutStartTime > fadeOutTime && ForceFade == MusicState.FadeOut) {
            ForceFade = MusicState.FadeIn;
            AudioSource.Stop();
        }
        // stan normalny - œrodek utworu
        else if (AudioSource.time >= fadeInTime && AudioSource.time <= AudioSource.clip.length - fadeOutTime && ForceFade == MusicState.Normal) {
            MusicState = MusicState.Normal;
        }

        if (MusicState == MusicState.FadeIn) {
            AudioSource.volume = MusicVolume * (AudioSource.time - FadeInStartTime) / fadeInTime;
        }

        if (MusicState == MusicState.FadeOut) {
            AudioSource.volume = MusicVolume * (fadeOutTime - AudioSource.time + FadeOutStartTime) / fadeOutTime;
        }
    }

    private void GetRandomMusic() {
        int random;
        while (1 == 1) {
            random = Random.Range(0, musicList.Count);
            if (!LastMusicList.Contains(musicList[random])) {
                break;
            }
        }

        AudioSource.clip = musicList[random];
        AudioSource.Play();
        AudioSource.time = 0;

        LastMusicList.Add(musicList[random]);
        if (LastMusicList.Count > musicBuforSize) {
            LastMusicList.RemoveAt(0);
        }
    }
}
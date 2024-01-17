using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    private GameObject soundObjectPrefab;

    [SerializeField]
    private List<KeyboardSoundsConfig> keyboardSoundsConfigs;

    public KeyboardSoundsConfig KeyboardSoundsConfig { get; private set; }

    private Dictionary<AudioClip, float> SoundHistory { get; set; }

    public void Start() {
        SetupObjects();
        DontDestroyOnLoad(gameObject);
    }

    public void Update() {
        UpdateSoundHistory();
    }

    public void AddNewSound(Sound sound) {
        AudioClip audioClip = sound.AudioClip;
        if (audioClip == null) {
            return;
        }

        if (!SoundHistory.ContainsKey(audioClip)) {
            SoundHistory.Add(audioClip, 0);
        }

        StartCoroutine(CreateNewSound(sound));
        SoundHistory[audioClip] += 0.1f;
    }

    public void PlayVoice(string key) {
        LetterSoundConfig letterSoundConfig = KeyboardSoundsConfig.GetLetterConfig(key);
        if (letterSoundConfig == null) {
            return;
        }

        AudioClip audioClip = KeyboardSoundsConfig.AudioClip;
        Sound sound = new(audioClip, SoundType.voice, Vector3.zero, letterSoundConfig.Time, letterSoundConfig.Length);
        AddNewSound(sound);
    }

    public void PauseSounds() {
        AudioSource[] audioSources = (AudioSource[])Resources.FindObjectsOfTypeAll(typeof(AudioSource));
        IEnumerable<AudioSource> audioSourcesPlaying = audioSources.Where(x => x.isPlaying && x.name != "MusicManager");
        foreach (AudioSource audioSource in audioSourcesPlaying) {
            audioSource.Pause();
        }
    }

    public void ResumeSounds() {
        AudioSource[] audioSources = (AudioSource[])Resources.FindObjectsOfTypeAll(typeof(AudioSource));
        IEnumerable<AudioSource> audioSourcesStoped = audioSources.Where(x => x.time > 0 && !x.isPlaying && x.name != "MusicManager");
        foreach (AudioSource audioSource in audioSourcesStoped) {
            audioSource.Play();
        }
    }

    public void SetupKeyboardSoundConfig(LanguageName currentLanguageName) {
        KeyboardSoundsConfig = keyboardSoundsConfigs.FirstOrDefault(x => x.Language == currentLanguageName);
    }

    private void SetupObjects() {
        SoundHistory = new Dictionary<AudioClip, float>();
    }

    private void UpdateSoundHistory() {
        foreach (AudioClip audioClip in SoundHistory.Keys.ToList()) {
            SoundHistory[audioClip] -= Time.deltaTime;
            if (SoundHistory[audioClip] < 0) {
                SoundHistory[audioClip] = 0;
            }
        }
    }

    private IEnumerator CreateNewSound(Sound sound) {
        yield return new WaitForSeconds(0);

        GameObject soundGameObject = Instantiate(soundObjectPrefab, Vector3.zero, Quaternion.identity);
        Transform soundTransform = soundGameObject.transform;
        soundTransform.position = sound.PositionOnScene;

        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;
        audioSource.clip = sound.AudioClip;
        audioSource.time = sound.StartTime;
        audioSource.volume = GetVolume(sound.SoundType);
        audioSource.Play();

        Destroy(soundGameObject, sound.Length);
    }

    private float GetVolume(SoundType soundType) {
        if (soundType == SoundType.effects) {
            return 0.6f;
        } else {
            return 1f;
        }
    }
}
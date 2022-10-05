using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    private List<KeyboardSoundsConfig> keyboardSoundsConfigs;

    private LanguageManager LanguageManager { get; set; }
    private KeyboardSoundsConfig KeyboardSoundsConfig { get; set; }
    private GameObject SoundObjectPrefab { get; set; }
    private Dictionary<AudioClip, float> SoundHistory { get; set; }

    public void Start() {
        SetupObjects();
        SetupKeyboardSoundConfig();
        DontDestroyOnLoad(gameObject);
    }

    public void Update() {
        UpdateSoundHistory();
    }

    public void AddNewSound(AudioClip audioClip, Vector3 position, float fadeTime = 0) {
        if (audioClip == null) {
            return;
        }

        if (!SoundHistory.ContainsKey(audioClip)) {
            SoundHistory.Add(audioClip, 0);
        }

        StartCoroutine(CreateNewSound(SoundHistory[audioClip], audioClip, position, audioClip.length, fadeTime));
        SoundHistory[audioClip] += 0.1f;
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

    private void SetupObjects() {
        LanguageManager = FindObjectOfType<LanguageManager>();
        SoundObjectPrefab = (GameObject)Resources.Load("Prefabs/SoundObject", typeof(GameObject));
        SoundHistory = new Dictionary<AudioClip, float>();
    }

    private void SetupKeyboardSoundConfig() {
        LanguageName currentLanguageName = LanguageManager.CurrentLanguageName;
        KeyboardSoundsConfig = keyboardSoundsConfigs.FirstOrDefault(x => x.Language == currentLanguageName);
    }

    private void UpdateSoundHistory() {
        foreach (AudioClip audioClip in SoundHistory.Keys.ToList()) {
            SoundHistory[audioClip] -= Time.deltaTime;
            if (SoundHistory[audioClip] < 0) {
                SoundHistory[audioClip] = 0;
            }
        }
    }

    private IEnumerator CreateNewSound(float time, AudioClip audioClip, Vector3 position, float length, float fadeTime) {
        yield return new WaitForSeconds(0);

        GameObject soundGameObject = Instantiate(SoundObjectPrefab, Vector3.zero, Quaternion.identity);
        Transform soundTransform = soundGameObject.transform;
        soundTransform.position = position;

        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.ignoreListenerPause = true;
        audioSource.Play();

        if (fadeTime > 0) {
            length = length >= fadeTime ? length : fadeTime;
            StartCoroutine(FadeSound(audioSource, length - fadeTime, fadeTime));
        }

        Destroy(soundGameObject, length);
    }

    private IEnumerator FadeSound(AudioSource audioSource, float timeToStartFade, float fadeTime) {
        yield return new WaitForSeconds(timeToStartFade);

        float t = fadeTime;
        while (t > 0) {
            yield return null;
            t -= Time.deltaTime;
            if (audioSource != null) {
                audioSource.volume = (t / fadeTime);
            }
        }
        yield break;
    }
}
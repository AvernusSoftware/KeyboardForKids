using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreValueText;
    [SerializeField] private TextMeshProUGUI scoreInRowValueText;

    [SerializeField] private AudioClip goodAnswerSound;
    [SerializeField] private AudioClip badAnswerSound;

    private AudioSource audioSource;
    private int numberOfAttemps;
    private int scoreOverall;
    private int scoreInRow;

    //todo: need to change to using SoundManager
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    internal void GoodAnswer() {
        numberOfAttemps++;
        scoreOverall++;
        scoreInRow++;
        RefreshScoreText();
        audioSource.clip = goodAnswerSound;
        audioSource.Play();
    }

    internal void BadAnswer() {
        numberOfAttemps++;
        scoreInRow = 0;
        RefreshScoreText();
        audioSource.clip = badAnswerSound;
        audioSource.Play();
    }

    private void RefreshScoreText() {
        scoreValueText.text = $"{scoreOverall} / {numberOfAttemps}";
        scoreInRowValueText.text = scoreInRow.ToString();
    }
}
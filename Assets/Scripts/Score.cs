using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreValueText;
    [SerializeField] private TextMeshProUGUI scoreInRowValueText;

    [SerializeField] private AudioClip goodAnswerSound;
    [SerializeField] private AudioClip badAnswerSound;

    private SoundManager SoundManager { get; set; }

    private int numberOfAttemps;
    private int scoreOverall;
    private int scoreInRow;

    private void Start() {
        SoundManager = FindObjectOfType<SoundManager>();
    }

    internal void GoodAnswer() {
        numberOfAttemps++;
        scoreOverall++;
        scoreInRow++;
        RefreshScoreText();
        Sound sound = new(goodAnswerSound, SoundType.effects);
        SoundManager.AddNewSound(sound);
    }

    internal void BadAnswer() {
        numberOfAttemps++;
        scoreInRow = 0;
        RefreshScoreText();
        Sound sound = new(badAnswerSound, SoundType.effects);
        SoundManager.AddNewSound(sound);
    }

    private void RefreshScoreText() {
        scoreValueText.text = $"{scoreOverall} / {numberOfAttemps}";
        scoreInRowValueText.text = scoreInRow.ToString();
    }
}
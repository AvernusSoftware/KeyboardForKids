using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreValueText;
    [SerializeField] private TextMeshProUGUI scoreInRowValueText;

    private int numberOfAttemps;
    private int scoreOverall;
    private int scoreInRow;

    internal void GoodAnswer() {
        numberOfAttemps++;
        scoreOverall++;
        scoreInRow++;
        RefreshScoreText();
    }

    internal void BadAnswer() {
        numberOfAttemps++;
        scoreInRow = 0;
        RefreshScoreText();
    }

    private void RefreshScoreText() {
        scoreValueText.text = $"{scoreOverall} / {numberOfAttemps}";
        scoreInRowValueText.text = scoreInRow.ToString();
    }
}
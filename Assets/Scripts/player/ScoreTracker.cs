using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    private const string SCORE_PREFIX = "Score:\n";
    public int HighScore { get; private set; } = 0;
    private int score = 0;
    private Text scoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    public void UpdateScore(int diff)
    {
        this.score += diff;
        this.HighScore = Mathf.Max(score, HighScore);
        scoreText.text = SCORE_PREFIX + score;
    }

    public void ResetScore()
    {
        this.score = 0;
        scoreText.text = SCORE_PREFIX + score;
    }
}

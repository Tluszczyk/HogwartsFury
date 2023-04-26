using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    private const string SCORE_PREFIX = "Score:\n";
    private int score;
    private Text scoreText;

    void Start()
    {
        Debug.Log("HI");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }
    public void UpdateScore(int diff)
    {
        this.score += diff;
        scoreText.text = SCORE_PREFIX + score;
    }
}

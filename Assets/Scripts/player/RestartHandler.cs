using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartHandler : MonoBehaviour
{
    private const string HIGHSCORE_PREFIX = "HIGH SCORE: ";
    private GameObject scoreBoard;
    private GameObject endMenu;
    private Text highScoreText;
    private HealthBehaviour playerHealth;
    private Transform playerTransform;
    private ScoreTracker scoreTracker;

    void Start()
    {
        var player = GameObject.Find("Player");
        scoreBoard = GameObject.Find("Scoreboard");
        endMenu = GameObject.Find("EndMenu");
        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        playerHealth = player.GetComponent<HealthBehaviour>();
        playerTransform = player.transform;
        scoreTracker = player.GetComponent<ScoreTracker>();
        endMenu.SetActive(false);
    }

    public void CheckDeathCondition()
    {
        if (playerHealth.isDead())
        {
            UpdateEndMenu();
        }
    }

    void UpdateEndMenu()
    {
        if (playerHealth.isDead())
        {
            highScoreText.text = HIGHSCORE_PREFIX + scoreTracker.HighScore;
            scoreBoard.SetActive(false);
            endMenu.SetActive(true);
        }
        else
        {
            scoreBoard.SetActive(true);
            endMenu.SetActive(false);
        }
    }

    public void Restart()
    {
        DestroyAll(GameObject.FindGameObjectsWithTag("Opponent"));
        DestroyAll(GameObject.FindGameObjectsWithTag("Bullet"));
        DestroyAll(GameObject.FindGameObjectsWithTag("Chest"));
        scoreTracker.ResetScore();
        playerHealth.SetMaxHealth();
        playerTransform.position = Vector2.zero;
        UpdateEndMenu();
    }

    private void DestroyAll(GameObject[] objects)
    {
        foreach (var gameObject in objects)
        {
            Destroy(gameObject);
        }
    }
}

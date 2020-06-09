using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] int playerLives = 3;
    [SerializeField] float playerRespawnDelay = 1f;
    [SerializeField] int score = 0;

    [Header("UI Refs")]
    [SerializeField] Text livesText = null;
    [SerializeField] Text scoreText = null;

    private void Awake()
    {
        int gameSessionNum = FindObjectsOfType<GameSession>().Length;

        if (gameSessionNum > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    private IEnumerator TakeLife()
    {
        yield return new WaitForSecondsRealtime(playerRespawnDelay);

        playerLives--;

        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(playerRespawnDelay);

        SceneManager.LoadScene(0);

        Destroy(gameObject);
    }

    public void AddToScore(int amount)
    {
        score += amount;

        scoreText.text = score.ToString();
    }
}

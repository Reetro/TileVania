using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] float playerRespawnDelay = 1f;

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(playerRespawnDelay);

        SceneManager.LoadScene(0);

        Destroy(gameObject);
    }
}

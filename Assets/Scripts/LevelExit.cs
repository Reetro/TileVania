using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        bool goBackToStart = currentSceneIndex >= SceneManager.sceneCountInBuildSettings;

        if (!goBackToStart)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}

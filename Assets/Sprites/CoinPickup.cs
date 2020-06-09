using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip audioClip = null;
    [SerializeField] int scoreToAdd = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);

        FindObjectOfType<GameSession>().AddToScore(scoreToAdd);

        Destroy(gameObject);
    }
}

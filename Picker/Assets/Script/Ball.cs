using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallCounter"))
        {
            gameManager.BallCounter();
        }
    }
}

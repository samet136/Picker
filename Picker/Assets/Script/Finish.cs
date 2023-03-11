using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finish : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BurderObject"))
        {
            gameManager.BorderReached();
            gameManager.Sounds[1].Play();
        }
    }
}

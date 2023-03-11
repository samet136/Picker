using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBall : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)  // Big ball bonus
    {
        if (other.CompareTag("BurderObject"))
        {
            gameObject.SetActive(false);
            gameManager.BringSwivelArm();
            gameManager.Sounds[0].Play();
        }
    }
}

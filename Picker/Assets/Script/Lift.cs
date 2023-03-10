using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator BarrierArea;
    public void BarrierRaise() // Lifting the barriers
    {
        BarrierArea.Play("BarrierRaise");
    }
    public void Finish()
    {
        gameManager.PickerMove = true;
    }

}

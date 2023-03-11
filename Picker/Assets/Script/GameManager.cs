using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Picker;
    [SerializeField] private GameObject[] SwivelArm;
    [SerializeField] private GameObject BollControllerObject;
    public AudioSource[] Sounds;
    public bool Pallet;
    public bool PickerMove;
    [SerializeField] private float PickerSpeed;
    [SerializeField] private int BallThrown;
    [SerializeField] private int TotalCheckNumber;
    [SerializeField] private int AvailableCheckIndex;
    [SerializeField] private List<BallAreaTechnicalOperations> ballAreaTechnicalOperations = new List<BallAreaTechnicalOperations>();
    [SerializeField] private float Force;
    [SerializeField] private float gravity;
    float FingerPozX;
    void Start()
    {
        Physics.gravity = new Vector3(0, -gravity, 0);
        PickerMove = true;
        CheckPointText();
    }
    void Update()
    {
        DirectionMovement();   //  Right-left movement of the collector with the finger
        PickerControl();      //  Limits to which the picker can move
    }


    public void BorderReached()  // When it reaches the limit, it applies force to the balls in the area inside the pickup
    {
        if (Pallet)
        {
            SwivelArm[0].SetActive(false);
            SwivelArm[1].SetActive(false);
        }
        PickerMove = false;
        Invoke("LiftControl", 2f);
        Collider[] HitColl = Physics.OverlapBox(BollControllerObject.transform.position, BollControllerObject.transform.localScale / 2, Quaternion.identity);

        int i = 0;
        while (i < HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, Force), ForceMode.Impulse);
            i++;
        }
    }


    public void BallCounter()
    {
        BallThrown++;
        ballAreaTechnicalOperations[AvailableCheckIndex].NumberText.text = BallThrown + "/" + ballAreaTechnicalOperations[AvailableCheckIndex].TargetBall;
    }

    public void LiftControl()  // Controls the number of balls in the elevator
    {
        if (BallThrown >= ballAreaTechnicalOperations[AvailableCheckIndex].TargetBall)
        {

            ballAreaTechnicalOperations[AvailableCheckIndex].BallAreaLift.Play("Lift");
            foreach (var item in ballAreaTechnicalOperations[AvailableCheckIndex].Balls)
            {
                item.SetActive(false);
            }
            if (AvailableCheckIndex == TotalCheckNumber)
            {
                Time.timeScale = 0;
            }
            else
            {
                AvailableCheckIndex++;
                BallThrown = 0;
                if (true)
                {
                    SwivelArm[0].SetActive(true);
                    SwivelArm[1].SetActive(true);
                }
            }
        }
        else
        {
            Sounds[2].Play();
            Time.timeScale = 0;
        }
    }
    public void BringSwivelArm()  // Reveals Swivel Arms
    {
        Pallet = true;
        SwivelArm[0].SetActive(true);
        SwivelArm[1].SetActive(true);
    }

    void PickerControl()
    {
        if (Picker.transform.position.x >= 1.15)
        {
            Picker.transform.position = new Vector3(1.15f, Picker.transform.position.y, Picker.transform.position.z);
        }
        if (Picker.transform.position.x < -1.15)
        {
            Picker.transform.position = new Vector3(-1.15f, Picker.transform.position.y, Picker.transform.position.z);
        }

    }

    void CheckPointText()
    {
        for (int i = 0; i < ballAreaTechnicalOperations.Count; i++)
        {
            ballAreaTechnicalOperations[i].NumberText.text = BallThrown + "/" + ballAreaTechnicalOperations[i].TargetBall;
        }
        TotalCheckNumber = ballAreaTechnicalOperations.Count - 1;
    }

    void DirectionMovement()
    {
        if (PickerMove)
        {
            Picker.transform.position += 5f * Time.deltaTime * Picker.transform.forward;
            if (Time.timeScale != 0)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    Vector3 TouchPosition = Camera.main.ScreenToViewportPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            FingerPozX = TouchPosition.x - Picker.transform.position.x;
                            break;
                        case TouchPhase.Moved:
                            if (TouchPosition.x - FingerPozX > -1.15 && TouchPosition.x - FingerPozX < 1.15)
                            {   
                                 Picker.transform.position = Vector3.Lerp(Picker.transform.position,
                                 new Vector3(TouchPosition.x - FingerPozX,
                                 Picker.transform.position.y, Picker.transform.position.z), 3f);
                            }
                            break;                        
                    }
                }
            }
        }
    }

}

[Serializable]
public class BallAreaTechnicalOperations // Checking the elevators in the list
{
    public Animator BallAreaLift;
    public TextMeshProUGUI NumberText;
    public int TargetBall;
    public GameObject[] Balls;
}

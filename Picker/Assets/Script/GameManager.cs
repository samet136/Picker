using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;


[Serializable]
public class BallAreaTechnicalOperations
{
    public Animator BallAreaLift;
    public TextMeshProUGUI NumberText;
    public int TargetBall;
    public GameObject[] Balls;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Picker;
    [SerializeField] private GameObject BollControllerObject;
    public bool PickerMove;
    [SerializeField] private float PickerSpeed;

    int BallThrown;
    int TotalCheckNumber;
    int AvailableCheckIndex;
    [SerializeField] private List<BallAreaTechnicalOperations> ballAreaTechnicalOperations = new List<BallAreaTechnicalOperations>();
    [Header("Acýklama")]
    public int sayi;
    public int a;
    public int b;
    public int c; 
    [Header("Acýklamsa")]
    public int sadyi;
    public int af;
    public int bsa;
    public int cdsa;
    void Start()
    {
        PickerMove = true;
        for (int i = 0; i < ballAreaTechnicalOperations.Count; i++)
        {
            ballAreaTechnicalOperations[i].NumberText.text = BallThrown + "/" + ballAreaTechnicalOperations[i].TargetBall;
        }       
        TotalCheckNumber = ballAreaTechnicalOperations.Count-1;
    }

    void Update()
    {
        if (PickerMove)
        {
            Picker.transform.position += 5f * Time.deltaTime * Picker.transform.forward;
            if (Time.timeScale != 0)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Picker.transform.position = Vector3.Lerp(Picker.transform.position, new Vector3
                   (Picker.transform.position.x - .1f, Picker.transform.position.y, Picker.transform.position.z), PickerSpeed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Picker.transform.position = Vector3.Lerp(Picker.transform.position, new Vector3
                   (Picker.transform.position.x + .1f, Picker.transform.position.y, Picker.transform.position.z), PickerSpeed);
                }
            }
        }
    }
    public void BorderReached()// Sýnýra gelýndýgýnde yapýlacak ýslemler
    {
        PickerMove = false;
        Invoke("LiftControl", 2f);
        Collider[] HitColl = Physics.OverlapBox(BollControllerObject.transform.position, BollControllerObject.transform.localScale/2,Quaternion.identity);

        int i = 0;
        while (i<HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 250f),ForceMode.Impulse);
            i++;
        }
    }
    

    public void BallCounter() 
    {
        BallThrown++;
        ballAreaTechnicalOperations[AvailableCheckIndex].NumberText.text = BallThrown + "/" + ballAreaTechnicalOperations[AvailableCheckIndex].TargetBall;
    }

    public void LiftControl()
    {
        if (BallThrown>= ballAreaTechnicalOperations[AvailableCheckIndex].TargetBall)
        {
           
            ballAreaTechnicalOperations[AvailableCheckIndex].BallAreaLift.Play("Lift");
            foreach (var item in ballAreaTechnicalOperations[AvailableCheckIndex].Balls)
            {
                item.SetActive(false);
            }
            if (AvailableCheckIndex==TotalCheckNumber)
            {
                Time.timeScale = 0;
                Debug.Log("Oyun Bitti");
            }
            else
            {
                AvailableCheckIndex++;
                BallThrown = 0;
            }
           
        }
        else
        {
            Debug.Log("kaybettin");
               
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(BollControllerObject.transform.position, BollControllerObject.transform.localScale);
    //}
}

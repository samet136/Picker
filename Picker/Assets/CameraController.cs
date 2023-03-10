using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 TargetOffset;



    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + TargetOffset, .125f);
    }}

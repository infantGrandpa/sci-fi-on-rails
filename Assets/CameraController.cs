using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void OnEnable()
    {
        References.theCamera = GetComponent<Camera>(); ;
    }
}

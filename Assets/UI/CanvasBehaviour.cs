using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{

    private void OnEnable()
    {
        References.theCanvas = this;
    }
}

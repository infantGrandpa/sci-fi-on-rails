using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{

    RectTransform rectangle;
    Image image;

    public UnityEvent eventToTrigger;

    void Start()
    {
        rectangle = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        //If the mouse is within our rectangle
        if (RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition))
        {
            image.color = Color.gray;

            //clicking should do something
            if (Input.GetButtonDown("Fire1"))
            {
                eventToTrigger.Invoke();
            }

        }
        else
        {
            image.color = Color.black;
        }

    }


}

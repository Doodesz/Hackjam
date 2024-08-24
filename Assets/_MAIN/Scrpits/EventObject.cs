using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    [SerializeField] GameObject objToOpen;

    public void TriggerEvent()
    {
        objToOpen.SetActive(false);
    }

    public void StartDrawingLine()
    {
        GetComponent<DrawLine>().isDrawing = true;
    }
}

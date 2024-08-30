using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    public void TriggerEvent()
    {
        PlayerSFX.Instance.PlayOpenWall();
        gameObject.SetActive(!gameObject.activeSelf);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShardColor { red, orange, yellow, green, blue, purple , white }

[RequireComponent(typeof(DrawLine))]
public class ShardBehaviour : MonoBehaviour
{
    public bool isConnected;
    public ShardColor thisShardColor;
    [SerializeField] List<EventObject> objectsToTriggers = new List<EventObject>();

    public void ConnectShard()
    {
        DrawLine line = GetComponent<DrawLine>();

        isConnected = true;
        line.isDrawing = false;
        foreach (EventObject obj in objectsToTriggers)
            obj.TriggerEvent();
        Debug.Log("Shard " + thisShardColor.ToString() + " connected");
    }
}

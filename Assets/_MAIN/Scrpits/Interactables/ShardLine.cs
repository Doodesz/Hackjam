using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Shard))]
public class ShardLine : MonoBehaviour
{
    public bool isDrawing;

    float initWidth;
    [SerializeField] float minDistance;
    [SerializeField] float currLength = 0;
    [SerializeField][Range(1f,50f)] float maxLength;
    [SerializeField] Shard shard;
    
    LineRenderer line;
    Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        shard = GetComponent<Shard>();

        prevPos = transform.position;
        line.positionCount = 0;
        initWidth = maxLength / 50;
        line.startWidth = initWidth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = PlayerController.Instance.gameObject.transform.position;
        float distance = Vector3.Distance(prevPos, currPos);

        if (isDrawing && !GetComponent<Shard>().isConnected)
        {
            if (distance > minDistance)
            {
                currLength += distance;
                prevPos = currPos;

                DrawLines();
            }
            if (currLength > maxLength)
            {
                SnapLine(true);
            }
        }
    }

    public void SnapLine(bool snapPlayerCurrShard = false, bool triggerEvent = false)
    {
        if (snapPlayerCurrShard)
            PlayerController.Instance.currShard = null;
        if (triggerEvent)
        {
            shard.TriggerEvents();
            shard.connectedShard.TriggerEvents();
        }
        
        ResetLine();

        shard.isConnected = false;
        shard.connectedShard = null;

        Debug.Log("Line snapped");
    }

    private void ResetLine()
    {
        isDrawing = false;
        currLength = 0;
        line.positionCount = 0;
        prevPos = transform.position;
    }

    private void DrawLines()
    {
        Vector3 currPos = PlayerController.Instance.gameObject.transform.position;

        line.positionCount++;
        line.SetPosition(line.positionCount - 1, currPos);
        line.endWidth = initWidth * ((maxLength - currLength) / maxLength);
    }

    public void FixLine(GameObject endLine)
    {
        line.Simplify(0.1f);
        line.SetPosition(0, transform.position);
        line.SetPosition(line.positionCount - 1, endLine.gameObject.transform.position);
    }
}

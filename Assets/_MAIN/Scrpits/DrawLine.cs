using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(ShardBehaviour))]
public class DrawLine : MonoBehaviour
{
    public bool isDrawing;

    [SerializeField] float minDistance;
    [SerializeField] float currLength = 0;
    [SerializeField] float maxLength;

    private LineRenderer line;
    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        prevPos = transform.position;
        line.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = PlayerController.Instance.gameObject.transform.position;
        float distance = Vector3.Distance(prevPos, currPos);

        if (isDrawing && !GetComponent<ShardBehaviour>().isConnected)
        {
            if (distance > minDistance)
            {
                currLength += distance;
                prevPos = currPos;

                DrawLines();
            }
            if (currLength > maxLength)
            {
                SnapLine();
            }
        }
    }

    void SnapLine()
    {
        PlayerController.Instance.currShard = null;
        Debug.Log("Line snapped");
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
    }

    public void FixLine(GameObject endLine)
    {
        line.Simplify(0.1f);
        line.SetPosition(0, transform.position);
        line.SetPosition(line.positionCount - 1, endLine.gameObject.transform.position);
    }
}

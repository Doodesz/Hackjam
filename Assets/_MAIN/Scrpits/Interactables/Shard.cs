using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShardLine))]
public class Shard : MonoBehaviour
{
    public bool isConnected;
    public bool canBeInteracted;
    public GameObject parent;

    [SerializeField] List<EventObject> objectsToTriggers = new List<EventObject>();
    Animator animator;
    ShardLine shardLine;

    private void Start()
    {
        animator = GetComponent<Animator>();
        shardLine = GetComponent<ShardLine>();
        parent = transform.parent.gameObject;
    }

    private void Update()
    {
        if (canBeInteracted && Input.GetKeyDown(KeyCode.F))
        {
            if (PlayerController.Instance.currShard == null)
            {
                PlayerController.Instance.currShard = this;
                shardLine.isDrawing = true;
            }
            else if (PlayerController.Instance.currShard == this)
            {
                shardLine.SnapLine();
            }
        }
    }

    public void ConnectShard()
    {
        ShardLine line = GetComponent<ShardLine>();

        isConnected = true;
        line.isDrawing = false;
        foreach (EventObject obj in objectsToTriggers)
            obj.TriggerEvent();
        Debug.Log("Shard from parent " + transform.parent.gameObject + " connected");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBeInteracted = true;
            animator.Play("show prompt");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBeInteracted = false;
            animator.Play("hide prompt");
        }
    }
}

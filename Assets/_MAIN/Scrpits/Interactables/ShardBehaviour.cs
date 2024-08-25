using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShardColor { red, orange, yellow, green, blue, purple , white }

[RequireComponent(typeof(DrawLine))]
public class ShardBehaviour : MonoBehaviour
{
    public bool isConnected;
    public bool canBeInteracted;
    public ShardColor shardColor;

    [SerializeField] List<EventObject> objectsToTriggers = new List<EventObject>();
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canBeInteracted && Input.GetKeyDown(KeyCode.F))
        {
            PlayerController.Instance.currShard = this;
            GetComponent<DrawLine>().isDrawing = true;
            animator.Play("hide prompt");
        }
    }

    public void ConnectShard()
    {
        DrawLine line = GetComponent<DrawLine>();

        isConnected = true;
        line.isDrawing = false;
        foreach (EventObject obj in objectsToTriggers)
            obj.TriggerEvent();
        Debug.Log("Shard " + shardColor.ToString() + " connected");
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

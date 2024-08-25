using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShardLine))]
public class Shard : MonoBehaviour
{
    public ShardColorEnum.ShardColor shardColor;
    public bool isConnected;
    public bool canBeInteracted;

    [SerializeField] List<EventObject> objectsToTriggers = new List<EventObject>();
    Animator animator;
    ShardLine shardLine;

    private void Start()
    {
        animator = GetComponent<Animator>();
        shardLine = GetComponent<ShardLine>();
        Debug.Log(shardColor.ToString());
    }

    private void Update()
    {
        if (canBeInteracted && Input.GetKeyDown(KeyCode.F) && PlayerController.Instance.currShard == null)
        {
            PlayerController.Instance.currShard = this;
            shardLine.isDrawing = true;
        }
        Debug.Log(shardColor.ToString());

    }

    public void ConnectShard()
    {
        ShardLine line = GetComponent<ShardLine>();

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

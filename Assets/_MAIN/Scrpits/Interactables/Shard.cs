using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShardLine))]
public class Shard : MonoBehaviour
{
    public bool isConnected;
    public Shard connectedShard;
    public bool canBeInteracted;
    public GameObject parent;

    [SerializeField] List<EventObject> objectsToTriggers = new List<EventObject>();
    Animator animator;
    ShardLine shardLine;
    //PlayerController playerController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        shardLine = GetComponent<ShardLine>();
        parent = transform.parent.gameObject;
        //playerController = PlayerController.Instance;
    }

    private void Update()
    {
        if (canBeInteracted && Input.GetKeyDown(KeyCode.F))
        {
            // If not currently drawing line, draw this shard's line
            if (!isConnected && PlayerController.Instance.currShard == null)
            {
                PlayerController.Instance.currShard = this;
                shardLine.isDrawing = true;
            }

            // If currently connected, snap it
            else if (isConnected && PlayerController.Instance.currShard == null)
            {
                connectedShard.shardLine.SnapLine(true);
                shardLine.SnapLine(true);
            }

            // Replaces currently connected shard with currShard
            else if (isConnected && PlayerController.Instance.currShard != null)
            {
                connectedShard.shardLine.SnapLine();
                shardLine.SnapLine();
                PlayerController.Instance.ConnectShards(this);
            }

            // If interacting with the same shard, snap it
            else if (PlayerController.Instance.currShard == this)
                shardLine.SnapLine(true);

            // Connect both shards
            else if (PlayerController.Instance.currShard != this)
                PlayerController.Instance.ConnectShards(this);
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

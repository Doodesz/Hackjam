using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    public bool isConnected;
    public Shard connectedShard;
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
            // If not currently drawing line, draw this shard's line
            if (!isConnected && PlayerController.Instance.currShard == null)
            {
                PlayerController.Instance.currShard = this;
                animator.SetTrigger("isDrawing");
                shardLine.isDrawing = true;
                MakeMatchingWallsIgnorePlayer();
            }

            // If currently connected, snap it
            else if (isConnected && PlayerController.Instance.currShard == null)
            {
                connectedShard.shardLine.SnapLine(true);
                connectedShard.animator.SetBool("isConnected", false);
                shardLine.SnapLine(true, true);
                animator.SetBool("isConnected", false);
                ResetAllWallsLayer();
            }

            // Replaces currently connected shard with currShard
            else if (isConnected && PlayerController.Instance.currShard != null
                && parent == PlayerController.Instance.currShard.parent)
            {
                connectedShard.shardLine.SnapLine(triggerEvent: true);
                connectedShard.animator.SetBool("isConnected", false);
                shardLine.SnapLine();
                //animator.SetBool("isConnected", false); still connected
                PlayerController.Instance.ConnectShards(this);
                ResetAllWallsLayer();
            }

            // If interacting with the same shard, snap it
            else if (PlayerController.Instance.currShard == this)
            {
                shardLine.SnapLine(true);
                animator.SetTrigger("isDrawing");
                ResetAllWallsLayer();
            }

            // Connect both shards
            else if (PlayerController.Instance.currShard != this
                && parent == PlayerController.Instance.currShard.parent)
            {
                PlayerController.Instance.currShard.animator.SetBool("isConnected", true);
                PlayerController.Instance.ConnectShards(this);
                animator.SetBool("isConnected", true);
                ResetAllWallsLayer();
            }
        }
    }

    public void MakeMatchingWallsIgnorePlayer()
    {
        SpecialWall[] specialWalls = GameObject.FindObjectsOfType<SpecialWall>(includeInactive: true);
        foreach (SpecialWall wall in specialWalls)
        {
            if (wall.transform.parent == this.gameObject.transform.parent)
                wall.gameObject.layer = 6;
        }
    }

    public void ResetAllWallsLayer()
    {
        SpecialWall[] specialWalls = GameObject.FindObjectsOfType<SpecialWall>(includeInactive: true);
        foreach (SpecialWall wall in specialWalls)
        {
            wall.gameObject.layer = 0;
        }
    }

    public void ConnectShard()
    {
        ShardLine line = GetComponent<ShardLine>();

        isConnected = true;
        line.isDrawing = false;
        TriggerEvents();
        GameManager.Instance.UpdateProgress();

        Debug.Log("Shard from parent " + transform.parent.gameObject + " connected");
    }

    public void TriggerEvents()
    {
        foreach (EventObject obj in objectsToTriggers)
            obj.TriggerEvent();
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

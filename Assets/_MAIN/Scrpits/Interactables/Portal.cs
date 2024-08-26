using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject worldToEnter;

    [SerializeField] bool canBeInteracted = false;

    Animator animator;
    WorldManager worldManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        worldManager = WorldManager.Instance;

        if (!worldToEnter.CompareTag("world")) 
            Debug.LogWarning("Target world is not tagged as world, may result in unexpected behaviour");
    }

    private void Update()
    {
        if (canBeInteracted && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Entered portal");
            
            worldManager.EnterWorld(worldToEnter, name);

            if (PlayerController.Instance.currShard !=  null)
            {
                PlayerController.Instance.currShard.GetComponent<ShardLine>().SnapLine();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBeInteracted = true;

            if (gameObject.activeInHierarchy)
                animator.Play("show prompt");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //activeinhierarchy to avoid error logs
        {
            canBeInteracted = false;

            if (gameObject.activeInHierarchy)
                animator.Play("hide prompt");
        }
    }
}

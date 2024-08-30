using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isEnabled;

    [SerializeField] List<EventObject> objectsToTrigger = new List<EventObject>();
    [SerializeField] bool canBeInteracted;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canBeInteracted && Input.GetKeyDown(KeyCode.F) && !PlayerController.Instance.isIgnoringInput)
        {
            isEnabled = !isEnabled;
            animator.SetBool("isEnabled", isEnabled);

            TriggerEvent();
        }

        
    }

    private void TriggerEvent()
    {
        PlayerSFX.Instance.PlayLever();
        foreach (EventObject obj in objectsToTrigger)
        {
            obj.TriggerEvent();
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    [SerializeField] bool canBeInteracted;
    [SerializeField] bool isOpen;
    
    Animator animator;

    public static EndPortal Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (isOpen)
            animator.SetBool("isOpened", true);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canBeInteracted && Input.GetKeyDown(KeyCode.F))
        {
            if (isOpen)
            {
                Debug.Log("Go to next level");
                // go to next level
            }
        }
    }


    public void OpenPortal()
    {
        isOpen = true;
        if (isActiveAndEnabled)
            animator.SetBool("isOpened", true);
        Debug.Log("End Portal opened");
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

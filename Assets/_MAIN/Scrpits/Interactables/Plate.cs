using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] bool isPressed;
    [SerializeField] List<EventObject> objectsToTrigger = new List<EventObject>();
    [SerializeField] List<GameObject> objectsOnPlate = new List<GameObject>();

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void TriggerEvent()
    {
        foreach (EventObject obj in objectsToTrigger)
        {
            obj.TriggerEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("box"))
        {
            if (!isPressed)
            {
                isPressed = true;
                TriggerEvent();
                animator.Play("pressed");
            }

            // Adds current object to a list of objects pressing the plate
            objectsOnPlate.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("box"))
        {
            // Removes exited object from the list of objects pressing the plate
            for (int i = 0; i < objectsOnPlate.Count; i++)
            {
                if (objectsOnPlate[i] == collision.gameObject) objectsOnPlate.Remove(objectsOnPlate[i]);
            }

            // If no box or player is on the plate, release the plate
            if (objectsOnPlate.Count == 0)
            {
                TriggerEvent();
                animator.Play("released");
                isPressed = false;
            }
        }

    }
}

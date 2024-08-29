using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAnimationTrigger : MonoBehaviour
{
    Animator playerAnim;

    private void Start()
    {
        playerAnim = transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("box"))
        {
            playerAnim.SetBool("isPushing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("box"))
        {
            playerAnim.SetBool("isPushing", false);
        }
    }
}

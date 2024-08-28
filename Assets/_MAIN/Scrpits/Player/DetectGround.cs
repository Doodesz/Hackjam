using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground") || collision.CompareTag("box"))
        {
            player.isOnGround = true;
            player.anim.SetBool("isFalling", false);
            player.anim.SetBool("isOnGround", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ground") || collision.CompareTag("box"))
        {
            PlayerController.Instance.isOnGround = false;
            player.anim.SetBool("isOnGround", false);
        }
    }
}

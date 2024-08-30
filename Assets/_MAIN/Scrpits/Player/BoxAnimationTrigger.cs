using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxAnimationTrigger : MonoBehaviour
{
    public bool isNextToBox;
    Animator playerAnim;


    private void Start()
    {
        playerAnim = transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isNextToBox && PlayerController.Instance.isRunning && !PlayerSFX.Instance.isPlayingPush)
            PlayerSFX.Instance.PlayPush();
        else if (PlayerSFX.Instance.isPlayingPush)
            PlayerSFX.Instance.StopPush();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("box"))
        {
            playerAnim.SetBool("isPushing", true);
            isNextToBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("box"))
        {
            playerAnim.SetBool("isPushing", false);
            PlayerSFX.Instance.StopPush();
            isNextToBox = false;
        }
        else if (collision.CompareTag("special wall"))
        {
            PlayerSFX.Instance.PlaySpecialWall();
        }
    }
}

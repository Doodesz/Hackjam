using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWall : MonoBehaviour
{
    void ResetExcludeCollision()
    {

    }

    void ExcludePlayerCollision()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player") 
            && PlayerController.Instance.currShard.parent == collision.gameObject.transform.parent)
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWall : MonoBehaviour
{
    BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ResetLayer()
    {
        gameObject.layer = 0;
    }

    public void SetIgnorePlayerLayer()
    {
        gameObject.layer = 6;
    }
}

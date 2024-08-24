using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private float horizontalAxis;
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpd;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalAxis * moveSpd, rb.velocity.y);        
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }
}

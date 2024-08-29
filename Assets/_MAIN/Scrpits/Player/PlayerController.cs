using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public bool isIgnoringInput = false;
    public bool isOnGround;
    public Shard currShard;
    public Animator anim;

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpd;

    float horizontalAxis;
    Rigidbody2D rb;

    public static PlayerController Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        currShard = null;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump when up, w, or space is pressed
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            && isOnGround && !isIgnoringInput)
        {
            Jump();
        }

        if (rb.velocity.y < 0 && !isOnGround)
        {
            //anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        else if (rb.velocity.y !> 0.01f)
            anim.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    { 
        // Sets appropriate animation
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (horizontalAxis > 0 || horizontalAxis < 0)
        {
            anim.SetBool("isRunning", true);
            if (horizontalAxis > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            anim.SetBool("isRunning", false);

        // Moves player horizontally
        if (!isIgnoringInput)
            rb.velocity = new Vector3(moveSpd * horizontalAxis * Time.deltaTime, rb.velocity.y);
        else
            rb.velocity = Vector3.zero;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        anim.Play("jump");
        anim.SetBool("isJumping", true);
        anim.SetBool("isFalling", false);
    }

    public void ConnectShards(Shard otherShard)
    {
        otherShard.ConnectShard();
        currShard.ConnectShard();
        otherShard.connectedShard = currShard;
        currShard.connectedShard = otherShard;
        currShard.GetComponent<ShardLine>().FixLine(otherShard.gameObject);
        currShard = null;
    }
}

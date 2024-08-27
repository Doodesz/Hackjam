using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public bool isIgnoringInput = false;
    public bool isOnGround;
    public Shard currShard;

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpd;

    float horizontalAxis;
    Rigidbody2D rb;

    public static PlayerController Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        currShard = null;
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

    }

    private void FixedUpdate()
    {
        // Moves player horizontally
        horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (!isIgnoringInput)
            rb.velocity = new Vector3(moveSpd * horizontalAxis * Time.deltaTime, rb.velocity.y);
        else
            rb.velocity = Vector3.zero;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
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

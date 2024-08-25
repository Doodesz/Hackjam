using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public ShardBehaviour currShard;

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpd;
    [SerializeField] bool isOnGround;

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
            && isOnGround)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Moves player horizontally
        horizontalAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalAxis * moveSpd, rb.velocity.y);        
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            isOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            isOnGround = false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ShardBehaviour>(out ShardBehaviour shard) && !shard.isConnected)
        {
            // If currently drawing lines and hit matching shard, connect both
            if (currShard != null && currShard.shardColor == shard.shardColor
                && currShard.gameObject != collision.gameObject)
            {
                shard.ConnectShard();
                currShard.ConnectShard();
                currShard.GetComponent<DrawLine>().FixLine(shard.gameObject);
            }
        }
    }
}

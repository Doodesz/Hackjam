using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private float horizontalAxis;
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpd;
    public ShardBehaviour currShard;
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ShardBehaviour>(out ShardBehaviour shard) && !shard.isConnected)
        {
            if (currShard != null && currShard.thisShardColor == shard.thisShardColor 
                && currShard.gameObject != collision.gameObject)
            {
                shard.ConnectShard();
                currShard.ConnectShard();
                currShard.GetComponent<DrawLine>().FixLine(shard.gameObject);
            }
            else if (!shard.GetComponent<DrawLine>().isDrawing)
            {
                currShard = shard;
                shard.GetComponent<DrawLine>().isDrawing = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public bool isIgnoringInput = false;
    public Shard currShard;

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
            rb.velocity = new Vector3(horizontalAxis * moveSpd * Time.deltaTime, rb.velocity.y);
        else
            rb.velocity = Vector3.zero;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    public void StopMovement()
    {

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
        if (collision.TryGetComponent<Shard>(out Shard shard) && !shard.isConnected)
        {
            // If currently drawing lines and hit matching shard, connect both
            if (currShard != null && currShard.parent == shard.parent
                && currShard.gameObject != collision.gameObject)
            {
                shard.ConnectShard();
                currShard.ConnectShard();
                currShard.GetComponent<ShardLine>().FixLine(shard.gameObject);
                currShard = null;
            }

        }
    }
}

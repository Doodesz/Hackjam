using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public bool isIgnoringInput = false;
    public bool isOnGround;
    public bool isRunning;
    public Shard currShard;
    public Animator anim;


    [SerializeField] float jumpForce;
    [SerializeField] float moveSpd;

    public float horizontalAxis;
    public Rigidbody2D rb;

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
            anim.SetBool("isFalling", true);
            anim.SetBool("isOnGround", false);
        }
       /* else if (rb.velocity.y !> 0.01f)
            anim.SetTrigger("jump");*/

        if (rb.velocity.y == 0)
        {
            anim.SetBool("isOnGround", true);
            anim.SetBool("isFalling", false);
        }

        if (!isIgnoringInput && !DialogueManager.Instance.isDialogueActive && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager.Instance.PauseGame();
        }
        else if (PauseManager.Instance.gamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager.Instance.UnpauseGame();
        }
    }

    private void FixedUpdate()
    { 
        // Sets appropriate animation
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        if ((horizontalAxis > 0 || horizontalAxis < 0) && !isIgnoringInput)
        {
            anim.SetBool("isRunning", true);
            if (horizontalAxis > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);

            if (!isRunning) 
                isRunning = true;
            if (!PlayerSFX.Instance.isPlayingWalk && isOnGround && isRunning)
                PlayerSFX.Instance.PlayWalk();
        }
        else
        {
            anim.SetBool("isRunning", false);
            if (isRunning)
                isRunning = false;

            // Stops walking sound
            if (PlayerSFX.Instance.isPlayingWalk)
                PlayerSFX.Instance.StopWalk();
        }


        // Moves player horizontally
        if (!isIgnoringInput)
            rb.velocity = new Vector3(moveSpd * horizontalAxis * Time.deltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector3(0, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        //anim.Play("jump");
        anim.SetTrigger("jump");
        //anim.SetBool("isFalling", false);
        anim.SetBool("isOnGround", false);
        PlayerSFX.Instance.PlayJump();
        PlayerSFX.Instance.StopWalk();
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("box") 
            || collision.gameObject.CompareTag("special wall"))
        {
            if (PlayerSFX.Instance.isPlayingWalk)
                PlayerSFX.Instance.StopWalk();
        }
    }
}

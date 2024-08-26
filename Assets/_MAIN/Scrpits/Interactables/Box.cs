using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] bool canBeInteracted = false;
    [SerializeField] bool respawnable;
    [SerializeField] GameObject box;
    [SerializeField] GameObject spawnPoint;

    ParticleSystem particle;
    Animator animator;
    float respawnableRange = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(box.transform.position, spawnPoint.transform.position) > respawnableRange)
            EnableRespawnPrompt();

        if (canBeInteracted && Input.GetKeyDown(KeyCode.F) && respawnable)
            RespawnBox();
    }

    void EnableRespawnPrompt()
    {
        if (!respawnable)
        {
            respawnable = true;
            particle.Play();
            spawnPoint.GetComponent<ParticleSystem>().Stop();
        }
    }

    void RespawnBox()
    {
        StopAllCoroutines();

        spawnPoint.GetComponent<ParticleSystem>().Stop();
        respawnable = false;
        particle.Stop();
        animator.Play("hide prompt");
        canBeInteracted = false;
        spawnPoint.GetComponent<ParticleSystem>().Play();

        box.transform.position = spawnPoint.transform.position;
    }

    IEnumerator StopPopParticle()
    {
        yield return new WaitForSeconds(3f);
        spawnPoint.GetComponent<ParticleSystem>().Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && respawnable)
        {
            canBeInteracted = true;
            animator.Play("show prompt");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && respawnable)
        {
            canBeInteracted = false;
            animator.Play("hide prompt");
        }
    }
}

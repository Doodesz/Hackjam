using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
    [SerializeField] bool canBeInteracted;
    [SerializeField] bool isOpen;
    [SerializeField] bool isGoingToNextLevel;
    [SerializeField] string nextLevelSceneName;
    
    Animator animator;
    ScreenTransition transition;
    ParticleSystem particle;

    public static EndPortal Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        // Fix bug portal don't open when in another world
        if (isOpen)
        {
            animator.SetBool("isOpened", true);
            particle.Play();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        transition = ScreenTransition.Instance;
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (canBeInteracted && Input.GetKeyDown(KeyCode.F) && !PlayerController.Instance.isIgnoringInput)
        {
            if (isOpen)
            {
                Debug.Log("Go to next level");
                GoToNextLevel();
                PlayerController.Instance.isIgnoringInput = true;
                PlayerSFX.Instance.PlayEnterPortal();
            }
        }

        if (isGoingToNextLevel && transition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank"))
            SceneManager.LoadScene(nextLevelSceneName);
    }

    public void GoToNextLevel()
    {
        isGoingToNextLevel = true;
        ScreenTransition.Instance.TransitionIn();
    }

    public void OpenPortal()
    {
        isOpen = true;
        if (isActiveAndEnabled)
        {
            animator.SetBool("isOpened", true);
            particle.Play();

        }
        Debug.Log("End Portal opened");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isOpen)
        {
            canBeInteracted = true;
            animator.Play("show prompt");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBeInteracted = false;
            animator.Play("hide prompt");
        }
    }
}

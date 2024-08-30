using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    Animator animator;

    public static ScreenTransition Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        TransitionOut();
    }

    public void TransitionIn()
    {
        animator.Play("transition in");
    }

    public void TransitionOut()
    {
        animator.Play("transition out");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TransitionOut();
        //if (scene.name == "Main Menu") Destroy(gameObject);
    }
}

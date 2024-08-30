using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] string targetScene;
    [SerializeField] bool isTransitioning;
    Animator mainMenuTransition;
    GameObject transitionObject;

    private void Start()
    {
        mainMenuTransition = GameObject.Find("Main Menu Canvas").GetComponent<Animator>();
        transitionObject = FindTransitionObject();
        mainMenuTransition.Play("transition out");
    }

    private void Update()
    {
        // To trigger the actual world switch
        if (isTransitioning && (mainMenuTransition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank")
            || ScreenTransition.Instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank")))
            ChangeWorld();
    }

    GameObject FindTransitionObject()
    {
        return GameObject.Find("Main Menu Transition");
    }

    public void GoToLevel(string sceneName)
    {
        isTransitioning = true;
        FindTransitionObject();
        mainMenuTransition.Play("transition in");
        ScreenTransition.Instance.TransitionIn();
        targetScene = sceneName;
    }

    private void ChangeWorld()
    {
        SceneManager.LoadScene(targetScene);
    }
}

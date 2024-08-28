using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] string targetScene;
    [SerializeField] bool isChangingWorld;
    ScreenTransition transition;

    private void Start()
    {
        transition = ScreenTransition.Instance;
    }

    private void Update()
    {
        // To trigger the actual world switch
        if (isChangingWorld && transition.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank"))
            ChangeWorld();
    }

    public void GoToLevel(string sceneName)
    {
        isChangingWorld = true;
        transition.TransitionIn();
        targetScene = sceneName;
    }

    private void ChangeWorld()
    {
        SceneManager.LoadScene(targetScene);
    }
}

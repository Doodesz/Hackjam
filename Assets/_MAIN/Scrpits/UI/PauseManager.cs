using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public bool gamePaused;

    [SerializeField] bool isExiting;
    [SerializeField] private bool isRestarting;
    [SerializeField] List<Button> pauseButtons;

    Animator animator;

    public static PauseManager Instance;

    private void Start()
    {
        Instance = this;
        DisableButtons();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isExiting 
            && ScreenTransition.Instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank"))
        {
            SceneManager.LoadScene("Main Menu");
        }
        if (isRestarting
            && ScreenTransition.Instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blank"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EnableButtons()
    {
        foreach (Button button in pauseButtons)
        {
            button.interactable = true;
        }
    }

    void DisableButtons()
    {
        foreach (Button button in pauseButtons)
        {
            button.interactable = false;
        }

    }

    public void PauseGame()
    {
        animator.Play("show");
        PlayerController.Instance.isIgnoringInput = true;
        EnableButtons();
        gamePaused = true;
    }

    public void UnpauseGame()
    {
        animator.Play("hide");
        PlayerController.Instance.isIgnoringInput = false;
        DisableButtons();
        gamePaused = false;
    }

    public void OnExitClick()
    {
        ScreenTransition.Instance.TransitionIn();
        animator.Play("hide");
        isExiting = true;
        DisableButtons();
    }

    public void OnRestartClick()
    {
        ScreenTransition.Instance.TransitionIn();
        animator.Play("hide");
        isRestarting = true;
        DisableButtons();
    }
}

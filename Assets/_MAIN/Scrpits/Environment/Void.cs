using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Void : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isChangingWorld && collision.CompareTag("Player"))
        {
            isChangingWorld = true;
            transition.TransitionIn();
        }
        
    }

    void ChangeWorld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

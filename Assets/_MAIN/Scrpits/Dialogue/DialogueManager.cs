using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    public Animator animator;

    [SerializeField] bool canNext;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

    private void Update()
    {
        if (isDialogueActive && Input.anyKeyDown && canNext)
        {
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();

        PlayerController.Instance.isIgnoringInput = true;
        PlayerController.Instance.rb.velocity = new Vector3(0, PlayerController.Instance.rb.velocity.y);

        GameObject.Find("Menu Button").GetComponent<Button>().interactable = false;
    }

    public void DisplayNextDialogueLine()
    {
        canNext = false;
     
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));

        PlayerSFX.Instance.PlayNextDialogue();
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canNext = true;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("hide");
        PlayerController.Instance.isIgnoringInput = false;
        GameObject.Find("Menu Button").GetComponent<Button>().interactable = true;
    }
}

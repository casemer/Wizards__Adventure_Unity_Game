using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator anim;

    public GameObject dialogueBox;

    public GameObject crosshair;
    public GameObject playerMove;
    public GameObject footDust;
    private Queue<string> sentences;
    public Dialogue[] conversations;
    private int index = 0;

    private bool hasClicked = false;

    public GameObject continueButton;

    public bool isTalking;

    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        DialogueTrigger dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        conversations = dialogueTrigger.conversations;
        sentences = new Queue<string>();

        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {


        if (nameText.text == "Wizard")
        {
            nameText.color = new Color32(142, 0, 165, 255);
        }
        if (nameText.text == "Roommate")
        {
            nameText.color = new Color32(21, 150, 0, 255);
        }
        if (nameText.text == "Adventurer")
        {
            nameText.color = new Color32(0, 179, 167, 255);
        }
        if (nameText.text == "List:")
        {
            nameText.color = new Color32(106, 106, 106, 255);
        }
        if (nameText.text == "Skull")
        {
            nameText.color = new Color32(194, 194, 194, 255);
        }
        if (nameText.text == "Megan")
        {
            nameText.color = new Color32(0, 255, 255, 255);
        }
        if (nameText.text == "Bjorgen")
        {
            nameText.color = new Color32(142, 18, 18, 255);
        }
    }

    public void StartDialogue (Dialogue dialogue)
    {
        isTalking = true;
        pauseMenu.enabled = false;
        Cursor.visible = true;
        nameText.text = dialogue.name;
        crosshair.GetComponent<Crosshair>().enabled = false;
        playerMove.GetComponent<PlayerController>().enabled = false;
        playerMove.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        playerMove.GetComponent<Animator>().enabled = false;
        footDust.SetActive(false);
        dialogueBox.SetActive(true);
        anim.SetBool("IsOpen", true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        else
        {
            string sentence = sentences.Dequeue();
            sentences.Enqueue(sentence);

            if (hasClicked)
            {
                StopAllCoroutines();
                finishSentence(sentence);

            }

            else
            {
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            }
        }

    }

    void finishSentence(string sentence)
    {
        dialogueText.text = sentence;
        hasClicked = false;
        sentence = sentences.Dequeue();
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            FindObjectOfType<AudioManager>().Play("Dialogue");
            yield return 0;
            yield return new WaitForSeconds(.03f);
            hasClicked = true;

        }
        hasClicked = false;
        sentence = sentences.Dequeue();
    }


    void EndDialogue()
    {
        index++;
        if (index < conversations.Length)
        {
            StartDialogue(conversations[index]);
            return;
        }
        else
        {
            isTalking = false;
            crosshair.GetComponent<Crosshair>().enabled = true;
            Cursor.visible = false;
            anim.SetBool("IsOpen", false);
            playerMove.GetComponent<PlayerController>().enabled = true;
            playerMove.GetComponent<Animator>().enabled = true;
            StartCoroutine(CloseTime());
            pauseMenu.enabled = true;
        }

    }

    IEnumerator CloseTime()
    {
        yield return new WaitForSeconds(.32f);
        footDust.SetActive(true);
        dialogueBox.SetActive(false);
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

}

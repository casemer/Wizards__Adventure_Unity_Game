using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class DialogueManagerNoOptionsWitchBoss : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator anim;

    public GameObject dialogueBox;

    public GameObject crosshair;
    public GameObject playerMove;
    private Queue<string> sentences;
    public Dialogue[] conversations;
    private int index = 0;
    private bool hasClicked = false;
    public PlayableDirector pd;

    public GameObject continueButton;

    public GameObject followArea;
    public GameObject mage, mage2;

    public bool isTalking;
    public bool isBeginning;
    //for skeleton to not attack
    public Collider2D talkspot;

    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        DialogueTriggerNoOptionsWitchBoss dialogueTrigger = FindObjectOfType<DialogueTriggerNoOptionsWitchBoss>();
        conversations = dialogueTrigger.conversations;
        sentences = new Queue<string>();

        followArea.SetActive(false);

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
        if (nameText.text == "List:")
        {
            nameText.color = new Color32(106, 106, 106, 255);
        }
        if (nameText.text == "Skeleton King")
        {
            nameText.color = new Color32(207, 179, 0, 255);
        }
        if (nameText.text == "Guard")
        {
            nameText.color = new Color32(119, 119, 119, 255);
        }
        if (nameText.text == "Witch")
        {
            nameText.color = new Color32(173, 101, 214, 255);
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
        playerMove.GetComponent<Animator>().enabled = false;
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

    IEnumerator TypeSentence(string sentence)
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
            Cursor.visible = false;
            anim.SetBool("IsOpen", false);
            StartCoroutine(CloseTime());
            playerMove.GetComponent<Animator>().enabled = true;
            playerMove.transform.Rotate(0f, 180f, 0f);

            if (isBeginning)
            {
                StartCoroutine(WaitToMove());
            }
            else
            {
                playerMove.GetComponent<PlayerController>().enabled = true;
                crosshair.GetComponent<Crosshair>().enabled = true;
                pauseMenu.enabled = true;
            }

        }

    }

    IEnumerator CloseTime()
    {
        yield return new WaitForSeconds(.32f);
        dialogueBox.SetActive(false);
        talkspot.enabled = true;
        followArea.SetActive(true);
        mage.SetActive(true);
        mage2.SetActive(true);
        isTalking = false;
        pd.Play();
    }


    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(4f);
        playerMove.GetComponent<PlayerController>().enabled = true;
        crosshair.GetComponent<Crosshair>().enabled = true;
    }

        public void setIndex(int index)
    {
        this.index = index;
    }

}

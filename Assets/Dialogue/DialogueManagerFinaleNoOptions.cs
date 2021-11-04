using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class DialogueManagerFinaleNoOptions : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator anim;
    public Animator cameraAnim;

    public GameObject dialogueBox;

    private Queue<string> sentences;
    public Dialogue[] conversations;
    private int index = 0;
    private bool hasClicked = false;
    public PlayableDirector pd;

    public GameObject continueButton;

    public bool isTalking;
    public bool isBeginning;
    //for skeleton to not attack
    public Collider2D talkspot;

    // Start is called before the first frame update
    void Start()
    {
        DialogueTriggerFinaleNoOptions dialogueTrigger = FindObjectOfType<DialogueTriggerFinaleNoOptions>();
        conversations = dialogueTrigger.conversations;
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (nameText.text == "Wizard")
        {
            if(cameraAnim.GetBool("alltoroommate") == true)
            {
                cameraAnim.SetBool("roommatetowizard", true);
            }

            nameText.color = new Color32(142, 0, 165, 255);
            cameraAnim.SetBool("adventurertowizard", true);
        }
        if (nameText.text == "Roommate")
        {
            nameText.color = new Color32(21, 150, 0, 255);
            cameraAnim.SetBool("alltoroommate", true);
        }
        if (nameText.text == "Bjorgen")
        {
            nameText.color = new Color32(142, 18, 18, 255);
            cameraAnim.SetBool("alltobjorgen", true);
        }
        if (nameText.text == "All")
        {
            nameText.color = new Color32(32, 219, 140, 255);
            cameraAnim.SetBool("wizardtoall", true);
        }
        if (nameText.text == "Adventurer")
        {
            nameText.color = new Color32(0, 179, 167, 255);
            cameraAnim.SetBool("bjorgentoadventurer", true);
        }


    }

    public void StartDialogue (Dialogue dialogue)
    {
        isTalking = true;
        Cursor.visible = true;
        nameText.text = dialogue.name;
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
            anim.SetBool("IsOpen", false);
            StartCoroutine(CloseTime());
            isTalking = false;
            cameraAnim.SetBool("theEnd", true);

        }

    }


    IEnumerator CloseTime()
    {
        yield return new WaitForSeconds(.32f);
        dialogueBox.SetActive(false);
        talkspot.enabled = true;
        pd.Play();
    }



        public void setIndex(int index)
    {
        this.index = index;
    }

}

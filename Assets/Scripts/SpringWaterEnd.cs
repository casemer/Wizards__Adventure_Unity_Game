using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpringWaterEnd : MonoBehaviour {
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator anim;

    public GameObject dialogueBox;

    public GameObject crosshair;
    public GameObject playerMove;
    private Queue<string> sentences;
    public Dialogue[] conversations;
    private int index = 0;

    public GameObject continueButton;

    public GameObject sceneTransition;

    public bool isTalking;

    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        SpringWaterTrigger dialogueTrigger = FindObjectOfType<SpringWaterTrigger>();
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
        if (nameText.text == "DarkRoot")
        {
            nameText.color = new Color32(54, 0, 65, 255);
        }
        if (nameText.text == "Spring Water")
        {
            nameText.color = new Color32(41, 190, 167, 255);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        PlayerPrefs.SetInt("WitchBoss", 1);
        Cursor.visible = true;
        pauseMenu.enabled = false;
        nameText.text = dialogue.name;
        crosshair.GetComponent<Crosshair>().enabled = false;
        playerMove.GetComponent<PlayerController>().enabled = false;
        playerMove.GetComponent<Animator>().enabled = false;
        dialogueBox.SetActive(true);
        anim.SetBool("IsOpen", true);
        isTalking = true;
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

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        continueButton.SetActive(false);
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            FindObjectOfType<AudioManager>().Play("Dialogue");
            yield return 0;
            yield return new WaitForSeconds(.03f);
        }
        continueButton.SetActive(true);
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
            crosshair.GetComponent<Crosshair>().enabled = true;
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
        dialogueBox.SetActive(false);
        isTalking = false;
        sceneTransition.SetActive(true);
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

}

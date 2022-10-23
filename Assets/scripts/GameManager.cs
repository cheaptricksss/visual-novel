using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //these lists have all the dialogue for each phase of questions
    public List<string> phaseOneDialogue;
    public List<string> phaseTwoDialogue;
    public List<string> phaseThreeDialogue;
    public List<string> phaseFourDialogue;
    public List<string> phaseFiveDialogue;
    public List<string> phaseSixDialogue;
    // end
    public List<string> phaseSevenDialogue;
    // secret end
    public List<string> phaseEightDialogue;

    //holds the phase we're currently going through
    List<string> currentDialogue;

    //tracks the current phase and the line we're on in that phase
    int phaseIndex = 0;
    int dialogueIndex = 0;

    //game object for all buttons
    public GameObject choiceOne;
    public GameObject choiceTwo;
    public GameObject nextButton;
    public GameObject hiddenChoice;

    //text component that is showing the dialogue
    public TMP_Text dialogueBox;

    // public string clownMessage;
    // public string notAClownMessage;

    public GameObject may;
    public RawImage mayImageComponent;
    // different images of the character (5 in total)
    public Texture may_normal;
    public Texture may_happy;
    public Texture may_mischivious;
    public Texture may_inLove;


    //"score" for how much of a clown u r
    int clownyLove = 0;
    //"score" for how bored you are
    int imBored = 0;
    // current choice is 1 for Yes 0 for no
    int currentChoise = 0;

    private bool actuallyTrue = false;

    // audio
    AudioSource mySource;
    AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        //turn off the choice buttons
        choiceOne.SetActive(false);
        choiceTwo.SetActive(false);
        hiddenChoice.SetActive(false);
        //start the dialogue
        currentDialogue = phaseOneDialogue;
        dialogueBox.text = currentDialogue[dialogueIndex];

        mySource = GetComponent<AudioSource>();
        buttonSound = GameObject.FindGameObjectWithTag("Panel").GetComponent<AudioSource>();
    }

    void SetDialogueText()
    {
        //if we haven't gotten our results yet
        if (phaseIndex <= 6)
        {
            //set the dialogue component to show the line we're on
            dialogueBox.text = currentDialogue[dialogueIndex];
        }
    }

    public void AdvanceDialog()
    {
        //if we haven't gotten our results yet
        if (phaseIndex < 5)
        {
            //go to the next line
            dialogueIndex++;
            SetDialogueText();
            //if we're on the last line of dialogue
            // one more -1 because there is an either or in dialogue choices
            if (dialogueIndex == currentDialogue.Count - 1)
            {
                //show the choices
                SetupChoices();
            }
            
        }
        else if(phaseIndex >= 5)
        {
            dialogueIndex++;
            SetDialogueText();
            
            if (dialogueIndex == currentDialogue.Count - 1)
            {
                if (phaseIndex == 6)
                {
                    SceneManager.LoadScene("SecretEnd");
                }
                else if (phaseIndex == 5)
                {
                    SceneManager.LoadScene("NormalEnd");
                }
            }else if (dialogueIndex == currentDialogue.Count - 2)
            {
                if (phaseIndex == 6)
                {
                    may.SetActive(false);
                }
            }
        }
    }

    void SetupChoices()
    {
        //turn off the next button and turn on the choice buttons
        nextButton.SetActive(false);
        choiceOne.SetActive(true);
        choiceTwo.SetActive(true);
        if(imBored > 2)
        {
            hiddenChoice.SetActive(true);
        }
    }

    //if we press "no",
    public void FaceyChoice()
    {
        buttonSound.Play();
        currentChoise = 0;
        imBored++;
        GoToNextPhase();
        
    }

    //if we press "yes", 
    public void ClownyChoice()
    {
        buttonSound.Play();
        currentChoise = 1;
        // clownyLove++;
        GoToNextPhase();
    }

    // hidden choice
    public void Actually()
    {
        buttonSound.Play();
        actuallyTrue = true;
        phaseIndex = 5;
        GoToNextPhase();
    }

    void GoToNextPhase()
    {
        //turn on the next button and turn off the choice buttons
        nextButton.SetActive(true);
        choiceOne.SetActive(false);
        choiceTwo.SetActive(false);
        hiddenChoice.SetActive(false);
        //reset the dialogue line counter
        dialogueIndex = 0;
        //depending on the phase
        //run an animation and determine what the next phase is
        switch (phaseIndex)
        {
            case 0:
                currentDialogue = phaseTwoDialogue;
                phaseIndex = 1;
                break;
            case 1:
                currentDialogue = phaseThreeDialogue;
                phaseIndex = 2;
                ChangeImage(may_normal);
                break;
            case 2:
                // here, the answer given changes parts of the response
                if (currentChoise == 1)
                {
                    currentDialogue = phaseFourDialogue;
                    ChangeImage(may_happy);
                }
                else if (currentChoise == 0)
                {
                    currentDialogue = phaseFiveDialogue;
                    ChangeImage(may_normal);
                }
                phaseIndex = 3;
                break;
            case 3:
                currentDialogue = phaseSixDialogue;
                phaseIndex = 4;
                ChangeImage(may_inLove);
                break;
            case 4:
                currentDialogue = phaseSevenDialogue;
                phaseIndex = 5;
                mySource.Play();
                ChangeImage(may_normal);
                // GiveResults();
                break;
            case 5:
                currentDialogue = phaseEightDialogue;
                ChangeImage(may_normal);
                phaseIndex = 6;
                // GiveResults();
                break;
        }
        SetDialogueText();
    }

    //void GiveResults()
    //{
    //    if(actuallyTrue == true)
    //    {
    //        // dialogueBox.text =
    //    }
    //    else if(clownyLove > 2)
    //    {
    //        dialogueBox.text = clownMessage;
    //    }
    //    else
    //    {
    //        dialogueBox.text = notAClownMessage;
    //    }
    //}

    void ChangeImage(Texture newImage)
    {
        mayImageComponent.texture = newImage;
    }
  
}

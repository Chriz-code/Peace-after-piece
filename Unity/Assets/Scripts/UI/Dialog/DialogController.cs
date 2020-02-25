﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Image profileBox = null;
    [SerializeField] Image profile = null, dialogBox = null;
    [SerializeField]
    Sprite profileBoxAngela = null, dialogBoxAngela = null, profileBoxElenor = null, dialogBoxElenor = null;
    [SerializeField] Text dialogText = null;
    [SerializeField] ChoiceMenu choiceMenu = null;
    [SerializeField] AudioSource textSoundSource = null;

    IEnumerator dialogStarter = null;
    IEnumerator dialogWritter = null;

    Vector3 originalPos = Vector3.zero;
    [Header("Variables")]
    [SerializeField] int speedMultiplier = 3;
    [SerializeField] bool silenceWhenMultiplying = false;
    [SerializeField] bool ready = false;
    [SerializeField] bool scrollText = false;
    [SerializeField] KeyCode dialogKey = KeyCode.E;
    [SerializeField] KeyCode dialogKeyAlternative = KeyCode.Mouse0;
    bool scrollTextDelta = false;
    float scrollSpeed = 30;

    int deltaSpeedMultiplier = 1;

    private void OnValidate()
    {
        if (!(profileBoxAngela && dialogBoxAngela && profileBoxElenor && dialogBoxElenor))
        {
            Debug.LogWarning("Dialog Controller is missing sprite refrences");

        }
        if (dialogBox && profileBox && profile && dialogBox && textSoundSource && choiceMenu)
        {
            ready = true;
        }
        else
        {
            ready = false;
            Debug.LogWarning("Dialog Controller is missing refrences");
        }
    }
    private void Start()
    {
        OnValidate();
        originalPos = dialogText.rectTransform.localPosition;
    }

    public void StartDialog(Dialog[] dialogProfiles)
    {
        if (ready && ienumerating == false)
        {
            dialogStarter = StartDialogIEnumerator(dialogProfiles);
            StartCoroutine(dialogStarter);
        }
    }
    bool ienumerating = false;
    int rowCount = 1;

    Player_UserInput user = null;
    public IEnumerator StartDialogIEnumerator(Dialog[] dialogProfiles)
    {
        if (ienumerating == true)
            yield break;
        ienumerating = true;

        if (GameController.Get.GetActivePlayer != null)
        {
            user = GameController.Get.GetActivePlayer;
            user.enabled = false;
        }
        for (int i = 0; i < dialogProfiles.Length; i++)
        {
            choiceNum = 0;

            dialogWritter = DialogIEnumerator(dialogProfiles[i]); // prepare IEnumerator

            if (dialogProfiles[i]._DialogEvent) // Dialog Events
                dialogProfiles[i].unityEvent?.events?.Invoke();

            yield return StartCoroutine(dialogWritter);// Print Text to dialogBox and Produce Sound

            if (dialogProfiles[i].multipleChoice) // Multiple Choice Area
            {
                if (choiceMenu.yes.transform.GetChild(0))
                    choiceMenu.yes.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = dialogProfiles[i].buttonTextYes;
                if (choiceMenu.no.transform.GetChild(0))
                    choiceMenu.no.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = dialogProfiles[i].buttonTextNo;

                choiceMenu.gameObject.SetActive(true);
                yield return new WaitUntil(() => choiceNum != 0); // Wait for Button Input
                ienumerating = false;
                if (choiceNum == 1)
                {
                    dialogProfiles[i].yes.CallDialog(transform);
                }
                if (choiceNum == 2)
                    dialogProfiles[i].no.CallDialog(transform);
                choiceMenu.gameObject.SetActive(false);
                yield break;
            }
            /*else if (dialogProfiles[i].playerInput)
            {
                do //Wait for playerInput
                {
                    yield return null;
                } while (KeyDown() == false);
            }*/

        }
        StopDialog();
    }
    [Header("Debug")]
    [SerializeField] int choiceNum = 0;
    public void SetChoice(int i) // Yes No Buttons
    {
        choiceNum = i;
        Debug.Log(choiceNum+" c:i "+ i);
    }

    public IEnumerator DialogIEnumerator(Dialog dialog)
    {
        CheckPerspective();
        //Profile
        //profileBox.gameObject.SetActive(true);
        //this.profile.sprite = dialogProfile.profile;
        //this.profile.color = dialogProfile.profileColor;

        //Box
        dialogBox.gameObject.SetActive(true);
        string[] messages = dialog.message.Split('@'); // Split message into parts
        if (scrollText)
            messages = dialog.message.Split('@', '-');

        dialogText.text = "";
        rowCount = 1;
        for (int i = 0; i < messages.Length; i++) //Loop through parts
        {
            int lowerWaitBy = 1;
            if (!scrollText) //Clear message
            {
                dialogText.text = "";
            }
            for (int j = 0; j < messages[i].Length; j++) //Loop through characters
            {
                if (messages[i][j] == '/') //Lower WaitTime if symbol found in message
                {
                    lowerWaitBy += 1;
                }
                else if (!(messages[i][j] == '-')) // Skip WaitTime if symbol found in message
                {
                    dialogText.text += messages[i][j];
                    if (textSoundSource != null && dialog.profile.textSounds.Length > 0 && //If there's a audioSource and soundClips
                        !(messages[i][j] == ' ' || messages[i][j] == '\n') && //If it's not empty space or new line
                        (!silenceWhenMultiplying || (silenceWhenMultiplying && deltaSpeedMultiplier == 1))) //If you should play when skipping text
                    {
                        AudioClip sound = dialog.profile.textSounds[Random.Range(0, dialog.profile.textSounds.Length)];
                        if (sound)
                            this.textSoundSource.PlayOneShot(sound, dialog.textVolume);
                    }
                    else if (messages[i][j] == '\n')//Increase row count
                    {
                        rowCount++;
                    }
                    yield return new WaitForSeconds(dialog.textSpeed == 0 ? 0.001f : dialog.textSpeed / deltaSpeedMultiplier);//Wait between characters
                }
            }
            if (scrollText && (i + 1) < messages.Length) // Scroll Text Effect
            {
                nextPos = dialogText.rectTransform.localPosition + (Vector3.up * (24 + 5f));
                float distance = (nextPos - dialogText.rectTransform.localPosition).y;
                scrollTextDelta = true;
                yield return new WaitForSeconds(distance / scrollSpeed);
                scrollTextDelta = false;
            }
            else if (dialog.playerInput == true && (!dialog.multipleChoice || (dialog.multipleChoice && i != messages.Length - 1))) // Player Input to go to next message and it's not the last message:  
            {
                do //Wait for playerInput
                {
                    yield return null;
                } while (KeyDown() == false);
            }
            else
                yield return new WaitForSeconds(dialog.textWaitTime / lowerWaitBy);//Wait between parts
        }
    }

    Vector3 nextPos = Vector2.zero;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            scrollText = !scrollText;

        if (scrollTextDelta && rowCount > 2 && dialogText.rectTransform.localPosition.y < nextPos.y)
            dialogText.rectTransform.localPosition += Vector3.up * Time.deltaTime * scrollSpeed;

        if (KeyHold())
            deltaSpeedMultiplier = speedMultiplier;
        else if (KeyUp())
            deltaSpeedMultiplier = 1;

    }
    public void CheckPerspective()
    {
        if (GameController.Get.CurrentPerspective == Perspective.Angela && ready)
        {
            profileBox.sprite = profileBoxAngela;
            dialogBox.sprite = dialogBoxAngela;
        }
        else if (GameController.Get.CurrentPerspective == Perspective.Elenor && ready)
        {
            profileBox.sprite = profileBoxElenor;
            dialogBox.sprite = dialogBoxElenor;
        }
    }
    public void StopDialog()
    {
        if (ready)
        {
            dialogText.text = "";
            dialogBox.gameObject.SetActive(false);
            choiceMenu.gameObject.SetActive(false);
            //profileBox.gameObject.SetActive(false);
            dialogText.rectTransform.localPosition = originalPos;
            ienumerating = false;
            if (user != null)
                user.enabled = true;
            if (dialogStarter != null)
                StopCoroutine(dialogStarter);
            if (dialogWritter != null)
                StopCoroutine(dialogWritter);
        }
    }

    public bool KeyHold()
    {
        if (Input.GetKey(dialogKey))
            return true;
        if (Input.GetKey(dialogKeyAlternative))
            return true;
        return false;
    }
    public bool KeyDown()
    {
        if (Input.GetKeyDown(dialogKey))
            return true;
        if (Input.GetKeyDown(dialogKeyAlternative))
            return true;
        return false;
    }
    public bool KeyUp()
    {
        if (Input.GetKeyUp(dialogKey))
            return true;
        if (Input.GetKeyUp(dialogKeyAlternative))
            return true;
        return false;
    }
}
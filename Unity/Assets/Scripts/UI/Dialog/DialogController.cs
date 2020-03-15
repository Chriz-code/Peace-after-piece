using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Image dialogProfile = null;
    [SerializeField] Image profile = null, dialogBox = null;
    [SerializeField]
    Sprite profileBoxAngela = null, dialogProfileAngela = null, dialogBoxAngela = null, profileBoxElenor = null, dialogProfileElenor = null, dialogBoxElenor = null;
    [SerializeField] Text dialogText = null;
    [SerializeField] ChoiceMenu choiceMenu = null;
    [SerializeField] AudioSource textSoundSource = null;

    IEnumerator dialogStarter = null;
    IEnumerator dialogWritter = null;

    [Header("Variables")]
    [SerializeField] bool showProfile = false;
    [SerializeField] int speedMultiplier = 3;
    [SerializeField] bool silenceWhenMultiplying = false;
    [SerializeField] bool ready = false;
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
        if (dialogBox && profile && dialogBox && textSoundSource && choiceMenu)
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

    [SerializeField] Player_UserInput user = null;
    public IEnumerator StartDialogIEnumerator(Dialog[] dialogProfiles)
    {
        if (ienumerating == true)
            yield break;
        ienumerating = true;

        /*if (GameController.Get.GetActivePlayer != null)
        {
            user = GameController.Get.GetActivePlayer;
            user.enabled = false;
        }*/

        for (int i = 0; i < dialogProfiles.Length; i++)
        {
            choiceNum = 0;

            dialogWritter = DialogIEnumerator(dialogProfiles[i]); // prepare IEnumerator

            if (dialogProfiles[i].dialogEventCheck) // Dialog Events
                dialogProfiles[i].dialogEvent?.events?.Invoke(transform, null);

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
                    if (dialogProfiles[i].yes)
                        dialogProfiles[i].yes.CallDialog(transform, null);
                    else
                        StopDialog();
                }
                if (choiceNum == 2)
                {
                    if (dialogProfiles[i].no)
                        dialogProfiles[i].no.CallDialog(transform, null);
                    else
                        StopDialog();
                }

                choiceMenu.gameObject.SetActive(false);
                yield break;
            }
        }
        StopDialog();
    }
    [Header("Debug")]
    [SerializeField] int choiceNum = 0;
    public void SetChoice(int i) // Yes No Buttons
    {
        choiceNum = i;
        //Debug.Log(choiceNum + " c:i " + i);
    }

    public IEnumerator DialogIEnumerator(Dialog dialog)
    {
        CheckPerspective();

        if (dialog.profile == null)
        {
            Debug.LogWarning("DialogProfile Missing!");
            dialog.profile = new DialogProfile(null, Color.white, new AudioClip[0]);
        }
        //dialog.profile = new DialogProfile(null, Color.white, new AudioClip[0]);

        if (showProfile)
        {
            //Profile
            dialogProfile.gameObject.SetActive(true);
            this.profile.sprite = dialog.profile.profileImage;
            this.profile.color = dialog.profile.color;
        }

        //Box
        dialogBox.gameObject.SetActive(true);
        string[] messages = dialog.message.Split('@'); // Split message into parts

        dialogText.text = "";
        rowCount = 1;
        for (int i = 0; i < messages.Length; i++) //Loop through parts
        {
            int lowerWaitBy = 1;
            for (int j = 0; j < messages[i].Length; j++) //Loop through characters
            {
                if (messages[i][j] == '/') //Lower WaitTime if symbol found in message
                {
                    lowerWaitBy += 1;
                }
                if ((messages[i][j] == '-')) // Skip WaitTime if symbol found in message
                {
                    dialogText.text += messages[i][j];
                }
                else
                {
                    dialogText.text += messages[i][j];
                    if (textSoundSource != null && dialog.profile.textSounds.Length > 0 && //If there's a audioSource and soundClips
                        !(messages[i][j] == ' ' || messages[i][j] == '\n') && //If it's not empty space or new line
                        (!silenceWhenMultiplying || (silenceWhenMultiplying && deltaSpeedMultiplier == 1))) //If you should play when skipping text
                    {
                        AudioClip sound = dialog.profile.textSounds[Random.Range(0, dialog.profile.textSounds.Length)];
                        if (sound)
                            this.textSoundSource.PlayOneShot(sound, dialog.overrideVolume ? dialog.textVolume : dialog.profile.volume);
                    }
                    else if (messages[i][j] == '\n')//Increase row count
                    {
                        rowCount++;
                    }
                    yield return new WaitForSeconds(dialog.textSpeed == 0 ? 0.001f : dialog.textSpeed / deltaSpeedMultiplier);//Wait between characters
                }
            }
            if (dialog.playerInput == true && (!dialog.multipleChoice || (dialog.multipleChoice && i != messages.Length - 1))) // Player Input to go to next message and it's not the last message:  
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
            dialogBox.sprite = dialogBoxAngela;
            dialogProfile.sprite = dialogProfileAngela;
        }
        else if (GameController.Get.CurrentPerspective == Perspective.Elenor && ready)
        {
            dialogBox.sprite = dialogBoxElenor;
            dialogProfile.sprite = dialogProfileElenor;
        }
    }
    public void StopDialog()
    {
        if (ready)
        {
            dialogText.text = "";
            dialogBox.gameObject.SetActive(false);
            choiceMenu.gameObject.SetActive(false);
            dialogProfile.gameObject.SetActive(false); //Profile
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
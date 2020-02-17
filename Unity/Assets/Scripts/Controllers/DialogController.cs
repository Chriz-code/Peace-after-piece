using System.Collections;
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
    [SerializeField] AudioSource textSound = null;

    IEnumerator dialog = null;

    Vector3 originalPos = Vector3.zero;
    [Header("Variables")]
    [SerializeField] int speedMultiplier = 3;
    [SerializeField] bool silenceWhenMultiplying = false;
    [SerializeField] bool ready = false;
    [SerializeField] bool scrollText = false;
    [SerializeField] KeyCode dialogKey = KeyCode.E;
    bool scrollTextDelta = false;
    float scrollSpeed = 30;

    int deltaSpeedMultiplier = 1;

    private void OnValidate()
    {
        if (!(profileBoxAngela && dialogBoxAngela && profileBoxElenor && dialogBoxElenor))
        {
            Debug.LogWarning("Dialog Controller is missing sprite refrences");

        }
        if (dialogBox && profileBox && profile && dialogBox && textSound)
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

    public void StartDialog(DialogProfile[] dialogProfiles)
    {
        if (ready)
        {
            dialog = StartDialogIEnumerator(dialogProfiles);
            StartCoroutine(dialog);
        }
    }
    bool ienumerating = false;
    int rowCount = 1;

    Player_UserInput user = null;
    public IEnumerator StartDialogIEnumerator(DialogProfile[] dialogProfiles)
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
            yield return StartCoroutine(DialogIEnumerator(
                dialogProfiles[i].message,
                dialogProfiles[i].profile,
                dialogProfiles[i].color,
                dialogProfiles[i].textSound,
                dialogProfiles[i].textVolume,
                dialogProfiles[i].textWaitTime,
                dialogProfiles[i].textSpeed,
                dialogProfiles[i].playerInput
                ));
        }
        StopDialog();
    }

    public IEnumerator DialogIEnumerator(string message, Sprite profile, Color profileColor, AudioClip textSound, float textVolume, float textWaitTime, float textSpeed, bool playerInput)
    {
        CheckPerspective();

        //Profile
        profileBox.gameObject.SetActive(true);
        this.profile.sprite = profile;
        this.profile.color = profileColor;

        //Box
        dialogBox.gameObject.SetActive(true);
        string[] messages = message.Split('@');
        if (scrollText)
            messages = message.Split('@', '-');

        dialogText.text = "";
        rowCount = 1;
        for (int i = 0; i < messages.Length; i++)
        {
            int lowerWaitBy = 1;
            if (!scrollText)
            {
                dialogText.text = "";
            }
            for (int j = 0; j < messages[i].Length; j++)
            {
                if (messages[i][j] == '/')
                {
                    lowerWaitBy += 1;
                }
                else if (!(messages[i][j] == '-'))
                {
                    dialogText.text += messages[i][j];
                    if (textSound != null && !(messages[i][j] == ' ' || messages[i][j] == '\n') && (!silenceWhenMultiplying || (silenceWhenMultiplying && deltaSpeedMultiplier == 1)))
                        this.textSound.PlayOneShot(textSound, textVolume);
                    else if (messages[i][j] == '\n')
                    {
                        rowCount++;
                    }
                    yield return new WaitForSeconds(textSpeed == 0 ? 0.001f : textSpeed / deltaSpeedMultiplier);
                }
            }
            if (scrollText && (i + 1) < messages.Length)
            {
                nextPos = dialogText.rectTransform.localPosition + (Vector3.up * (24 + 5f));
                float distance = (nextPos - dialogText.rectTransform.localPosition).y;
                scrollTextDelta = true;
                yield return new WaitForSeconds(distance / scrollSpeed);
                scrollTextDelta = false;
            }
            else if (playerInput == true)
            {
                do
                {
                    yield return null;
                } while (!Input.GetKeyDown(dialogKey));
            }
            else
                yield return new WaitForSeconds(textWaitTime / lowerWaitBy);
        }
    }

    Vector3 nextPos = Vector2.zero;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            scrollText = !scrollText;

        if (scrollTextDelta && rowCount > 2 && dialogText.rectTransform.localPosition.y < nextPos.y)
            dialogText.rectTransform.localPosition += Vector3.up * Time.deltaTime * scrollSpeed;

        if (Input.GetKeyDown(dialogKey))
        {
            deltaSpeedMultiplier = speedMultiplier;
        }
        else if (Input.GetKeyUp(dialogKey))
        {
            deltaSpeedMultiplier = 1;
        }
    }
    void CheckPerspective()
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
            dialogBox.gameObject.SetActive(false);
            profileBox.gameObject.SetActive(false);
            dialogText.rectTransform.localPosition = originalPos;
            ienumerating = false;
            if (user != null)
                user.enabled = true;
            if (dialog != null)
                StopCoroutine(dialog);
        }
    }
}
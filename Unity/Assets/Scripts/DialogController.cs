using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogController : MonoBehaviour
{
    [SerializeField]
    Image profileBox = null, profile = null, dialogBox = null;
    [SerializeField]
    Sprite profileBoxAngela = null, dialogBoxAngela = null, profileBoxElenor = null, dialogBoxElenor = null;
    [SerializeField]
    Text dialogText = null;
    [SerializeField]
    AudioSource textSound = null;

    IEnumerator dialog = null;

    Vector3 originalPos = Vector3.zero;
    [SerializeField] bool ready = false;
    [SerializeField] bool scrollText = false;
    bool scrollTextDelta = false;
    float scrollSpeed = 30;

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
            Debug.LogWarning("Dialog Controller is missing refrences");
    }

    private void Start()
    {
        OnValidate();
        originalPos = dialogText.rectTransform.localPosition;
    }

    public void StartDialog(string message = "", Sprite profile = null, AudioClip textSound = null, float textWaitTime = 2f, float textSpeed = 0.5f)
    {
        if (ready)
        {
            dialog = StartDialogIEnumerator(message, profile, Color.white, textSound, textWaitTime, textSpeed);
            StartCoroutine(dialog);
        }
    }
    public void StartDialog(string message, Sprite profile, Color color, AudioClip textSound = null, float textWaitTime = 2f, float textSpeed = 0.5f)
    {
        if (ready)
        {
            dialog = StartDialogIEnumerator(message, profile, color, textSound, textWaitTime, textSpeed);
            StartCoroutine(dialog);
        }
    }
    bool ienumerating = false;
    int rowCount = 1;
    public IEnumerator StartDialogIEnumerator(string message, Sprite profile, Color profileColor, AudioClip textSound, float textWaitTime, float textSpeed)
    {
        if (ienumerating == true)
            yield break;
        ienumerating = true;

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
                    messages[i].Remove(j, 1);
                    messages[i].Insert(j, "");
                }
                else if (!(messages[i][j] == '-'))
                {
                    dialogText.text += messages[i][j];
                    if (textSound != null && !(messages[i][j] == ' ' || messages[i][j] == '\n'))
                        this.textSound.PlayOneShot(textSound);
                    else if (messages[i][j] == '\n')
                    {
                        rowCount++;
                    }
                    yield return new WaitForSeconds(textSpeed);
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
            else
                yield return new WaitForSeconds(textWaitTime / lowerWaitBy);
        }

        StopDialog();
    }
    Vector3 nextPos = Vector2.zero;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            scrollText = !scrollText;

        if (scrollTextDelta && rowCount > 2 && dialogText.rectTransform.localPosition.y < nextPos.y)
            dialogText.rectTransform.localPosition += Vector3.up * Time.deltaTime * scrollSpeed;

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
        dialogBox.gameObject.SetActive(false);
        profileBox.gameObject.SetActive(false);
        dialogText.rectTransform.localPosition = originalPos;
        ienumerating = false;

        if (dialog != null)
            StopCoroutine(dialog);
    }
}
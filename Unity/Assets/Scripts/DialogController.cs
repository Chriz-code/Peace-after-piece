using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogController : MonoBehaviour
{
    [SerializeField]
    Image profile = null, dialogBox = null;
    [SerializeField]
    Text dialogText = null;
    [SerializeField]
    AudioSource textSound = null;

    [SerializeField] bool ready = false;

    [SerializeField] bool scrollText = true;
    bool scrollTextDelta = false;
    float scrollSpeed = 30;

    private void OnValidate()
    {
        if (dialogBox && profile && dialogBox && textSound)
        {
            ready = true;
        }
        else
            Debug.LogWarning("Dialog Controlling is missing refrences");

    }

    private void Start()
    {
        OnValidate();
    }

    public void StartDialog(string message = "", Sprite profile = null, AudioClip textSound = null, float textWaitTime = 2f, float textSpeed = 0.5f)
    {
        if (ready)
            StartCoroutine(StartDialogIEnumerator(message, profile, Color.white, textSound, textWaitTime, textSpeed));
    }

    public void StartDialog(string message, Sprite profile, Color color, AudioClip textSound = null, float textWaitTime = 2f, float textSpeed = 0.5f)
    {
        if (ready)
            StartCoroutine(StartDialogIEnumerator(message, profile, color, textSound, textWaitTime, textSpeed));
    }


    bool ienumerating = false;
    int rowCount = 1;
    public IEnumerator StartDialogIEnumerator(string message, Sprite profile, Color profileColor, AudioClip textSound, float textWaitTime, float textSpeed)
    {
        if (ienumerating == true)
            yield break;
        ienumerating = true;

        Vector3 oldPos = Vector3.zero;

        //Profile
        this.profile.gameObject.SetActive(true);
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
                else if (messages[i][j] == '-')
                {
                }
                else
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
                oldPos += (Vector3.up * (24 + 5f));
                nextPos = dialogText.rectTransform.localPosition + (Vector3.up * (24 + 5f));
                float distance = (nextPos - dialogText.rectTransform.localPosition).y;
                scrollTextDelta = true;
                yield return new WaitForSeconds(distance / scrollSpeed);
                scrollTextDelta = false;
            }
            else
                yield return new WaitForSeconds(textWaitTime / lowerWaitBy);
        }

        //Box
        dialogBox.gameObject.SetActive(false);

        //Profile
        this.profile.gameObject.SetActive(false);
        this.profile.sprite = profile;
        this.profile.color = profileColor;

        dialogText.rectTransform.localPosition -= oldPos;

        ienumerating = false;
    }

    Vector3 nextPos = Vector2.zero;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            scrollText = !scrollText;

        if (scrollTextDelta && rowCount > 2 && dialogText.rectTransform.localPosition.y < nextPos.y)
            dialogText.rectTransform.localPosition += Vector3.up * Time.deltaTime * scrollSpeed;

    }
}
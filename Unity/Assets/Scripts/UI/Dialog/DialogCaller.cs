using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCaller : MonoBehaviour
{
    public Dialog[] dialogChain = new Dialog[1];

    public void CallDialog(Transform transform)
    {
        Debug.Log("woop");
        UIController.Get.dialogController.StartDialog(dialogChain);
    }
}
[System.Serializable]
public class Dialog
{
    [TextArea(1, 10)]
    public string message = "";
    public DialogProfile profile;
    public float textVolume = 0.7f;
    public float textWaitTime = 1f;
    public float textSpeed = 0.05f;
    public bool playerInput = true;

    [Header("Dialog Event")]
    public bool _DialogEvent;
    [ConditionalHide("_DialogEvent", true)] public DialogEvent unityEvent;

    [Header("MultipleChoice")]
    public bool multipleChoice;
    [ConditionalHide("multipleChoice")] public string buttonTextYes = "Yes";
    [ConditionalHide("multipleChoice")] public DialogCaller yes;
    [ConditionalHide("multipleChoice")] public string buttonTextNo = "No";
    [ConditionalHide("multipleChoice")] public DialogCaller no;


    public Dialog(DialogProfile profile, string message, float textVolume, float textWaitTime, float textSpeed, bool playerInput)
    {
        this.profile = profile;
        this.message = message;
        this.textVolume = textVolume;
        this.textWaitTime = textWaitTime;
        this.textSpeed = textSpeed;
        this.playerInput = playerInput;
    }
}
[System.Serializable]
public class DialogEvent 
{
    public UnityEngine.Events.UnityEvent events;
}

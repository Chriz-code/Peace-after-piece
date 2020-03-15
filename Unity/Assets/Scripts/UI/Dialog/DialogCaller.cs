using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCaller : MonoBehaviour
{
    public Dialog[] dialogChain = new Dialog[1];

    public void CallDialog(Transform caller, Transform sender)
    {
        UIController.Get.dialogController.StartDialog(dialogChain);
    }
}
[System.Serializable]
public class Dialog
{
    [TextArea(1, 10)]
    public string message = "";
    public DialogProfile profile;
    public bool overrideVolume = false;
    public float textVolume = 0.7f;
    public float textWaitTime = 1f;
    public float textSpeed = 0.05f;
    public bool playerInput = true;

    [Header("Dialog Event")]
    public bool dialogEventCheck;
    //[ConditionalHide("_DialogEvent", true)] 
    public DialogEvent dialogEvent;

    [Header("MultipleChoice")]
    public bool multipleChoice;
    //[ConditionalHide("multipleChoice")]
    public string buttonTextYes = "Yes";
    //[ConditionalHide("multipleChoice")]
    public DialogCaller yes;
    //[ConditionalHide("multipleChoice")]
    public string buttonTextNo = "No";
    //[ConditionalHide("multipleChoice")]
    public DialogCaller no;

    public Dialog()
    {

    }
    public Dialog(DialogProfile profile, string message, 
        float textVolume, float textWaitTime, 
        float textSpeed, bool playerInput)
    {
        this.profile = profile;
        this.message = message;
        this.textVolume = textVolume;
        this.textWaitTime = textWaitTime;
        this.textSpeed = textSpeed;
        this.playerInput = playerInput;
    }

    public Dialog(string message, DialogProfile profile, 
                    bool overrideVolume, float textVolume, 
                    float textWaitTime, float textSpeed, 
                    bool playerInput, bool dialogEventCheck, 
                    DialogEvent dialogEvent, bool multipleChoice, 
                    string buttonTextYes, DialogCaller yes, 
                    string buttonTextNo, DialogCaller no
                  )
    {
        this.message = message;
        this.profile = profile;
        this.overrideVolume = overrideVolume;
        this.textVolume = textVolume;
        this.textWaitTime = textWaitTime;
        this.textSpeed = textSpeed;
        this.playerInput = playerInput;
        this.dialogEventCheck = dialogEventCheck;
        this.dialogEvent = dialogEvent;
        this.multipleChoice = multipleChoice;
        this.buttonTextYes = buttonTextYes;
        this.yes = yes;
        this.buttonTextNo = buttonTextNo;
        this.no = no;
    }

    public Dialog Copy()
    {
        return new Dialog(message, profile, 
            overrideVolume, textVolume,
            textWaitTime, textSpeed, 
            playerInput, dialogEventCheck, 
            dialogEvent, multipleChoice, 
            buttonTextYes, yes, 
            buttonTextNo, no);
    }

}
[System.Serializable]
public class DialogEvent 
{
    public InteractEvent events;
}

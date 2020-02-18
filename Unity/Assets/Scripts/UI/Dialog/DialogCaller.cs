using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCaller : MonoBehaviour
{
    public Dialog[] dialogTree =  new Dialog[1];

    public void CallDialog()
    {
        //Debug.Log("Started Dialog");
        UIController.Get.dc.StartDialog(dialogTree);
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

    public Dialog(DialogProfile profile,string message, float textVolume, float textWaitTime, float textSpeed,bool playerInput)
    {
        this.profile = profile;
        this.message = message;
        this.textVolume = textVolume;
        this.textWaitTime = textWaitTime;
        this.textSpeed = textSpeed;
        this.playerInput = playerInput;
    }
}

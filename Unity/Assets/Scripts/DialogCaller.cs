using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCaller : MonoBehaviour
{
    public DialogProfile[] dialogProfiles = null;

    public void CallDialog()
    {
        Debug.Log("Started Dialog");
        UIController.Get.dc.StartDialog(dialogProfiles);
    }
}
[System.Serializable]
public class DialogProfile
{
    [TextArea(1, 10)]
    public string message = "";
    public Sprite profile = null;
    public Color color = Color.white;
    public AudioClip textSound = null;
    public float textVolume = 0.7f;
    public float textWaitTime = 1f;
    public float textSpeed = 0.05f;
    public bool playerInput = true;

    public DialogProfile(string message, Sprite profile, Color color, AudioClip textSound, float textVolume, float textWaitTime, float textSpeed,bool playerInput)
    {
        this.message = message;
        this.profile = profile;
        this.color = color;
        this.textSound = textSound;
        this.textVolume = textVolume;
        this.textWaitTime = textWaitTime;
        this.textSpeed = textSpeed;
        this.playerInput = playerInput;
    }
}
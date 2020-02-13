using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCaller : MonoBehaviour
{
    [TextArea(1, 10)]
    [SerializeField] string message = "";
    [SerializeField] Sprite profile = null;
    [SerializeField] Color color = Color.white;
    [SerializeField] AudioClip textSound = null;
    [SerializeField] float textWaitTime = 2f;
    [SerializeField] float textSpeed = 0.5f;


    public void CallDialog()
    {
        Debug.Log("Started Dialog");
        UIController.Get.dc.StartDialog(message, profile, color, textSound, textWaitTime, textSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDialog : MonoBehaviour
{
    [Header("From")]
    public DialogCaller dialogCaller = null;
    public int index = 0;
    [Header("To")]
    public Dialog dialog = null;

    [Header("Conditional")]
    public bool loopBetween = false;
    [ConditionalHide("loopBetween", true)]
    public Dialog dialog2 = null;

    public void DialogSwitch(Transform transform)
    {
        if (loopBetween && dialogCaller.dialogChain[index] == dialog)
            dialogCaller.dialogChain[index] = dialog2;
        else
            dialogCaller.dialogChain[index] = dialog;
    }
}

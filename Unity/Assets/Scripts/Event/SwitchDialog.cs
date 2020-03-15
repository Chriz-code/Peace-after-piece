using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDialog : MonoBehaviour
{
    [Header("Conditional")]
    public bool loopBetween = false;
    private Dialog dialog2 = null;

    [Header("From")]
    public DialogCaller dialogCaller = null;
    public int index = 0;
    [Header("To")]
    public Dialog dialog = null;


    private void Start()
    {
        dialog2 = dialogCaller.dialogChain[index];
    }

    public void DialogSwitch(Transform caller, Transform sender)
    {
        if (loopBetween && dialogCaller.dialogChain[index] == dialog)
            dialogCaller.dialogChain[index] = dialog2;
        else
            dialogCaller.dialogChain[index] = dialog;
    }

    public void InheritValues()
    {
        if(dialogCaller.dialogChain.Length+1 >= index)
        {
            Dialog dialog = new Dialog().Copy();
            dialog = dialogCaller.dialogChain[index].Copy();
        }
    }
}

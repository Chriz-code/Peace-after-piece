using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OntriggerEvent : MonoBehaviour
{
    public Perspective perspectiveCheck = Perspective.None;
    public DialogCaller dialogCaller = null;
    bool used = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (used)
            return;
        if (!collision.CompareTag("Player"))
            return;
        if (GameController.Get.CurrentPerspective != perspectiveCheck)
            return;

        dialogCaller.CallDialog();
        used = true;
    }
}

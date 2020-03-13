using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Codelock : PuzzelBase
{
    [SerializeField] Text x1 = null, x2 = null, x3 = null;
    [SerializeField] int id = 0;

    [SerializeField] int code = 123;

    [SerializeField] public InteractEvent unMatchEvent;

    private void OnEnable()
    {
        ClearText();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NotInteract();
        }
        KeyboardInput();
    }
    void KeyboardInput()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
                AddNumber(0);
            if (Input.GetKeyDown(KeyCode.Alpha1))
                AddNumber(1);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                AddNumber(2);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                AddNumber(3);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                AddNumber(4);
            if (Input.GetKeyDown(KeyCode.Alpha5))
                AddNumber(5);
            if (Input.GetKeyDown(KeyCode.Alpha6))
                AddNumber(6);
            if (Input.GetKeyDown(KeyCode.Alpha7))
                AddNumber(7);
            if (Input.GetKeyDown(KeyCode.Alpha8))
                AddNumber(8);
            if (Input.GetKeyDown(KeyCode.Alpha9))
                AddNumber(9);
        }
    }

    [SerializeField]Player_UserInput sender = null;
    public void Interact(Transform call, Transform sender)
    {
        call.parent.gameObject.SetActive(true);
        if (sender.GetComponent<Player_UserInput>())
        {
            this.sender = sender.GetComponent<Player_UserInput>();
            this.sender.enabled = false;
        }
    }
    public void NotInteract()
    {
        transform.parent.gameObject.SetActive(false);
        if (sender)
        {
            sender.enabled = true;
        }
    }


    public void AddNumber(int i)
    {
        if (ienumertaing)
            return;

        id++;
        switch (id)
        {
            case 1:
                x1.text = i.ToString();
                break;
            case 2:
                x2.text = i.ToString();
                break;
            case 3:
                x3.text = i.ToString();
                break;
        }
        if (id == 3)
        {
            CheckCode(x1.text + x2.text + x3.text);
        }
    }
    void CheckCode(string stringCode)
    {
        int code = int.Parse(stringCode);
        if (code == this.code)
        {
            Debug.Log("Code Accepted");
            Match = true;

            StartCoroutine(CorrectCode());
        }
        else
        {
            Debug.Log("Code Denied");
            unMatchEvent?.Invoke(transform, null);
            //Clear Text
            StartCoroutine(WrongCode());
        }
        id = 0;
    }
    void ClearText()
    {
        x1.text = "0";
        x2.text = "0";
        x3.text = "0";
        x1.color = Color.white;
        x2.color = Color.white;
        x3.color = Color.white;
        ienumertaing = false;
    }

    bool ienumertaing = false;
    IEnumerator WrongCode()
    {
        ienumertaing = true;

        x1.color = Color.red;
        x2.color = Color.red;
        x3.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        ClearText();
    }
    IEnumerator CorrectCode()
    {
        ienumertaing = true;

        x1.color = Color.green;
        x2.color = Color.green;
        x3.color = Color.green;
        yield return new WaitForSeconds(0.5f);
        NotInteract();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Codelock : MonoBehaviour
{
    [SerializeField] Text x1 = null, x2 = null, x3 = null;
    [SerializeField] int id = 0;

    [SerializeField] int code = 123;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    bool ienumertaing = false;
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
    private void OnEnable()
    {
        ClearText();
    }
    IEnumerator ClearTextAnimation()
    {
        ienumertaing = true;

        x1.color = Color.red;
        x2.color = Color.red;
        x3.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        ClearText();
    }
    void CheckCode(string stringCode)
    {
        int code = int.Parse(stringCode);
        if(code == this.code)
        {
            Debug.Log("Code Accepted");
            x1.color = Color.green;
            x2.color = Color.green;
            x3.color = Color.green;
            ienumertaing = true;
        }
        else
        {
            Debug.Log("Code Denied");
            //Clear Text
            StartCoroutine(ClearTextAnimation());
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

    void NumberInput()
    {

    }

}
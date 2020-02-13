using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Perspective perspective;
    private Rigidbody2D rb2D;

    public float moveInput;
    public float speed = 10f;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameController.Get.ComparePerspective(perspective))
        {
            Movement();
        }
    }

    private void OnEnable()
    {
        //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += SpelaMaginarByte;
       // GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += DisableOnEvent;
        //GameController.get.onChangePerspective += DisableOnEvent;
    }
    private void OnDisable()
    {
        //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective -= DisableOnEvent;
        //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective -= SpelaMaginarByte;

        //GameController.get.onChangePerspective -= DisableOnEvent;
    }
    void DisableOnEvent(GameController gc, Perspective change)
    {
        if (change == perspective)
        {
            Debug.Log("Mitt namn är " + perspective.ToString());
        }
    }

    void SpelaMaginarByte(GameController gc, Perspective change)
    {
        Debug.Log("MAGISKT BYTE YAY");
    }

    void Movement()
    {
            moveInput = Input.GetAxis("Horizontal");
            rb2D.velocity = new Vector2(moveInput * speed, rb2D.velocity.y);
            if (moveInput < 0)
            {
                this.gameObject.GetComponent<Transform>().localScale = new Vector3(-1f, 5f, 1f);
            }
            else if(moveInput > 0)
            {
                this.gameObject.GetComponent<Transform>().localScale = new Vector3(1f, 5f, 1f);
            }
    }
}

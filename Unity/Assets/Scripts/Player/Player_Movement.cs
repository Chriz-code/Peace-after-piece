using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public void ApplyMovement(Vector2 inputDirection, float speed)
    {
        transform.Translate(inputDirection * speed * Time.deltaTime);
    }
}

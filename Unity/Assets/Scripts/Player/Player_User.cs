using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Player_User : MonoBehaviour
{
    /*
     * Basically Player Input handler
     * All Player Update will run through here to easly turn off player input and physics if needed.
    */
    [SerializeField] Player player = null;
    [SerializeField] Player_Movement _Movement = null;
    private void Awake()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (!player)
        {
            TryGetComponent<Player>(out Player player);
            this.player = player;
        }
        if (!_Movement)
        {
            TryGetComponent<Player_Movement>(out Player_Movement _Movement);
            this._Movement = _Movement;
        }
    }

    void Update()
    {
        if (player)
        {
            if (Input.GetKeyDown(KeyCode.H))
                GameController.get.Test();
        }
    }
    private void FixedUpdate()
    {
        if (player)
        {
            if (_Movement)
            {
                HandleMovement();
            }
        }
    }
    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 move = new Vector2(x, 0);
        _Movement.ApplyMovement(move, player.speed);
    }
}

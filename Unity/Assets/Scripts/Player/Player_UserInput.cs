using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UserInput : MonoBehaviour
{
    [SerializeField] Player player = null;
    [SerializeField] Player_Movement _Movement = null;

    private void Start()
    {
        OnValidate();
    }
    private void OnValidate()
    {
        if(!player || !_Movement)
        {
            TryGetComponent<Player>(out Player player);
            this.player = player;
            TryGetComponent<Player_Movement>(out Player_Movement _Movement);
            this._Movement = _Movement;
        }
    }

    void Update()
    {
        if (GameController.Get.ComparePerspective(player.GetPerspective))
        {
            _Movement.Movement(Input.GetAxis("Horizontal"));
        }
    }

    private void OnDisable()
    {
        _Movement.Movement(0);
    }
}

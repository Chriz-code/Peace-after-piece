using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController get = null;
    private void Awake()
    {
        get = this;
    }
    
    public void Test()
    {
        print("Hello");
    }
}

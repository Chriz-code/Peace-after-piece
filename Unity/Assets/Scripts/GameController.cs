﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Perspective { None, Angela, Elenor }
public class GameController : MonoBehaviour
{
    public static GameController Get { get; private set; }

    public UnityAction<GameController, Perspective> onChangePerspective = delegate { };
    [SerializeField] Perspective currentPerspective = Perspective.None;
    public Perspective startPerspective = Perspective.Elenor;
    public bool skipNone = true;
    public Perspective CurrentPerspective
    {
        get
        {
            return currentPerspective;
        }
        set
        {
            value = (Perspective)Mathf.Repeat((int)value, Enum.GetNames(typeof(Perspective)).Length);
            if (value == 0 && skipNone)
                value = Perspective.Angela;
            if (currentPerspective != value)
            {
                currentPerspective = value;
                onChangePerspective?.Invoke(this, currentPerspective);
            }
        }
    }
    public KeyCode switchKey = KeyCode.Tab;

    private void Awake()
    {
        if (Get != null && Get != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Get = this;
        }
    }
    private void Start()
    {
        Invoke("GameControllerStart", 0.01f);
    }

    private void Update()
    {
        QuitGame();
        Switch();
    }
    void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
        }
    }
    void Switch()
    {
        if (Input.GetKeyDown(switchKey))
        {
            CurrentPerspective += 1;
        }
    }
    public bool ComparePerspective(Perspective perspective)
    {
        if (perspective == CurrentPerspective)
            return true;
        return false;
    }

    void GameControllerStart()
    {
        CurrentPerspective = startPerspective;
    }
}
//To subscribe to this singelton, subscribe on Start() and unsubscribe on OnDisable()
using System;
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
    public Player_UserInput GetActivePlayer
    {
        get
        {
            foreach (Player_UserInput userInput in FindObjectsOfType<Player_UserInput>())
            {
                userInput.TryGetComponent<ThisPerspective>(out ThisPerspective playerPerspective);
                if (playerPerspective.perspective == CurrentPerspective)
                {
                    return userInput;
                }
            }
            return null;
        }
    }


    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Get != null && Get != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Get = this;
        }
    }
    private void Update()
    {
        QuitGame();
        Switch();
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEditor.EditorApplication.isPlaying = false;
        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
#else
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
#endif
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
    public void GameControllerStart()
    {
        CurrentPerspective = startPerspective;
    }
}
//To subscribe to this singelton, subscribe on Start() and unsubscribe on OnDisable()
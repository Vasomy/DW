using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UGameState
{
    MainMenu,
    GameRuntime,
    GamePauseMenu
}

public class GameGlobalContext : MonoBehaviour
    // 游戏全局状态文本
    // 例如游戏状态 暂停 进行， 处于什么界面（主菜单，游戏运行时，游戏暂停菜单等等）
{
    static public GameGlobalContext instance;

    public UTimer mTimer = new UTimer();
    public UGameState mGameState = UGameState.MainMenu;

    private Vector3 mCursorPos = Vector3.zero;    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        mTimer.Update();
        CalculateCurrentCursorPos();    
    }

    private void Awake()
    {
        
    }

    void CalculateCurrentCursorPos()
    {
        Vector3 MousePos = Input.mousePosition;
        mCursorPos = Camera.main.ScreenToWorldPoint(MousePos);
    }

    public static Vector2 GetMousePos2()
    {
        if (instance == null)
            Debug.LogError("Game Global Context hasn't been initialized!");
        return instance.mCursorPos;
    }
    public static Vector3 GetMousePos3()
    {
        if (instance == null)
            Debug.LogError("Game Global Context hasn't been initialized!");
        return instance.mCursorPos;
    }
    public static Vector3 GetMouseScreenPos3()
    {
        if(instance == null)
            Debug.LogError("Game Global Context hasn't been initialized!");
        return Input.mousePosition;
    }
    public static Vector2 GetMouseScreenPos2()
    {
        if (instance == null)
            Debug.LogError("Game Global Context hasn't been initialized!");
        return Input.mousePosition;
    }
}

public class UTimer
{
    public float CurrentTime = 0.0f;
    public float TotalTime = 0.0f;
    public float PauseTime = 0.0f;
    public float DeltaTime;
    public bool IsPause = false;
    public void Update()
    {
        DeltaTime = Time.deltaTime;
        if(!IsPause)
        {
            CurrentTime += DeltaTime;
        }
        else
        {
            PauseTime += DeltaTime;
        }
        TotalTime += DeltaTime;
    }
    
    public void Reset()
    {

    }
}
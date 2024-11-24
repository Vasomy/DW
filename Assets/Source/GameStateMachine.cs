using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModeState
{
    Defense,
    Build,
    Select,
    Arrange
}
public class GameStateMachine : MonoBehaviour
    // 游戏游玩进行时的状态机，
    // 管理当前时段游戏模式，如
    // 游戏模式 准备阶段和战争阶段
    // 玩家可以切换的 建造模式 选择模式 指挥模式（类似RTS中排兵布阵）
    // （以上模式仅在准备阶段，战争阶段会强制停止）
{
    public GameModeState CurrentState;
    public GameMode CurrentMode;
    // Start is called before the first frame update
    void Start()
    {
        CurrentMode = new BuildMode("Build");
        CurrentState = GameModeState.Build;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(((BuildMode)CurrentMode).SelectedBuildingName);
        }
        CurrentMode.Update();
    }

    public static GameStateMachine instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public Dictionary<string, GameMode> mModes = new Dictionary<string, GameMode>();

}

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
    // ��Ϸ�������ʱ��״̬����
    // ����ǰʱ����Ϸģʽ����
    // ��Ϸģʽ ׼���׶κ�ս���׶�
    // ��ҿ����л��� ����ģʽ ѡ��ģʽ ָ��ģʽ������RTS���ű�����
    // ������ģʽ����׼���׶Σ�ս���׶λ�ǿ��ֹͣ��
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct GameStats
{
    public int gold;
    public int wood;
    public int metal;
    public int carbon;
}

public class GameRuntimeContext : MonoBehaviour 
{
    public BuildingsStorge mBuildingsStorge = new BuildingsStorge();

    public GameObject MainCamera=>Camera.main.gameObject;
    public float MainCameraMoveSensitivity = 0.5f;
    bool IsHoldRightButton = false;
    [SerializeField]
    Vector2 LastCursorPos = Vector2.zero;
    [SerializeField]
    Vector2 CurrentCursorPos = Vector2.zero;
    
    [Space]
    public GameStats mStats;
    [InspectorLabel("Camera Properties")]
    [SerializeField]
    float CameraMoveErrorMetric = 0.3f;// 移动屏幕时的最下误差
    
    void ProccessInput()
    {
        if(Input.GetMouseButtonDown(1))
        {
            IsHoldRightButton=true;
            LastCursorPos = GameGlobalContext.GetMousePos2();
        }
        ProccessMainCameraBehavior();
        if(Input.GetMouseButtonUp(1))
        {
            IsHoldRightButton = false;
        }

    }
    private void ProccessMainCameraBehavior()
    {
        if(IsHoldRightButton) 
        { 
            CurrentCursorPos = GameGlobalContext.GetMousePos2();
            if (CompareFunction.ManhattanDistance(CurrentCursorPos, LastCursorPos) <= CameraMoveErrorMetric)
            {
                return;
            }
            Vector2 DiffVec = CurrentCursorPos - LastCursorPos;
            DiffVec *= MainCameraMoveSensitivity;
            MainCamera.transform.position = MainCamera.transform.position+(Vector3)DiffVec;
            LastCursorPos = CurrentCursorPos;
        }
    }

    public Buildings SelectedBuilding = null;
    public BuildHandle BuildingGenerator;// 在建筑模式下，且在选中非空slot的情况下，在BuildMode的Update方法中调用Generate方法
    

    public GameObject mBuildModeUI; // build UI Controller

    public BuildingPreview mBuildPreview;

    public static GameRuntimeContext instance;

    public List<BuildingsInfo> AllBuildingsInfo;
    private Dictionary<string, BuildingsInfo> mBuildingsInfo;

    public Dictionary<string,BuildingsInfo> UnLockedBuildingsInfo;
    public BuildingsInfo GetBuildingInfo(string name)
    {
        return mBuildingsInfo[name];
    }

    public LevelInfo CurrentLevel;
    public void RegisterBuilding(GameObject go)// 为了存档和序列化
    {
        CurrentLevel.AllBuildings.Add(go);
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        mBuildingsInfo = new Dictionary<string, BuildingsInfo>();
        UnLockedBuildingsInfo = new Dictionary<string, BuildingsInfo>();
        foreach(var b in AllBuildingsInfo)
        {
            mBuildingsInfo.Add(b.BuildingName, b);
        }
        AllBuildingsInfo.Clear();
       
        UnLockedBuildingsInfo.Add("Tower", mBuildingsInfo["Tower"]);
        UnLockedBuildingsInfo.Add("Wall", mBuildingsInfo["Wall"]);
    }

    public void UnLockBuilding(string name)
    {
        mBuildingsInfo.TryGetValue(name,out var bd);
        if(bd!=null)
        {
            UnLockedBuildingsInfo.Add(name, bd);
        }
        else
        {
            Debug.LogError("Error building for unlock!");
            return;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            ReleaseBuildModeCapture();
        }
        ProccessInput();

        
    }



    private void OnDrawGizmos()
    {
        if (SelectedBuilding != null)
        {
            SelectedBuilding.DrawRange();
        }
    }

    private void ReleaseBuildModeCapture()
    {
        SelectedBuilding = null;
        GridManager.UnRegisterPreviewOccupied();
    }

}
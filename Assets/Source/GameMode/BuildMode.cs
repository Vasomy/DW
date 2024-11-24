using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildMode : GameMode
{
    public string SelectedBuildingName = "Null";
    public bool IsSelected = false; // 是否选择了非空的建筑槽
    public BuildMode(string state_name) : base(state_name)
    {
    }

    bool Dirty = false;
    public override void BeginMode()
    {
        base.BeginMode();
        Dirty = false;
    }

    public override void EndMode()
    {
        base.EndMode();
        GameRuntimeContext.instance.mBuildPreview.ResetData();
        GameRuntimeContext.instance.mBuildPreview.gameObject.SetActive(false);
        IsSelected = false;
    }

    public override void Update()
    {
        base.Update();
        if(IsSelected)
        {
            Vector3 WorldPos = GameGlobalContext.GetMousePos3();
            WorldPos.z = -2;
            GameRuntimeContext.instance.mBuildPreview.transform.position = WorldPos;

        }
        if(Dirty)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameRuntimeContext.instance.mBuildPreview.ResetData();
                GameRuntimeContext.instance.mBuildPreview.gameObject.SetActive(false);
                IsSelected = false;
                SelectedBuildingName = null;
            }
            if (Input.GetMouseButtonDown(0))
            {
                GameRuntimeContext.instance.BuildingGenerator.Generate(SelectedBuildingName);
            }
        }
        if(IsSelected)
        {
            if(!Dirty)
            {
                Dirty = true;
                return;
            }
            
        }
        else
        {
            Dirty = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildUISlot : MonoBehaviour 
{
    public bool IsNull = false; 
    public SpriteItem mSpriteItem;
    public Image mImage;

    public string SlotName;

    private void Start()
    {
    }
    public void Init(string BuildingName)
    {
        SlotName = BuildingName;
        mSpriteItem = SpriteManager.instance.GetSpriteItem(BuildingName);
        mImage.sprite = mSpriteItem.Sprite;
    }

    public void InitAsDelete()
    {
        SlotName = "Delete";
        mSpriteItem = SpriteManager.instance.GetSpriteItem("Delete");
        mImage.sprite = mSpriteItem.Sprite;
    }

    public void OnClick()
    {
        if (GameStateMachine.instance.CurrentState != GameModeState.Build)
            return;
        BuildMode mode = (BuildMode)GameStateMachine.instance.CurrentMode;
        if (!IsNull)
        {
            GameRuntimeContext.instance.mBuildPreview.gameObject.SetActive(true);
            GameRuntimeContext.instance.mBuildPreview.Init(SlotName, mSpriteItem.Sprite);
            mode.SelectedBuildingName = SlotName;
            mode.IsSelected = true;
        }
        else
        {
            GameRuntimeContext.instance.mBuildPreview.gameObject.SetActive(false);
            mode.IsSelected = false;
        }
    }
}

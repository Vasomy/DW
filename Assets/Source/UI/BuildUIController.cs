using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildUIController : UIController
{
    public GameObject BuildSlotPrefab;
    public List<GameObject> BuildSlotList;
    private void Start()
    {
        BuildSlotList = new List<GameObject>();

        float currentX = 0.0f;
        float StepX = 60.0f;
        // 每次进游戏的初始化
        // 将所有已解锁的 building 制作相应的 Slot供 建造模式使用
        foreach(var bd in GameRuntimeContext.instance.UnLockedBuildingsInfo)
        {
            var slot = Instantiate(BuildSlotPrefab,Target.transform);
            slot.name += bd.Value.BuildingName; 
            var bd_s = slot.GetComponent<BuildUISlot>();
            bd_s.Init(bd.Value.BuildingName);
            bd_s.mImage.rectTransform.localPosition = new Vector3(currentX, 0, 0);
            BuildSlotList.Add(slot);
            currentX += StepX;
        }

        var deleteSlot = Instantiate(BuildSlotPrefab,Target.transform);
        deleteSlot.name += "Delete";
        var bds =deleteSlot.GetComponent<BuildUISlot>();
        bds.InitAsDelete();
        bds.mImage.rectTransform.localPosition = new Vector3(currentX,0,0);
        BuildSlotList.Add(deleteSlot);
    }
    public override void Begin()
    {
        base.Begin();
        Target.SetActive(true);

    }

    private void Update()
    {
        
    }

    public override void End()
    {
        base.End();
        Target.SetActive(false);
    }
}

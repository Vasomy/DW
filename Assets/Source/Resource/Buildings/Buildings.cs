using System;
using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Buildings : StaticEntity
    // 所有的建筑本质上为一个带碰撞的矩形
    // 所有的建筑（如果在地面上）都会占据某些地块（被占据的地块无法进行路径检测）
    // 该脚本应该被挂载到GameObject并存为Prefab
    // 当该脚本所挂载的Prefab被Instantiated时，

    // AsBuilt()作为被建造，在建造模式中建造建筑时，被以这种方法初始化

   
{

    public int Id; // 唯一id作为查找的key值

    // 可能 还会有一些 背景 轮廓线的渲染器-- 暂时没有实现

    public Vector2 Origin => transform.position;// 建筑中心
    public BuildingsInfo Info;

    protected List<StaticComponent> mStaticComponent = new List<StaticComponent>();
    protected List<TickableComponent> mTickableComponent = new List<TickableComponent>();
    [Serializable]
    public class BuildingStats
    {
        public float HP;
        public float AttackRange;
        public float Attack;

        public int Armor;

        public List<int> GridFlag = new List<int>((int)GridFlags.FlagNums);
    }
    public BuildingStats Stats = new BuildingStats();

    public override void OnInit()
    {
        base.OnInit();
        Init();
    }
    public void Init()
    {
        gameObject.tag = "Buildings";
        Id = UIdAllocator.Instance.AllocateId();
        this.Info = BuildHandle.currentBuildingsInfo;

        Stats.HP = Info.HP;
        Stats.AttackRange = Info.Radius;
        Stats.Attack = Info.Attack;
        Stats.Armor = Info.Armor;
        for(int i =0;i<Stats.GridFlag.Capacity;++i)
        {
            Stats.GridFlag.Add(0);
        }
        
        m_sr.sprite = SpriteManager.instance.GetSprite(Info.BuildingName);
        LoadComponent();
        Debug.Log(Info.BuildingName);
        CalculateOccupyGrid();
    }

    virtual public void LoadComponent()
        // 在子类实现各个建筑应该挂载的组件
    {

    }

    public void CalculateOccupyGrid()
    {


        if(Info.OccupyType == BuildingsOccupyType.Circle)
            GridManager.RegisterGridOccupied(Id,Origin, Info.OccupyArea);
        else if(Info.OccupyType == BuildingsOccupyType.Square)
            GridManager.RegisterGridOccupiedSquare(Id,Origin,Info.OccupyWidth, Info.OccupyHeight);

    }

    public void UnRegisterOccupiedGrid()
    {
        if (Info.OccupyType == BuildingsOccupyType.Circle)
        {
            GridManager.UnRegisterGridOccupied(Id, transform.position, Info.OccupyArea);
        }
        else if(Info.OccupyType == BuildingsOccupyType.Square)
        {
            GridManager.UnRegisterGridOccupiedSquare(Id, transform.position, Info.OccupyWidth, Info.OccupyHeight);
        }

    }


    public void DrawRange(int VertexCount = 50) // 当被选中时在BuildMode中调用该方法 绘制范围
    {
        Vector2 center = transform.position;
        DrawCircle(center, Info.Radius,Color.red);
        DrawCircle(center, Info.OccupyArea,Color.blue);
    }

    private void DrawCircle(Vector2 center, float Radius,Color color,int VertexCount = 50)
    {
        float deltaTheta = (2f * Mathf.PI) / VertexCount;
        float theta = 0f;
        int i = 0;
        Gizmos.color = Color.red;
        Vector3 oldPos = new Vector3(Radius * Mathf.Cos(theta), Radius * Mathf.Sin(theta), 0f);
        oldPos = oldPos + (Vector3)center;
        theta += deltaTheta;
        i++;
        Gizmos.color = color;
        for (i = 1; i < VertexCount + 1; i++)
        {
            Vector3 pos = new Vector3(Radius * Mathf.Cos(theta), Radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, (Vector3)center + pos);
            oldPos = transform.position + pos;

            theta += deltaTheta;
        }
    }

    public void OnMouseDown()
    {
        GameRuntimeContext.instance.SelectedBuilding = this;
    }
}
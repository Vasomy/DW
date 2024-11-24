using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuildingsType
{
    Emitter, // 发射器，弓箭塔...
    General,// 常规建造，如 资源单位（生产），功能建筑。普遍没有攻击性
    Generator, // 生成器，产生 作战单位，生产单位（农民）（可能会有）。。。
}

public enum BuildingsOccupyType
{
    Circle,
    Square
}

public enum GridFlags
{
    None, // 该grid不会被影响 小怪的路径检测
    Normal,// 该grid作为普通影响
    Wall,// 作为城墙影响，（大部分怪物，不能将城墙作为路径）
    FlagNums
}

[Serializable]
[CreateAssetMenu(fileName = "New Building Data", menuName = "Data/New Building")]
public class BuildingsInfo : ScriptableObject // 被以 BuildingName为Key值 存在 GameRuntimeContext中
                                              // 储存各种建筑的信息，目前只有 Radius和名字
{
    public string BuildingName =>name; // 对应Spritename
    public float Radius = 0.0f;// 攻击半径
    
    public BuildingsOccupyType OccupyType = BuildingsOccupyType.Square;
 
    public float OccupyArea = 0.0f;//占地面积半径 为固定的几个等级
    public float OccupyWidth = 0.0f;
    public float OccupyHeight = 0.0f;

    public float HP = 50.0f;
    public float Attack = 5.0f;
    public int Armor = 0;// Damage = min(1,Attack-Armor);
    public float FunctionGap = 1.0f;// 功能作用时间的间隔，例如攻击间隔
    public BuildingsType type = BuildingsType.Emitter;
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuildingsType
{
    Emitter, // ��������������...
    General,// ���潨�죬�� ��Դ��λ�������������ܽ������ձ�û�й�����
    Generator, // ������������ ��ս��λ��������λ��ũ�񣩣����ܻ��У�������
}

public enum BuildingsOccupyType
{
    Circle,
    Square
}

public enum GridFlags
{
    None, // ��grid���ᱻӰ�� С�ֵ�·�����
    Normal,// ��grid��Ϊ��ͨӰ��
    Wall,// ��Ϊ��ǽӰ�죬���󲿷ֹ�����ܽ���ǽ��Ϊ·����
    FlagNums
}

[Serializable]
[CreateAssetMenu(fileName = "New Building Data", menuName = "Data/New Building")]
public class BuildingsInfo : ScriptableObject // ���� BuildingNameΪKeyֵ ���� GameRuntimeContext��
                                              // ������ֽ�������Ϣ��Ŀǰֻ�� Radius������
{
    public string BuildingName =>name; // ��ӦSpritename
    public float Radius = 0.0f;// �����뾶
    
    public BuildingsOccupyType OccupyType = BuildingsOccupyType.Square;
 
    public float OccupyArea = 0.0f;//ռ������뾶 Ϊ�̶��ļ����ȼ�
    public float OccupyWidth = 0.0f;
    public float OccupyHeight = 0.0f;

    public float HP = 50.0f;
    public float Attack = 5.0f;
    public int Armor = 0;// Damage = min(1,Attack-Armor);
    public float FunctionGap = 1.0f;// ��������ʱ��ļ�������繥�����
    public BuildingsType type = BuildingsType.Emitter;
}


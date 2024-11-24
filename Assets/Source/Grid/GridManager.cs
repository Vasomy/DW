using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OccupiedInfo
{
    public bool isOccupied = false;
    public bool isWall = false;
    
}

public class GroundEffect
{

}

public class GridManager : MonoBehaviour
    // �����ͼ�����е� �������񻮷ֵĴ�С�ɿ����߾��ã�
    // ����������Ƿ�ռ�ݣ��������ڱ�����ʱ����������ռ�ݵ������ϴ������ࣩ
    // ս����������ʵ�֣�
{
    Hashtable PreviewOccupiedGrid;
    bool IsLegal = true;// ��ǰԤ����ռ��grid�Ƿ����Ѵ��ڵĽ������ص�

    Hashtable OccupiedGrid;// val Ϊ ռ�ݸõصĽ�����id
    //Hashtable<Vector2Int,int> OccupiedGrid; // ��¼���е�ռ�ݵ�Grid����

    OccupiedInfo GetOccupiedInfo_v(Vector2 point)
    {
        OccupiedInfo ret = new OccupiedInfo();
        var id = GetId_v(point);
        if(id==-1)
        {
            ret.isOccupied = false;
            return ret;
        }
        var bdStats = GameRuntimeContext.instance.mBuildingsStorge.Find(id).Stats;
        if (bdStats.GridFlag[(int)GridFlags.Wall]!=0)
        {
            ret.isWall = true;
        }
        ret.isOccupied = true;
        return ret;
    }

    bool IsOccupied_v(Vector2 point)
    {
        return OccupiedGrid.ContainsKey(GetPos(point));
    }

    int GetId_v(Vector2 point)// ��ȡ���λ�� ��ռ�ݸõصĽ���id
    {
        var pos = GetPos(point);
        if(OccupiedGrid.ContainsKey(pos))
        {
            return (int)OccupiedGrid[pos];
        }
        return -1;
    }

    public float XStep = 0.5f;// ���ڼ���ռ������ľ��ȣ�����0.5���� һ��unity��׼�ĸ����л���4��С����
    public float YStep = 0.5f;
    public Vector2 Anchor = Vector2.zero;// ����������㣬Ĭ��Ϊ0 0��������Ķ�

    bool CalculateOccupySquare(int OccupiedId, Vector2 center, float width, float height, bool isDelete = false, bool isPreview = false)
        //���еĵ㲻�ܱ��ظ����㣬����ظ�����false
    {
        bool ret = true;
        Vector2Int centerPos = GetPos(center);
        Vector2 centerPoint = center;


        Vector2 leftPoint = centerPoint;
        leftPoint.x -= width;
        Vector2 rightPoint = centerPoint;
        rightPoint.x += width;

        Vector2 topPoint = centerPoint;
        topPoint.y += height;
        Vector2 bottomPoint = centerPoint;
        bottomPoint.y -= height;

        Vector2Int leftPos = GetPos(leftPoint);
        Vector2Int rightPos = GetPos(rightPoint);
        Vector2Int topPos = GetPos(topPoint);
        Vector2Int bottomPos = GetPos(bottomPoint);

        int squre_width = (rightPos.x - leftPos.x) / 2;
        int squre_height = (topPos.y - bottomPos.y) / 2;

        if(isPreview)
        {
            IsLegal = true;
        }

        for(int i = -squre_width;i<=squre_width;++i)
        {
            for(int j = -squre_height;j<=squre_height;++j)
            {
                Vector2Int currentPos = centerPos;
                currentPos.x += i;
                currentPos.y += j;

                if (isPreview)
                {
                    PreviewOccupiedGrid.Add(currentPos, OccupiedId);
                    if (OccupiedGrid.ContainsKey(currentPos))
                        IsLegal = false;
                    continue;
                }

                if (!OccupiedGrid.ContainsKey(currentPos))
                {
                    if (!isDelete)
                        OccupiedGrid.Add(currentPos, OccupiedId);
                }
                else
                {
                    if(!isDelete)
                    {
                        ret = false;
                        return ret;
                    }
                    else
                    {
                        OccupiedGrid.Remove(currentPos);
                    }
                }
            }
        }

        return ret;
    }
    bool CalculateOccupy(int OccupiedId,Vector2 center, float radius, bool isDelete = false, bool isPreview = false)
    {

        Vector2Int centerPos = new Vector2Int();
        Vector2 centerPoint = center;
        
        centerPos = GetPos(center);
        

        Vector2 leftPoint = center;
        leftPoint.x-= radius;
        Vector2 rightPoint = center;
        rightPoint.x+= radius;
        Vector2 topPoint = center;
        topPoint.y+= radius;
        Vector2 bottomPoint = center;
        bottomPoint.y-= radius;

        Vector2Int leftPos = GetPos(leftPoint);
        Vector2Int rightPos = GetPos(rightPoint);
        Vector2Int topPos = GetPos(topPoint);
        Vector2Int bottomPos = GetPos(bottomPoint);
        
        int width = (rightPos.x - leftPos.x)/2;
        int height = (topPos.y - bottomPos.y)/2;

        if(isPreview )
        {
            isPreview = true;
        }

        for(int i = -width; i <= width;++i)
        {
            for(int j = -height; j <= height;++j)
            {
                Vector2Int currentPos = centerPos;
                currentPos.x += i;
                currentPos.y += j;
                Vector2 currentPoint = GetCenterPoint(currentPos);
                float dis = GetLength(currentPoint - centerPoint);
                if(dis<=radius)
                {

                    if(isPreview)
                    {
                        PreviewOccupiedGrid.Add(currentPos, OccupiedId);
                        if (OccupiedGrid.ContainsKey(currentPos))
                            IsLegal = false;
                        continue;
                    }

                    if (!OccupiedGrid.ContainsKey(currentPos))
                    {
                        if (!isDelete)
                            OccupiedGrid.Add(currentPos, OccupiedId);
                    }
                    else
                    {
                        if (!isDelete)
                            OccupiedGrid[currentPos] = OccupiedId;
                        else
                        {   
                            OccupiedGrid[currentPos] = -1;
                        }
                        
                    }
                    //Debug.Log(currentPos);
                }
            }
        }

        return true;
    }

    float GetLength(Vector2 vec)
    {
        float ret;
        ret = Mathf.Sqrt(vec.x*vec.x+vec.y*vec.y);
        return ret;
    }
    //����ʵ�ʵ�X��Y�����꣬��ȡ��Grid������
    Vector2Int GetPos(Vector2 point)
    {
        Vector2Int ret = new Vector2Int();

        int x = (int)((point.x - (float)Anchor.x)/XStep);
        int y = (int)((point.y - (float)Anchor.y)/YStep);

        ret.x = x;
        ret.y = y;

        return ret;
    }
        
    Vector2 GetCenterPoint(Vector2Int Pos)
    {
        Vector2 ret = new Vector2Int();

        float x = Pos.x * XStep +  Anchor.x;
        float y = Pos.y * YStep + Anchor.y;

        float next_x = x+XStep;
        float next_y = y+YStep;

        float center_x = (x + next_x) / 2.0f;
        float center_y = (y + next_y) / 2.0f;

        ret.x = center_x;
        ret.y = center_y;

        return ret;
    }

    public static GridManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        OccupiedGrid = new Hashtable();
        PreviewOccupiedGrid = new Hashtable();
    }

    private void OnDrawGizmos()
    {
        if (OccupiedGrid == null)
            return;
        foreach(DictionaryEntry pair in OccupiedGrid)
        {
            //Debug.Log(pair.Key);
            var pos = (Vector2Int)pair.Key;
            var point = GetCenterPoint(pos);

            var id = (int)pair.Value;
            var bdStats = GameRuntimeContext.instance.mBuildingsStorge.Find(id).Stats;
            if (bdStats.GridFlag[(int)GridFlags.Wall]!=0)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color= Color.red;
            }


            Gizmos.DrawWireCube(point, Vector3.one/(1.0f/XStep));
            //Debug.Log(point);
        }
        Gizmos.color = Color.red;
        foreach(DictionaryEntry pair in PreviewOccupiedGrid)
        {
            //Debug.Log(pair.Key);
            var pos = (Vector2Int)pair.Key;
            var point = GetCenterPoint(pos);
            Gizmos.DrawWireCube(point, Vector3.one / (1.0f / XStep));
            //Debug.Log(point);
        }
    }
    // ����API
    static private void DebugNullInstance()
    {
        Debug.LogError("GridManager instance hasn't been initialized!");
    }
    static public bool RegisterGridOccupied(int OccupiedId, Vector2 center,float Radius)
    {
        if (instance == null)
            DebugNullInstance();
        return instance.CalculateOccupy(OccupiedId,center, Radius);
    }

    static public bool RegisterGridOccupiedSquare(int OccupiedId, Vector2 center,float width,float height)
    {
        if(instance == null) 
            DebugNullInstance();

        return instance.CalculateOccupySquare(OccupiedId,center, width, height);
    }

    static public void UnRegisterGridOccupied(int OccupiedId,Vector2 center,float Radius)
    {
        if(instance == null)
           DebugNullInstance();
        instance.CalculateOccupy(OccupiedId,center, Radius, true);
    }

    static public void UnRegisterGridOccupiedSquare(int OccupiedId, Vector2 center,float width,float height)
    {
        if(instance == null)
        {
            DebugNullInstance();
        }
        instance.CalculateOccupySquare(OccupiedId, center, width, height, true);
    }

    static public bool IsOccupied(Vector2 point)
    {
        if(instance ==null)
            DebugNullInstance();
        return instance.IsOccupied_v(point);
    }

    static public int GetId(Vector2 point)
    {
        if(instance==null)
            DebugNullInstance();
        return instance.GetId_v(point); 
    }

    static public OccupiedInfo GetOuccupiedInfo(Vector2 point)
    {
        if(instance == null)
            DebugNullInstance();
        return instance.GetOccupiedInfo_v(point);
    }
    static public bool RegisterPreviewOccupiedSquare(Vector2 center,float width,float height)
    {
        if(instance == null)
            DebugNullInstance();
        instance.PreviewOccupiedGrid.Clear();
        return instance.CalculateOccupySquare(0,center,width,height,false,true);
    }
    static public void UnRegisterPreviewOccupied()
    {
        if (instance == null)
            DebugNullInstance();
        instance.PreviewOccupiedGrid.Clear();
    }
    static public bool CanBuild()
    {
        if (instance == null)
            DebugNullInstance();
        return instance.IsLegal;
    }
      
    static public Vector2 TransPos(Vector2 pos) // ��������
    {
        if(instance == null)
            DebugNullInstance();
        return instance.GetCenterPoint(instance.GetPos(pos));
    }
}

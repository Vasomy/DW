using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SelectSystem : MonoBehaviour
{
    List<Entity> m_selected_entities = new List<Entity>();

    public static List<Entity> Get()
    {
        return instance.m_selected_entities;
    }

    static public SelectSystem instance;
    private void Awake()
    {
        instance = this; 
    }
    [SerializeField]
    bool onDrawingRect = false;
    [SerializeField]
    Vector3 RectBeginPos = Vector3.zero;
    [SerializeField]
    Vector3 RectEndPos = Vector3.zero;
    [Space]
    [SerializeField]
    Vector3 RectLeftBotPos = Vector3.zero;
    [SerializeField]
    Vector3 RectRightTopPos = Vector3.zero;
    private void Update()
    {
        if(GameGlobalContext.instance.mGameState!=UGameState.GameRuntime)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            onDrawingRect = true;
            RectBeginPos = GameGlobalContext.GetMouseScreenPos3();
            
        }
        if(onDrawingRect && Input.GetMouseButtonUp(0)) {
            onDrawingRect = false;
            if(CompareFunction.ManhattanDistance(RectBeginPos,RectEndPos)>5.0f)
                CheckSelectEntity(RectBeginPos,RectEndPos);
        }
        if(onDrawingRect)
        {
            RectEndPos = GameGlobalContext.GetMouseScreenPos3();
        }
        // do someting on selected entity
        // try to move
        var mousePos2 = GameGlobalContext.GetMousePos2();
        if(Input.GetMouseButtonDown(1))
        {
            foreach(var entity in m_selected_entities)
            {
                if(entity.entityType == EntityType.Controlable)
                {
                    ((ControllableUnits)entity).MoveTo(mousePos2);
                }
            }
        }



    }

    void CheckSelectEntity(Vector3 rectBeginPos,Vector3 rectEndPos)
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Entity");
        ReleaseCapture();
        //Vector2 leftBotPos = new Vector2(Mathf.Min(rectBeginPos.x,rectEndPos.x),Mathf.Min(rectBeginPos.y,rectEndPos.y));
        //Vector2 rightTopPos = new Vector2(Mathf.Max(rectBeginPos.x,rectEndPos.x),Mathf.Max(rectBeginPos.y,rectEndPos.y));
        RectLeftBotPos.Set(Mathf.Min(rectBeginPos.x, rectEndPos.x), Mathf.Min(rectBeginPos.y, rectEndPos.y), 0);
        RectRightTopPos.Set(Mathf.Max(rectBeginPos.x, rectEndPos.x), Mathf.Max(rectBeginPos.y, rectEndPos.y), 0);
        foreach (var unit in units)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
            if(
                screenPos.x>= RectLeftBotPos.x&&screenPos.x<= RectRightTopPos.x&&
                screenPos.y>= RectLeftBotPos.y&&screenPos.y<= RectRightTopPos.y
                )
            {
                SelectEntityOneByOne(unit.GetComponent<Entity>());
            }
        }
    }

    void SelectEntityOneByOne(Entity entity)
    {
        entity.mSelectable.Capture();
        instance.m_selected_entities.Add(entity);
    }
    public GUIStyle rectStyle;
    private void OnGUI()
    {
        return;
        if (onDrawingRect)
        {
            float Xmin = Mathf.Min(RectBeginPos.x, RectEndPos.x);
            float Xmax = Mathf.Max(RectBeginPos.x, RectEndPos.x);
            float Ymin = Mathf.Min(RectBeginPos.y, RectEndPos.y);
            float Ymax = Mathf.Max(RectBeginPos.y, RectEndPos.y);

            //ȷ������Ķ�λ�㣨���Ͻǵ㣩�ĺ����ꡢ�����꣬�Լ�����ĺ����Ⱥ�����߶�
            Rect rect = new Rect(Xmin, Screen.height - Ymax, Xmax - Xmin, Ymax - Ymin);

            //����
            GUI.Box(rect, "",rectStyle);

        }
    }
    public Material GLRectMat;//��ͼ�Ĳ��ʣ���Inspector������
    public Color GLRectColor;//���ε��ڲ���ɫ����Inspector������
    public Color GLRectEdgeColor;//���εı߿���ɫ����Inspector������


    void OnPostRender()
    {
        if (onDrawingRect)
        {
            //׼������:��ȡȷ�����ο������������ĸ�����ֵ
            float Xmin = Mathf.Min(RectBeginPos.x, RectEndPos.x);
            float Xmax = Mathf.Max(RectBeginPos.x, RectEndPos.x);
            float Ymin = Mathf.Min(RectBeginPos.y, RectEndPos.y);
            float Ymax = Mathf.Max(RectBeginPos.y, RectEndPos.y);

            GL.PushMatrix();//GL��ջ
                            //�����ֻ��Ҫ֪��GL.PushMatrix()��GL.PopMatrix()�ֱ��ǻ�ͼ�Ŀ�ʼ�ͽ����ź�,��ͼָ��д�������м�

            if (!GLRectMat)
                return;
  
            GLRectMat.SetPass(0);//�����߿����rectMat

            GL.LoadPixelMatrix();//��������Ļ�����ͼ


            /*------��һ�������ƾ���------*/
            GL.Begin(GL.QUADS);//��ʼ���ƾ���,��һ�λ����ǿ��м�İ�͸�����֣��������߽���

            GL.Color(GLRectColor);//���þ��ε���ɫ��ע��GLRectColor�������Ϊ��͸��

            //�������ε��ĸ�����
            GL.Vertex3(Xmin, Ymin, 0);//������һ���㣬��������½ǵ㣬��Ϊ��1
            GL.Vertex3(Xmin, Ymax, 0);//�����ڶ����㣬��������Ͻǵ㣬��Ϊ��2
            GL.Vertex3(Xmax, Ymax, 0);//�����������㣬��������Ͻǵ㣬��Ϊ��3
            GL.Vertex3(Xmax, Ymin, 0);//�������ĸ��㣬��������½ǵ㣬��Ϊ��4

            GL.End();//��һ���䣬��ʱ������һ���ޱ߿�ľ���


            /*------�ڶ��������ƾ��εı߿�------*/
            GL.Begin(GL.LINES);//��ʼ�����ߣ�����������εı߿�

            GL.Color(GLRectEdgeColor);//���÷���ı߿���ɫ����������Ϊ��͸����

            //���һ����
            GL.Vertex3(Xmin, Ymin, 0);//��ʼ�ڵ�1
            GL.Vertex3(Xmin, Ymax, 0);//��ֹ�ڵ�2

            //��ڶ�����
            GL.Vertex3(Xmin, Ymax, 0);//��ʼ�ڵ�2
            GL.Vertex3(Xmax, Ymax, 0);//��ֹ�ڵ�3

            //���������
            GL.Vertex3(Xmax, Ymax, 0);//��ʼ�ڵ�3
            GL.Vertex3(Xmax, Ymin, 0);//��ֹ�ڵ�4

            //���������
            GL.Vertex3(Xmax, Ymin, 0);//��ʼ�ڵ�4
            GL.Vertex3(Xmin, Ymin, 0);//���ص���1

            GL.End();//��������

            GL.PopMatrix();//GL��ջ
        }
    }

    private void ReleaseCapture()
    {
        foreach(var ett in m_selected_entities)
        {
            ett.mSelectable.ReleaseCapture();
        }
        m_selected_entities.Clear();
    }
    static private void DebugNullInstance()
    {
        Debug.Log("SelectSystem not been initialized!");
    }
    static public void SelectOne(Entity entity)
    {
        if (instance == null)
            DebugNullInstance();
        instance.ReleaseCapture();
        entity.mSelectable.Capture();
        instance.m_selected_entities.Add(entity);
    }
    
    static public void SelectEtts(List<Entity> entities)
    {
        if(instance == null)
            DebugNullInstance();
        instance.ReleaseCapture();
        foreach(var ett in entities)
        {
            ett.mSelectable.Capture();
            instance.m_selected_entities.Add(ett);
        }
    }

}

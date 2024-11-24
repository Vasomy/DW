using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    public Vector3 Origin=>transform.position;
    public float Radius=5.0f;
    public SpriteRenderer SR;
    private BuildingsInfo Info;
    bool IsOk = false;

    private void Start()
    {
    }

    private void Update()
    {
        if(IsOk == true)
        {
            if(Info.OccupyType == BuildingsOccupyType.Square)
                GridManager.RegisterPreviewOccupiedSquare(transform.position, Info.OccupyWidth, Info.OccupyHeight);
            else if(Info.OccupyType == BuildingsOccupyType.Circle)
            {

            }

        }
        
    }

    public void ResetData()
    {
        Radius = 0;
        SR.sprite = null;
        IsOk = false;
    }

    public void Init(string building_name,Sprite sp)
    {
        Info = GameRuntimeContext.instance.GetBuildingInfo(building_name);
        SR.sprite= sp;
        Radius= Info.Radius;
        IsOk = true;
    }
    private void OnDrawGizmos()
    {
        DrawCircle(transform.position,Radius,Color.red);
      
    }
    private void DrawCircle(Vector2 center, float Radius, Color color ,int VertexCount = 50)
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
        //Debug.Log(oldPos);
        for (i = 1; i < VertexCount + 1; i++)
        {
            Vector3 pos = new Vector3(Radius * Mathf.Cos(theta), Radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, (Vector3)center + pos);
            oldPos = transform.position + pos;

            theta += deltaTheta;
        }

    }

}
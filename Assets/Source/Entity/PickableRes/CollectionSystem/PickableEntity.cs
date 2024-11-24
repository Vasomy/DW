using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableEntity : Entity
{
    public float pickScore; // 完成采集所需的进度值
    public float pickRate; // 当前被采集的进度值
    // 必须有一个collieder
    public Collider2D collider;
    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        OnUpdate();
    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnInit()
    {
        mSelectable.Hook(this);
        entityType = EntityType.Static;
    }
    
    public virtual void FinishCollection()
        // 在子类被重写
        // 当采集值满时被调用
    {

    }

    public void DoCollect()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.tag != "Entity")
        {
            return;
        }
        var ett = collision.gameObject.GetComponent<Entity>();
        if ((((int)ett.entityType) & ((int)EntityType.Pick)) != 0)
        {
            Debug.Log("Farmer Get!");
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}

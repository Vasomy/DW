using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//Controllable units
public enum UnitsType
{


}

public class ControllableUnits : Entity
    // 玩家可控制的单位
    //子类在重写OnUpdate时必须调用base.OnUpdate()
    // 子类禁止调用 mono的所有反射方法如 Update(),Start(),Awake()....
    // 
{
    public Rigidbody2D rb2d;
    public float speed = 1.0f;
    public Vector2 ToPos;

    public UnitsType unitsType;
    private void Start()
    {
        OnInit();
    }
    public void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

        TickComponents();
        if(CompareFunction.is_same_vec2f(ToPos,transform.position))
        {
            rb2d.velocity = Vector2.zero;
        }
        if(mSelectable.isSelected)
        {
            if(Input.GetMouseButtonDown(1)) {
                //MoveTo(GameGlobalContext.GetMousePos2());
            }
        }
    }
    public virtual void OnInit()
    {
        mSelectable.Hook(this);
        ToPos = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        entityType = EntityType.Controlable;
    }

    public virtual void MoveTo(Vector2 to_pos)
    {
        ToPos = to_pos;
        Vector2 v_dir = to_pos - new Vector2(transform.position.x,transform.position.y);
        rb2d.velocity = v_dir.normalized * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StaticEntityType
{
    None,
    Buildings,
}
public class StaticEntity : Entity
{
    public StaticEntityType staticEntityType = StaticEntityType.None;

    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        OnUpdate();
    }
    public virtual void OnInit()
    {

    }
    public virtual void OnUpdate()
    {

    }
}

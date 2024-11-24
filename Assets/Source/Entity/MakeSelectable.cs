using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSelectable : MonoBehaviour
    // 
{
    public Entity HookedEntity;
    public bool isSelected = false;    
    public void Hook(Entity entity)
    {
        HookedEntity = entity;
        entity.gameObject.tag = "Entity";
    }
    public void Capture()
    {
        isSelected = true;
    }
    public void ReleaseCapture()
    {
        isSelected = false;
    }
    private void OnMouseDown()
    {
        SelectSystem.SelectOne(HookedEntity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : Buildings
{
    public override void LoadComponent()
    {
        base.LoadComponent();
        WallComponent wallC = new WallComponent();
        mStaticComponent.Add(wallC);

        foreach(var component in mStaticComponent)
        {
            component.Load(gameObject);
        }
    }
}

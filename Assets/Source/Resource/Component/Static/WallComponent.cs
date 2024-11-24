using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WallComponent : StaticComponent
{
    public WallComponent() 
        :base("static_wall_component","城墙")
    {
        
    }
    public override void Load(GameObject go)
    {
        base.Load(go);
        var bd = go.GetComponent<Buildings>();
        bd.Stats.GridFlag[(int)(GridFlags.Wall)]++;
    }
    public override void UnLoad(GameObject go)
    {
        base.UnLoad(go);
        var bd = go.GetComponent<Buildings>();
        bd.Stats.GridFlag[(int)(GridFlags.Wall)]--;

    }
}


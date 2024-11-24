using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class TickableComponent : BaseComponent
{
    public TickableComponent(string sign,string desc)
        : base(sign, desc)
    {

    }
    virtual public void OnEffect(GameObject bd)
        // 在所挂载的对象中每帧调用
    {

    }
}


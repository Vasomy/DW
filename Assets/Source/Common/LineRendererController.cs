using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController
//用于控制 线渲染器 渲染圆形 扇形
{
    public LineRenderer renderer;
    public LineRendererController(LineRenderer renderer)
    {
        this.renderer = renderer;
    }

    public void Reset()
    {
        renderer.positionCount = 0;
    }

    public void AsCircle(Transform transform, float radius)
    {
               //绘制圆形
        int a = 0;
        renderer.positionCount = 360;
        for (int i = -360 / 2; i< 360 / 2; i++)
        {
            renderer.SetPosition(a, Quaternion.Euler(0, i, 0) * transform.forward* radius + transform.position);
            a++;
        }
    }

}


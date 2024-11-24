using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController
//���ڿ��� ����Ⱦ�� ��ȾԲ�� ����
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
               //����Բ��
        int a = 0;
        renderer.positionCount = 360;
        for (int i = -360 / 2; i< 360 / 2; i++)
        {
            renderer.SetPosition(a, Quaternion.Euler(0, i, 0) * transform.forward* radius + transform.position);
            a++;
        }
    }

}


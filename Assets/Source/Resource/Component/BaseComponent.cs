using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class BaseComponent
{
    public string Signature;// ��������Ա�����õ�����
    public string DescriptionName;// ��ҿ���������
    public string EffectDescription;// Ч������
    public GameObject ParentGameObject {  get; private set; }//��������ص�gameobject
    public BaseComponent(string signature, string descriptionName)
    {
        Signature = signature;
        DescriptionName = descriptionName;
    }

    public virtual void Load(GameObject go)
        // �ڹҼ���ʵ����ʱ���õ��õĺ������ڸı������صĶ��������
    {
       ParentGameObject = go;
    }

    public virtual void UnLoad(GameObject bd)
        // �ڹҼ���ж��ʱ���ã����ڸı���������
    {

    }

}
                                                                                                                              
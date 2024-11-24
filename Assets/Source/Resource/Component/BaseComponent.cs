using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class BaseComponent
{
    public string Signature;// 供开发人员检测调用的名字
    public string DescriptionName;// 玩家看到的名字
    public string EffectDescription;// 效果描述
    public GameObject ParentGameObject {  get; private set; }//该组件负载的gameobject
    public BaseComponent(string signature, string descriptionName)
    {
        Signature = signature;
        DescriptionName = descriptionName;
    }

    public virtual void Load(GameObject go)
        // 在挂件被实例化时，该调用的函数用于改变所挂载的对象的属性
    {
       ParentGameObject = go;
    }

    public virtual void UnLoad(GameObject bd)
        // 在挂件被卸载时调用，用于改变对象的属性
    {

    }

}
                                                                                                                              
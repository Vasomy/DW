using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EntityType
{
    Undefined = 0,
    Controlable = 1,// 可以被控制 移动的 在select里面可以 调用其moveTo相关function
    Static = 1<<1,// 不可移动的或控制的，只能查看其属性值 如 可被采集的资源 树木，矿物等待
    Uncontrolable = 1<<2,// 不可以被控制的，如敌方单位
    Pick = (1<<3)|Controlable,
}
public class Entity : MonoBehaviour
    // 所有的初始化函数like start awake 应该在entity的一级子类中实现
    // 所有继承一级子类的具体entity实现不应该重写start和awake
{
    public SpriteRenderer m_sr;
    public List<StaticComponent> mStaticComponents = new List<StaticComponent>();
    public List<TickableComponent> mTickableComponents = new List<TickableComponent>();

    public MakeSelectable mSelectable;
    public EntityType entityType;
    protected void TickComponents()
    {
        foreach(var component in mTickableComponents) {
            component.OnEffect(gameObject);
        }
    }

}

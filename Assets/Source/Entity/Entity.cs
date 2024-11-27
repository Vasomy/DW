using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EntityType
{
    Undefined = 0,
    Controlable = 1,// ���Ա����� �ƶ��� ��select������� ������moveTo���function
    Static = 1<<1,// �����ƶ��Ļ���Ƶģ�ֻ�ܲ鿴������ֵ �� �ɱ��ɼ�����Դ ��ľ������ȴ�
    Uncontrolable = 1<<2,// �����Ա����Ƶģ���з���λ
    Pick = (1<<3)|Controlable,
}
public class Entity : MonoBehaviour
    // ���еĳ�ʼ������like start awake Ӧ����entity��һ��������ʵ��
    // ���м̳�һ������ľ���entityʵ�ֲ�Ӧ����дstart��awake
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

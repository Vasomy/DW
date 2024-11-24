using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class BuildingsStorge // 储存所有实例化的建筑，用于增删查改
{
    private Hashtable ActiveBuildings;//所有已实例化的建筑
    public BuildingsStorge() { 
        ActiveBuildings = new Hashtable();
    }
    public void Insert(int id, GameObject go)
    {

        var bd = go.GetComponent<Buildings>();
        ActiveBuildings.Add(id, bd);
    }

    public bool Contain(int id)
    {
        return ActiveBuildings.ContainsKey(id);
    }

    public void Remove(int id)
    {
        ActiveBuildings.Remove(id);
    }

    public Buildings Find(int id)
    {
        if (!Contain(id))
        {
            Debug.LogError("Find Error Building as "+id+" !");
            return null;
        }
        else
        {
            return (Buildings)ActiveBuildings[id];
        }
    }

}

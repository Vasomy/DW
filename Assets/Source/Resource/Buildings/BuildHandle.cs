using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHandle :MonoBehaviour
    
{
    public GameObject BuildingPrefab;
    public Transform GenerateTarget;
    
    public List<GameObject> BuildingsPrefabs = new List<GameObject>();
    public Dictionary<string, GameObject> mBuildingsPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach(var bp in BuildingsPrefabs)
        {
            string bdName = bp.GetComponent<Buildings>().Info.BuildingName;
            mBuildingsPrefabs.Add(bdName, bp);  
        }
    }

    public void Generate(string name)
    {
        if (name == null) return;
        if(name == "Delete")
        {
            
            if(GetOverlappingGameObject(out var target,out var id))
            {
                Debug.Log("Destory");
                DestroyBuilding(target,id);
            }
            return;
        }
        if (!GridManager.CanBuild()) return;
        var prefab = mBuildingsPrefabs[name];
        var go = Instantiate(prefab,GenerateTarget);
        go.name += name;
        Vector3 pos = GameRuntimeContext.instance.mBuildPreview.transform.position;
        pos = GridManager.TransPos(pos);
        go.transform.position = pos;
        var Building = go.GetComponentInChildren<Buildings>();
        var info = GameRuntimeContext.instance.GetBuildingInfo(name);
        Building.Init(info);

        GameRuntimeContext.instance.mBuildingsStorge.Insert(Building.Id, go);
    }

    public void DestroyBuilding(GameObject tar,int id)
    {
        var bd = tar.GetComponent<Buildings>();
        bd.UnRegisterOccupiedGrid();
        GameRuntimeContext.instance.mBuildingsStorge.Remove(id);
        Destroy(tar);
    }

    public bool GetOverlappingGameObject(out GameObject go,out int id)
    {

        var mouse_pos = GameGlobalContext.GetMousePos2();
        id = GridManager.GetId(mouse_pos);

        if(id !=-1)
        {
            var bd = GameRuntimeContext.instance.mBuildingsStorge.Find(id);
            go = bd.gameObject;
            return true;
        }
        go = null;
        return false;
    }
}

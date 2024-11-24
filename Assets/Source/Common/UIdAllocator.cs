using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIdAllocator : MonoBehaviour
{
    public static UIdAllocator Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        Instance = this; 
    }

    public int AllocateId()
    {
        int ret = next_id;
        next_id++;
        return ret;
    }

    private int next_id = 0;


}


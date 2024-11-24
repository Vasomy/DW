using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "New Image Data", menuName = "Data/New Image Data")]
public class SpriteItem:ScriptableObject
{
    public string Name => name;
    public Sprite Sprite;
}

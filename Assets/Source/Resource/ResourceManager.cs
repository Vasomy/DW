using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpriteManager : MonoBehaviour 
{
    public List<SpriteItem> sprites;
    private Dictionary<string, SpriteItem> mSprites;
    public static SpriteManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        mSprites = new Dictionary<string, SpriteItem>();
        foreach(var sp in sprites)
        {
            mSprites.Add(sp.name, sp);
        }
        sprites.Clear();
    }

    public SpriteItem GetSpriteItem(string name)
    {
        return mSprites[name];
    }
    public Sprite GetSprite(string name)
    {
        return mSprites[name].Sprite;
    }

}

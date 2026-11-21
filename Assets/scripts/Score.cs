using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score current;

    [SerializeField] Transform underUI;

    private List<SpriteRenderer> shroomCounterTxt = new List<SpriteRenderer>();
    private List<SpriteRenderer> treeCounterTxt = new List<SpriteRenderer>();
    private List<SpriteRenderer> sporeCounterTxt = new List<SpriteRenderer>();
    private List<SpriteRenderer> underSporeCounterTxt = new List<SpriteRenderer>();

    private int shroomCounter;
    private int treeCounter;
    private int sporeCounter;

    public Score()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateShroomCounterTxt();
        UpdateTreeCounterTxt();
        UpdateSporeCounterTxt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addShroom()
    {
        shroomCounter++;
        UpdateShroomCounterTxt();
    }

    private void UpdateShroomCounterTxt()
    {
        shroomCounterTxt.ForEach(f => Destroy(f.gameObject));
        shroomCounterTxt.Clear();
        shroomCounterTxt = Font.current.WriteLine(shroomCounter.ToString(), -2f + 0.375f, 0.75f + 0.125f, transform);
    }

    public void addTree()
    {
        treeCounter++;
        UpdateTreeCounterTxt();
    }

    private void UpdateTreeCounterTxt()
    {
        treeCounterTxt.ForEach(f => Destroy(f.gameObject));
        treeCounterTxt.Clear();
        treeCounterTxt = Font.current.WriteLine(treeCounter.ToString(), 1f - 0.5f, 0.75f + 0.125f, transform);
    }

    public void addSpore(int spore)
    {
        sporeCounter += spore;
        UpdateSporeCounterTxt();
    }

    private void UpdateSporeCounterTxt()
    {
        sporeCounterTxt.ForEach(f => Destroy(f.gameObject));
        sporeCounterTxt.Clear();
        underSporeCounterTxt.ForEach(f => Destroy(f.gameObject));
        underSporeCounterTxt.Clear();
        sporeCounterTxt = Font.current.WriteLine(sporeCounter.ToString().PadLeft(4, '0'), -2f + 0.375f, -0.5f + 0.25f, transform);
        underSporeCounterTxt = Font.current.WriteLine(sporeCounter.ToString().PadLeft(4, '0'), -0.5f, 3.25f, underUI);
        ShopItem[] shops = FindObjectsOfType<ShopItem>(true);
        foreach(ShopItem shop in shops)
        {
            shop.UpdateDisabled(sporeCounter);
        }
    }

    public int GetSpore()
    {
        return sporeCounter;
    }
}

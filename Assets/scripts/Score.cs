using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score current;

    private List<SpriteRenderer> shroomCounterTxt;
    private List<SpriteRenderer> treeCounterTxt;
    private List<SpriteRenderer> sporeCounterTxt;

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
        shroomCounterTxt = Font.current.WriteLine("000", -2f + 0.375f, 0.75f+0.125f, transform);
        treeCounterTxt = Font.current.WriteLine("000", 1f-0.5f, 0.75f+0.125f, transform);
        sporeCounterTxt = Font.current.WriteLine("0000", -2f+0.375f, -0.5f+0.25f, transform);

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
        sporeCounterTxt = Font.current.WriteLine(sporeCounter.ToString().PadLeft(4, '0'), -2f + 0.375f, -0.5f + 0.25f, transform);
    }
}

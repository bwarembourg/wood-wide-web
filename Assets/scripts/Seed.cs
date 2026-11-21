using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private Tree tree;
    private bool generating;
    private Vector2 dest;
    private Tile tile;

    private bool harvesting;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generating)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, Time.deltaTime * 20f);
            if (Vector2.Distance(transform.position, dest) < 0.1f)
            {
                transform.position = dest;
                generating = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (OnOffSwitch.current.mode == "outside" && !generating)
        {
            Destroy(gameObject);
            tree.InitConnected();
            tile.BuildGrowingTree();
        }
    }

    public void Init(Tree newTree)
    {
        tree = newTree;
        Tile destTile = Utils.current.GetRandomEmptyTiles(tree.transform.position, 6f);
        if (destTile == null)
        {
            Destroy(gameObject);
            tree.InitConnected();
            return;
        }
        tile = destTile;
        dest = destTile.transform.position;
        generating = true;
        if (OnOffSwitch.current.mode == "underground")
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
        }
    }
}

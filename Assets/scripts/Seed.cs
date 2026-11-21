using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private Tree tree;
    private bool generating;
    private Vector2 dest;

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
        if (OnOffSwitch.current.mode == "outside")
        {
            Destroy(gameObject);
            tree.InitConnected();
            // Plant Tree
        }
    }

    public void Init(Tree newTree)
    {
        tree = newTree;
        Tile tile = Utils.current.GetRandomEmptyTiles(tree.transform.position, 4f);
        if (tile == null)
        {
            Destroy(gameObject);
            tree.InitConnected();
            return;
        }
        dest = tile.transform.position;
        generating = true;
        if (OnOffSwitch.current.mode == "underground")
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
        }
    }
}

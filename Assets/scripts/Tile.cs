using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Animator path;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite mushroomIconSprite;
    [SerializeField] private Sprite treeIconSprite;
    [SerializeField] private Animator tree;
    [SerializeField] private Animator mushroom;
    [SerializeField] private ProgressBar progressBar;

    public float x;
    public float y;


    public bool conRight;
    public bool conLeft;
    public bool conTop;
    public bool conBottom;
    public bool isMushroom;
    public bool isTree;
    public bool isSeed;

    public bool isConnected;

    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateIsConnected()
    {
        PathState ps = IsConnected(new PathState(false, false, false));
        isConnected = ps.connected;
        path.SetBool("on", ps.connected);
        if (isTree && isConnected)
        {
            tree.GetComponent<Tree>().InitConnected();
            if (OnOffSwitch.current.mode == "underground")
            {
                SpriteRenderer treeSr = tree.GetComponent<SpriteRenderer>();
                treeSr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
                progressBar.UpdateUnderground();
            }
        }
    }

    private void OnMouseDown()
    {
        BuildPath();
    }

    public void BuildPath()
    {
        Debug.Log("build path");
        NewPath selected = UIUnderground.current.GetSelected();
        if (selected != null)
        {
            selected.RemoveCounter();
            if (selected.type == "mushroom")
            {
                path.SetTrigger("cross");
            } else
            {
                path.SetTrigger(selected.type);
            }
            path.GetComponent<Transform>().rotation = selected.GetRotation();
            conRight = selected.conRight;
            conLeft = selected.conLeft;
            conTop = selected.conTop;
            conBottom = selected.conBottom;
            GameMgt.current.UpdateAllTiles();
            UIUnderground.current.Reset();
            if (selected.type == "mushroom")
            {
                BuildMushRoom();
            }
        } 
    }

    public void BuildMushRoom()
    {
        isMushroom = true;
        icon.sprite = mushroomIconSprite;
        icon.gameObject.SetActive(true);
        BuildCross();
        mushroom.gameObject.SetActive(true);
        mushroom.GetComponent<Mushroom>().Init();
        Score.current.addShroom();
        if (OnOffSwitch.current.mode == "underground")
        {
            SpriteRenderer treeSr = tree.GetComponent<SpriteRenderer>();
            treeSr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
            progressBar.UpdateUnderground();
        }
    }

    public void BuildTree()
    {
        isTree = true;
        icon.sprite = treeIconSprite;
        icon.gameObject.SetActive(true);
        BuildCross();
        tree.gameObject.SetActive(true);
        Score.current.addTree();
        if (OnOffSwitch.current.mode == "underground")
        {
            SpriteRenderer shroomSr = mushroom.GetComponent<SpriteRenderer>();
            shroomSr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
            progressBar.UpdateUnderground();
        }
    }

    private void BuildCross()
    {
        conRight = true;
        conLeft = true;
        conTop = true;
        conBottom = true;
        path.SetTrigger("cross");
        GameMgt.current.UpdateAllTiles();
    }

    public PathState IsConnected(PathState pathState)
    {
        PathState newPathState = pathState;
        if (isMushroom)
        {
            return new PathState(true, true, true);
        }
        List<Tile> tiles = GetTilesAround();
        tiles = tiles.FindAll(t => newPathState.tilesDone.Find(td => td == t) == null);
        if (conRight)
        {
            Tile rightTile = tiles.Find(tile => transform.position.x < tile.transform.position.x);
            if (rightTile != null && rightTile.conLeft)
            {
                newPathState.tilesDone.Add(this);
                newPathState = rightTile.IsConnected(newPathState);
            }
        }
        if (conLeft)
        {
            Tile leftTile = tiles.Find(tile => transform.position.x > tile.transform.position.x);
            if (leftTile != null && leftTile.conRight)
            {
                newPathState.tilesDone.Add(this);
                newPathState = leftTile.IsConnected(newPathState);
            }
        }
        if (conTop)
        {
            Tile topTile = tiles.Find(tile => transform.position.y < tile.transform.position.y);
            if (topTile != null && topTile.conBottom)
            {
                newPathState.tilesDone.Add(this);
                newPathState = topTile.IsConnected(newPathState);
            }
        }
        if (conBottom)
        {
            Tile bottomTile = tiles.Find(tile => transform.position.y > tile.transform.position.y);
            if (bottomTile != null && bottomTile.conTop)
            {
                newPathState.tilesDone.Add(this);
                newPathState = bottomTile.IsConnected(newPathState);
            }
        }
        return newPathState;
    }

    public List<Tile> GetTilesAround()
    {
        List<Tile> tilesAround = new List<Tile>();
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach (Tile tile in tiles)
        {
            if (Vector2.Distance(transform.position, tile.transform.position) <= 1f && tile != this)
            {
                if (tile.conBottom || tile.conTop || tile.conRight || tile.conLeft)
                    tilesAround.Add(tile);
            }
        }
        return tilesAround;
    }

}

public class PathState
{
    public List<Tile> tilesDone = new List<Tile>();
    public bool hasTree;
    public bool hasMushroom;
    public bool connected;

    public PathState(bool hasTree, bool hasMushroom, bool connected)
    {
        this.hasTree = hasTree;
        this.hasMushroom = hasMushroom;
        this.connected = connected;
        this.tilesDone = new List<Tile>();
    }
}

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
    [SerializeField] private Animator hover;
    [SerializeField] private GameObject hoverIcon;

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
    public bool isTuto;

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

    public void UpdateMushroomConnected()
    {
        UpdateConnected(new PathState(false, false, false));

    }

    public void UpdateIsConnected()
    {
        path.SetBool("on", isConnected);
        GameMgt.current.syncAnims();
        if (isTree && isConnected)
        {
            tree.SetBool("dying", false);
            if (!tree.GetComponent<Tree>().growing)
            {
                tree.GetComponent<Tree>().InitConnected();
            }
            if (OnOffSwitch.current.mode == "underground")
            {
                SpriteRenderer treeSr = tree.GetComponent<SpriteRenderer>();
                treeSr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
                progressBar.UpdateUnderground();
            }
        }
        else if (isTree && !isConnected && !tree.GetComponent<Tree>().growing)
        {
            tree.SetBool("dying", true);
            tree.GetComponent<Tree>().InitDying();
        }
    }

    private void OnMouseDown()
    {
        if (isMushroom || isTree)
            return;
        BuildPath();
    }

    private void OnMouseOver()
    {
        NewPath selected = UIUnderground.current.GetSelected();
        if (selected != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                // CLIC DROIT
                selected.Rotate();
                hover.GetComponent<Transform>().rotation = selected.GetRotation();
                return;
            }
        }
        if (isMushroom || isTree)
            return;
        if (selected != null)
        {
            hover.gameObject.SetActive(true);
            if (selected.type == "mushroom")
            {
                hover.SetTrigger("cross");
                hoverIcon.SetActive(true);
            }
            else
            {
                hover.SetTrigger(selected.type);
            }
            hover.GetComponent<Transform>().rotation = selected.GetRotation();
        }
    }


    private void OnMouseExit()
    {
        hover.gameObject.SetActive(false);
        hoverIcon.SetActive(false);
    }

    public void BuildPath()
    {
        if (!GameMgt.current.gameOn() && Tuto.current.tutoId != 6)
            return;
        NewPath selected = UIUnderground.current.GetSelected();
        if (selected != null && ((Tuto.current.tutoId == 6 && selected.type == "line" && selected.conRight && selected.conLeft && isTuto) || GameMgt.current.gameOn()))
        {
            selected.RemoveCounter();
            if (selected.type == "mushroom")
            {
                SFX.current.Play("BUILD_MUSHROOM");
                path.SetTrigger("cross");
            } else
            {
                SFX.current.Play("BUILD_PATH");
                path.SetTrigger(selected.type);
            }
            path.GetComponent<Transform>().rotation = selected.GetRotation();
            conRight = selected.conRight;
            conLeft = selected.conLeft;
            conTop = selected.conTop;
            conBottom = selected.conBottom;
            if (selected.counter <= 0)
                UIUnderground.current.Reset();
            if (selected.type == "mushroom")
            {
                BuildMushRoom();
            }
            GameMgt.current.UpdateAllTiles();
            if (Tuto.current.tutoId == 6)
            {
                Tuto.current.EndTuto6();
            }
        } 
    }

    public void DeletePath()
    {
        path.SetTrigger("reset");
        conRight = false;
        conLeft = false;
        conTop = false;
        conBottom = false;
        isConnected = false;
    }

    public void DeleteMushroom()
    {
        DeletePath();
        mushroom.GetComponent<Mushroom>().RemoveSpore();
        icon.gameObject.SetActive(false);
        mushroom.GetComponent<Mushroom>().Delete();
        mushroom.gameObject.SetActive(false);
        Score.current.removeShroom();
        GameMgt.current.UpdateAllTiles();
        isMushroom = false;
        isConnected = false;
    }

    public void DeleteTree()
    {
        DeletePath();
        tree.GetComponent<Tree>().RemoveSeed();
        icon.gameObject.SetActive(false);
        tree.gameObject.SetActive(false);
        Score.current.removeTree();
        GameMgt.current.UpdateAllTiles();
        isTree = false;
        isConnected = false;
    }

    public void BuildMushRoom()
    {
        isMushroom = true;
        icon.sprite = mushroomIconSprite;
        icon.gameObject.SetActive(true);
        BuildCross();
        mushroom.gameObject.SetActive(true);
        mushroom.GetComponent<Mushroom>().Init(this);
        Score.current.addShroom();
        if (OnOffSwitch.current.mode == "underground")
        {
            SpriteRenderer mushSr = mushroom.GetComponent<SpriteRenderer>();
            mushSr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
            progressBar.UpdateUnderground();
        }
        path.SetTrigger("cross");
    }

    public void BuildTree(bool isFirst = false)
    {
        tree.GetComponent<Tree>().isFirst = isFirst;
        isTree = true;
        if (isFirst)
            tree.SetBool("dying", false);
        icon.sprite = treeIconSprite;
        icon.gameObject.SetActive(true);
        tree.gameObject.SetActive(true);
        Score.current.addTree();
        BuildCross();
        if (OnOffSwitch.current.mode == "underground")
        {
            SpriteRenderer shroomSr = mushroom.GetComponent<SpriteRenderer>();
            shroomSr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
            progressBar.UpdateUnderground();
        }
        path.SetTrigger("cross");
        tree.SetTrigger("grown");
        if (isConnected)
        {
            tree.SetBool("dying", false);
            tree.GetComponent<Tree>().InitConnected();
        } else
        {
            if (!isFirst)
            {
                tree.SetBool("dying", true);
                tree.GetComponent<Tree>().InitDying();
            } else
            {
                tree.SetBool("dying", false);
            }
        }
    }

    public void BuildGrowingTree()
    {
        isTree = true;
        icon.sprite = treeIconSprite;
        icon.gameObject.SetActive(true);
        tree.GetComponent<Tree>().InitGrowing();
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

    PathState UpdateConnected(PathState pathState)
    {
        if (isMushroom)
        {
            isConnected = true;
        }
        PathState newPathState = pathState;
        List<Tile> tiles = GetTilesAround();
        tiles = tiles.FindAll(t => newPathState.tilesDone.Find(td => td == t) == null);
        if (conRight)
        {
            Tile rightTile = tiles.Find(tile => transform.position.x < tile.transform.position.x);
            if (rightTile != null && rightTile.conLeft)
            {
                rightTile.isConnected = true;
                newPathState.tilesDone.Add(this);
                newPathState = rightTile.UpdateConnected(newPathState);
            }
        }
        if (conLeft)
        {
            Tile leftTile = tiles.Find(tile => transform.position.x > tile.transform.position.x);
            if (leftTile != null && leftTile.conRight)
            {
                leftTile.isConnected = true;
                newPathState.tilesDone.Add(this);
                newPathState = leftTile.UpdateConnected(newPathState);
            }
        }
        if (conTop)
        {
            Tile topTile = tiles.Find(tile => transform.position.y < tile.transform.position.y);
            if (topTile != null && topTile.conBottom)
            {
                topTile.isConnected = true;
                newPathState.tilesDone.Add(this);
                newPathState = topTile.UpdateConnected(newPathState);
            }
        }
        if (conBottom)
        {
            Tile bottomTile = tiles.Find(tile => transform.position.y > tile.transform.position.y);
            if (bottomTile != null && bottomTile.conTop)
            {
                bottomTile.isConnected = true;
                newPathState.tilesDone.Add(this);
                newPathState = bottomTile.UpdateConnected(newPathState);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgt : MonoBehaviour
{
    public static GameMgt current;

    public GameMgt()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Utils.current.getAllTiles().ForEach(tile =>
        {
            if (Vector2.Distance(tile.transform.position, new Vector2(3f, 4f)) < 1f)
            {
                tile.BuildTree();
            }
            if (Vector2.Distance(tile.transform.position, new Vector2(1f, 3f)) < 0.75f)
            {
                tile.BuildMushRoom();
            }
        });
        UpdateAllTiles();
    }

    public void UpdateAllTiles()
    {
        Debug.Log("Update alive tiles");
        List<Tile> aliveTiles = Utils.current.GetAllAliveTiles();
        Debug.Log(aliveTiles.Count + " tiles alives");
        aliveTiles.ForEach(tile => tile.UpdateIsConnected());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

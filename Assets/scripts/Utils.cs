using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils current;

    public Utils()
    {
        current = this;
    }

    public List<Tile> getAllTiles()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        return tiles.ToList();
    }

    public List<Tile> GetAllAliveTiles()
    {
        return getAllTiles().FindAll(t => t.conBottom || t.conLeft || t.conRight || t.conTop);
    }

    public List<SpriteRenderer> GetAllOutsideSprites()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("outside");
        GameObject[] goTiles = GameObject.FindGameObjectsWithTag("outsidetile");
        List<GameObject> gosL = gos.ToList();
        List<GameObject> goTilesL = goTiles.ToList();
        gosL.AddRange(goTilesL);

        return gosL
            .FindAll(g => g.GetComponent<SpriteRenderer>() != null)
            .Select(g => g.GetComponent<SpriteRenderer>()).ToList();
    }

    public List<SpriteRenderer> GetAllUndergroundSprites()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("underground");
        GameObject[] goTiles = GameObject.FindGameObjectsWithTag("undergroundtile");
        List<GameObject> gosL = gos.ToList();
        List<GameObject> goTilesL = goTiles.ToList();

        gosL.AddRange(goTilesL);
        return gosL
            .FindAll(g => g.GetComponent<SpriteRenderer>() != null)
            .Select(g => g.GetComponent<SpriteRenderer>()).ToList();
    }
}

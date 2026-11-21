using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMgt : MonoBehaviour
{
    public static GameMgt current;

    [SerializeField] Tile mushroomStartTile;
    [SerializeField] Tile treeStartTile;

    public GameMgt()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        mushroomStartTile.BuildMushRoom();
        treeStartTile.BuildTree();
        UpdateAllTiles();
    }

    public void UpdateAllTiles()
    {
        List<Tile> aliveTiles = Utils.current.GetAllAliveTiles();
        aliveTiles.ForEach(tile => tile.UpdateIsConnected());
        GameObject[] gos = GameObject.FindGameObjectsWithTag("underground");
        float time = gos[1].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
        List<GameObject> gosFiltered = gos.ToList()
            .FindAll(g => g.GetComponent<Animator>() != null);
        gosFiltered
            .ForEach(g => {
                AnimatorStateInfo currentState = g.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                g.GetComponent<Animator>().Play(currentState.fullPathHash, 0, 0f);
                g.GetComponent<Animator>().Update(time);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

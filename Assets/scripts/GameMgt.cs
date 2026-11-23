using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMgt : MonoBehaviour
{
    public static GameMgt current;

    [SerializeField] Tile mushroomStartTile;
    [SerializeField] Tile treeStartTile;
    [SerializeField] Groingroin groingroinPrefab;
    [SerializeField] ProgressBar dayProgressBar;
    [SerializeField] float[] maxTimerEat;
    public float maxTimerDay;

    private float timerDay;
    private float timerEat;
    public bool isGameOver;
    

    public GameMgt()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        mushroomStartTile.BuildMushRoom();
        treeStartTile.BuildTree(true);
        UpdateAllTiles();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIUnderground.current.Reset();
        }
        if (!gameOn())
            return;
        timerDay += Time.deltaTime;
        dayProgressBar.UpdateProgressBar(timerDay, maxTimerDay);
        if (timerDay >= maxTimerDay)
        {
            timerDay = 0;
            Score.current.addDay();
        }
        timerEat += Time.deltaTime;
        int idMaxTimer = Score.current.GetDay();
        if (maxTimerEat.Length - 1 < idMaxTimer)
        {
            idMaxTimer = maxTimerEat.Length - 1;
        }
        if (timerEat >= maxTimerEat[idMaxTimer])
        {
            timerEat = 0;
            EatMushroom();
        }
    }

    public void ResetTimerEat()
    {
        timerDay = 0;
    }

    public void UpdateAllTiles()
    {
        List<Tile> aliveTiles = Utils.current.GetAllAliveTiles();
        aliveTiles.ForEach(tile => tile.UpdateIsConnected());
        syncAnims();
    }

    public void EatMushroom()
    {
        Mushroom[] mushrooms = FindObjectsOfType<Mushroom>();
        List<Mushroom> mushroomList = mushrooms.ToList();
        Mushroom mushroom = mushroomList[Random.Range(0, mushroomList.Count)];
        if (mushroom != null)
        {
            Groingroin groingroin = Instantiate(groingroinPrefab, new Vector2(-2.5f, mushroom.transform.position.y - 2f/16f), Quaternion.identity, transform);
            groingroin.Init(mushroom);
        }
    }

    public void syncAnims()
    {
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

    public bool gameOn()
    {
        return !Tuto.current.isTuto && !GameOver.current.on;
    }
}


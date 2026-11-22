using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] ProgressBar progressBar;
    [SerializeField] ProgressBar dyingProgressBar;
    [SerializeField] float maxSeed;
    [SerializeField] float maxGrow;
    [SerializeField] float maxDie;
    [SerializeField] Seed seedPrefab;
    [SerializeField] Tile tile;
    [SerializeField] SpriteRenderer heart;

    private bool working;
    private float timerGrowing;
    private float timerWorking;
    private float timerDying;
    public bool growing;
    public bool hasSeed;
    private bool dying;
    private Seed seed = null;

    public bool isFirst;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(working && !growing && !dying)
        {
            timerWorking += Time.deltaTime;
            progressBar.UpdateProgressBar(timerWorking, maxSeed);
            if (timerWorking >= maxSeed)
            {
                timerWorking = 0;
                progressBar.gameObject.SetActive(false);
                working = false;
                hasSeed = true;
                seed = Instantiate(seedPrefab, transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
                seed.Init(this);
            }
        }
        else if (growing)
        {
            timerGrowing += Time.deltaTime;
            progressBar.UpdateProgressBar(timerGrowing, maxGrow);
            if (timerGrowing >= maxGrow)
            {
                timerGrowing = 0;
                progressBar.gameObject.SetActive(false);
                growing = false;
                GetComponent<Animator>().SetTrigger("grown");
                tile.UpdateIsConnected();
                if (tile.isConnected)
                {
                    InitConnected();
                }
            }
        }
        else if (dying)
        {
            timerDying += Time.deltaTime;
            dyingProgressBar.UpdateProgressBar(timerDying, maxDie);
            if (timerDying >= maxDie)
            {
                timerDying = 0;
                dyingProgressBar.gameObject.SetActive(false);
                dying = false;
                tile.DeleteTree();
            }
        }
    }

    public void InitGrowing()
    {
        growing = true;
        dying = false;
        working = false;
        dyingProgressBar.gameObject.SetActive(false);
        progressBar.transform.localPosition = new Vector3(0, 0.5f, 0);
        progressBar.gameObject.SetActive(true);
        timerDying = 0;
        timerGrowing = 0;
        timerWorking = 0;
    }

    public void InitConnected()
    {
        timerDying = 0;
        timerGrowing = 0;
        if (!growing && !hasSeed)
        {
            isFirst = false;
            growing = false;
            dying = false;
            working = true;
            dyingProgressBar.gameObject.SetActive(false);
            progressBar.transform.localPosition = new Vector3(0, 0.5625f + 0.75f, 0);
            progressBar.gameObject.SetActive(true);
        }
    }

    public void InitDying()
    {
        if (dying)
            return;
        if (isFirst)
            return;
        if (!growing)
        {
            Life.current.LoseLife();
        }
        timerDying = 0;
        timerGrowing = 0;
        timerWorking = 0;
        growing = false;
        working = false;
        dying = true;
        dyingProgressBar.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(false);
    }

    public void RemoveSeed()
    {
        if (seed != null && seed.gameObject != null)
        {
            Destroy(seed.gameObject);
        }
    }
}

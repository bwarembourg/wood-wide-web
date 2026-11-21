using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] ProgressBar progressBar;
    [SerializeField] float maxSeed;
    [SerializeField] float maxGrow;
    [SerializeField] Seed seedPrefab;
    [SerializeField] Tile tile;

    private bool working;
    private float timer;
    private bool growing;
    private Seed seed = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(working && !growing)
        {
            timer += Time.deltaTime;
            progressBar.UpdateProgressBar(timer, maxSeed);
            if (timer >= maxSeed)
            {
                timer = 0;
                progressBar.gameObject.SetActive(false);
                working = false;
                seed = Instantiate(seedPrefab, transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
                seed.Init(this);
            }
        }
        if (growing)
        {
            timer += Time.deltaTime;
            progressBar.UpdateProgressBar(timer, maxGrow);
            if (timer >= maxGrow)
            {
                timer = 0;
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
    }

    public void InitGrowing()
    {
        growing = true;
        progressBar.transform.localPosition = new Vector3(0, 0.5f, 0);
        progressBar.gameObject.SetActive(true);
    }

    public void InitConnected()
    {
        if (growing || seed != null)
        {
            return;
        }
        working = true;
        progressBar.transform.localPosition = new Vector3(0, 0.5625f + 0.75f, 0);
        progressBar.gameObject.SetActive(true);
    }
}

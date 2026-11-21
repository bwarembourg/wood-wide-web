using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] ProgressBar progressBar;
    [SerializeField] float max;
    [SerializeField] Seed seedPrefab;

    private bool working;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(working)
        {
            timer += Time.deltaTime;
            progressBar.UpdateProgressBar(timer, max);
            if (timer >= max)
            {
                timer = 0;
                progressBar.gameObject.SetActive(false);
                working = false;
                Seed seed = Instantiate(seedPrefab, transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
                seed.Init(this);
            }
        }
    }

    public void InitConnected()
    {
        working = true;
        progressBar.transform.localPosition = new Vector3(0, 0.5625f + 0.75f, 0);
        progressBar.gameObject.SetActive(true);
    }
}

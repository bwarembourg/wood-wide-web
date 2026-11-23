using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] ProgressBar progressBar;
    [SerializeField] float max;
    [SerializeField] Spore sporePrefab;
    [SerializeField] Puff puff;

    private bool working;
    private float timer;
    public Tile tile;
    private Spore spore;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (working)
        {
            timer += Time.deltaTime;
            progressBar.UpdateProgressBar(timer, max);
            if (timer >= max)
            {
                timer = 0;
                progressBar.gameObject.SetActive(false);
                working = false;
                spore = Instantiate(sporePrefab, transform.position - new Vector3(0, 0.25f, 0), Quaternion.identity, transform);
                spore.Init(this);
            }
        }
    }

    public void Init(Tile newTile)
    {
        tile = newTile;
        working = true;
        progressBar.gameObject.SetActive(true);
        if (!GameMgt.current.gameOn() && Tuto.current.tutoId == 0)
        {
            timer = 0;
            progressBar.gameObject.SetActive(false);
            working = false;
            spore = Instantiate(sporePrefab, transform.position - new Vector3(0, 0.25f, 0), Quaternion.identity, transform);
            spore.Init(this);
        }
    }

    public void Eat()
    {
        tile.DeleteMushroom();
    }

    public void Delete()
    {
        Instantiate(puff, transform.position, Quaternion.identity, GameMgt.current.transform);
        working = false;
        progressBar.gameObject.SetActive(false);
        timer = 0;
    }

    public void RemoveSpore()
    {
        if (spore != null && spore.gameObject != null)
        {
            Destroy(spore.gameObject);
        }
    }
}

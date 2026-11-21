using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour
{
    [SerializeField] int score;

    private Mushroom mushroom;
    private bool generating;
    private Vector2 dest;

    private bool harvesting;
    private

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generating)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, Time.deltaTime * 4f);
            if (Vector2.Distance(transform.position, dest) < 0.1f)
            {
                transform.position = dest;
                generating = false;
            }
        }
        if (harvesting)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, Time.deltaTime * 20f);
            if (Vector2.Distance(transform.position, dest) < 0.1f)
            {
                transform.position = dest;
                harvesting = false;
                Score.current.addSpore(score);
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        if (OnOffSwitch.current.mode == "outside")
        {
            GameObject go = GameObject.FindGameObjectWithTag("sporedest");
            dest = go.transform.position;
            generating = false;
            harvesting = true;
            mushroom.Init();
        }
    }

    public void Init(Mushroom newMushroom)
    {
        mushroom = newMushroom;
        generating = true;
        dest = mushroom.transform.position + new Vector3(0, 0.25f, 0);
        if (OnOffSwitch.current.mode == "underground")
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
        }
    }
}

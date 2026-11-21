using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIcon : MonoBehaviour
{
    [SerializeField] Sprite line;
    [SerializeField] Sprite turn;
    [SerializeField] Sprite cross;
    [SerializeField] Sprite shroom;
    [SerializeField] SpriteRenderer sr;

    private bool moving;
    private Vector2 dest;
    private GameObject goDest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, Time.deltaTime * 8f);
            if (Vector2.Distance(transform.position, dest) < 0.1f)
            {
                moving = false;
                Destroy(gameObject);
                goDest.GetComponent<NewPath>().AddCounter();
            }
        }
    }

    public void Init(string type)
    {
        goDest = GameObject.FindGameObjectWithTag("iconline");
        if (type == "path")
        {
            int rand = Random.Range(0, 5);
            if (rand < 2)
            {
                sr.sprite = line;
                goDest = GameObject.FindGameObjectWithTag("iconline");
            } else if (rand < 4)
            {
                sr.sprite = turn;
                goDest = GameObject.FindGameObjectWithTag("iconturn");
            } else
            {
                sr.sprite = cross;
                goDest = GameObject.FindGameObjectWithTag("iconcross");
            }
        }
        else
        {
            sr.sprite = shroom;
            goDest = GameObject.FindGameObjectWithTag("iconshroom");
        }
        dest = goDest.transform.position;
        moving = true;
    }
}

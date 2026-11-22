using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groingroin : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] anims;

    private Mushroom mushroom;
    private bool going;
    private bool eating;
    private bool goingBack;

    private Animator animator;
    private SpriteRenderer sr;
    private Vector3 initialPos;
    private Vector3 dest;

    private float timer = 0;
    private float max = 3f;


    private void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (going)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * 4f);
            if (Vector3.Distance(transform.position, dest) < 0.1f)
            {
                transform.position = dest;
                animator.SetTrigger("idle");
                going = false;
                eating = true;
            }
        }
        if (eating)
        {
            timer += Time.deltaTime;
            if (timer >= max)
            {
                mushroom.Eat();
                sr.flipX = true;
                animator.SetTrigger("run");
                eating = false;
                goingBack = true;
            }
        }
        if (goingBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, Time.deltaTime * 4f);
            if (Vector3.Distance(transform.position, initialPos) < 0.1f)
            {
                transform.position = dest;
                Destroy(gameObject);
            }

        }
    }

    public void Init(Mushroom newMushroom)
    {
        GetComponent<Animator>().runtimeAnimatorController = anims[Random.Range(0, anims.Length)];
        mushroom = newMushroom;
        initialPos = transform.position;
        dest = mushroom.transform.position - new Vector3(0.25f + 2f/16f, 0);
        going = true;
    }
}

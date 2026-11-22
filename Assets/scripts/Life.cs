using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public static Life current;
    [SerializeField] SpriteRenderer heart1;
    [SerializeField] SpriteRenderer heart2;
    [SerializeField] SpriteRenderer heart3;
    [SerializeField] Sprite empty;
    [SerializeField] Sprite full;

    public int life = 3;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLife();
    }

    public void LoseLife()
    {
        life--;
        UpdateLife();
        if (life <= 0)
        {
            GameOver.current.Init();
        }
    }

    public void UpdateLife()
    {
        heart3.sprite = life >= 3 ? full : empty;
        heart2.sprite = life >= 2 ? full : empty;
        heart1.sprite = life >= 1 ? full : empty;
    }
}

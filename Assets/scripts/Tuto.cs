using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    [SerializeField] private GameObject keyEnter;

    public static Tuto current;
    bool isTuto = false;
    int tutoId = 999;
    List<SpriteRenderer> dialogTxt = new List<SpriteRenderer>();

    float timer = 0;
    float maxTimerChar = 0.05f;
    int id = 0;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LaunchTuto();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTuto)
        {
            if (tutoId == 0)
            {
                timer += Time.deltaTime;
                if (timer >= maxTimerChar && id < dialogTxt.Count)
                {
                    dialogTxt[id].gameObject.SetActive(true);
                    id++;
                    timer = 0;
                }
                if (id >= dialogTxt.Count - 1)
                {
                    keyEnter.SetActive(true);

                }
                if ((id >= dialogTxt.Count - 1) && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
                {
                    keyEnter.SetActive(false);
                    id = 0;
                    clearDialog();
                    Write("Your work, if you accept it, is to make a forest by spreading mushroom and trees. See this spore ?");
                    tutoId = 1;
                }
            }
            else if (tutoId == 1)
            {
                timer += Time.deltaTime;
                if (timer >= maxTimerChar && id < dialogTxt.Count)
                {
                    dialogTxt[id].gameObject.SetActive(true);
                    id++;
                    timer = 0;
                }
                if (id >= dialogTxt.Count - 1)
                {
                    keyEnter.SetActive(true);

                }
                if ((id >= dialogTxt.Count - 1) && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
                {
                    keyEnter.SetActive(false);
                    id = 0;
                    clearDialog();
                    Write("Click on it to catch it!");
                    tutoId = 2;
                }
            }
            else if (tutoId == 2)
            {
                timer += Time.deltaTime;
                if (timer >= maxTimerChar && id < dialogTxt.Count)
                {
                    dialogTxt[id].gameObject.SetActive(true);
                    id++;
                    timer = 0;
                }
                if (id >= dialogTxt.Count - 1)
                {
                    keyEnter.SetActive(true);

                }
                if ((id >= dialogTxt.Count - 1) && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
                {
                    keyEnter.SetActive(false);
                    id = 0;
                    clearDialog();
                    tutoId = 4;
                }
            }
        }
    }

    public void LaunchTuto()
    {

        isTuto = true;
        tutoId = 0;
        Write("Hey! Welcome to the world of Trees and Shrooms!");
    }

    private void Write(string txt)
    {
        dialogTxt = Font.current.Write(txt, 7.5f, -4.75f, transform, 1010, false, false, 21f);
    }

    private void clearDialog()
    {
        dialogTxt.ForEach(d => Destroy(d.gameObject));
        dialogTxt.Clear();
    }

    public void HideTuto()
    {
        isTuto = false;
        gameObject.SetActive(false);
    }


}

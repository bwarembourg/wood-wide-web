using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    [SerializeField] private GameObject keyEnter;
    [SerializeField] GameObject arrow;
    [SerializeField] Tile targetTile;
    [SerializeField] public Tile targetTreeTile;
    [SerializeField] bool resetTuto;

    public static Tuto current;
    public bool isTuto = false;
    public int tutoId = 999;
    List<SpriteRenderer> dialogTxt = new List<SpriteRenderer>();

    float timer = 0;
    float maxTimerChar = 0.02f;
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
                    arrow.SetActive(true);
                    arrow.transform.position = FindObjectOfType<Spore>().transform.position + new Vector3(0, 0.5f, 0);
                }
            }
            else if (tutoId >= 2 && tutoId <= 6)
            {
                timer += Time.deltaTime;
                if (timer >= maxTimerChar && id < dialogTxt.Count)
                {
                    dialogTxt[id].gameObject.SetActive(true);
                    id++;
                    timer = 0;
                }
            }
            else if (tutoId == 7)
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
                    Write("You can also get mushrooms. Getting more mushroom will help securing the forest.");
                    tutoId = 8;
                    arrow.SetActive(true);
                    arrow.transform.position = FindObjectsOfType<ShopItem>().ToList().Find(s => s.type == "mushroom").transform.position + new Vector3(0.75f, 0.5f, 0);
                }
            }
            else if (tutoId == 8)
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
                    Write("Sometimes, animals eat mushrooms.");
                    tutoId = 9;
                    arrow.SetActive(false);
                }
            }
            else if (tutoId == 9)
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
                    Write("If you have no more mushroom, your trees will be tired and you will lose health.");
                    tutoId = 10;
                    arrow.SetActive(true);
                    arrow.GetComponent<SpriteRenderer>().flipY = true;
                    arrow.transform.position = FindObjectOfType<Life>().transform.position + new Vector3(1f, -1.25f, 0);
                }
            }
            else if (tutoId == 10)
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
                    Write("Now let's go back outside by clicking space or on the upper button.");
                    tutoId = 11;
                    arrow.SetActive(true);
                    arrow.GetComponent<SpriteRenderer>().flipY = true;
                    arrow.transform.position = FindObjectOfType<OnOffSwitch>().transform.position + new Vector3(3f, -1.25f, 0);
                }
            }
            else if (tutoId == 11)
            {
                timer += Time.deltaTime;
                if (timer >= maxTimerChar && id < dialogTxt.Count)
                {
                    dialogTxt[id].gameObject.SetActive(true);
                    id++;
                    FindObjectOfType<Tree>().maxSeed = 5f;
                    timer = 0;
                }
            }
            else if (tutoId == 12)
            {
                timer += Time.deltaTime;
                if (timer >= maxTimerChar && id < dialogTxt.Count)
                {
                    dialogTxt[id].gameObject.SetActive(true);
                    id++;
                    timer = 0;
                }
            }
            else if (tutoId == 13)
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
                    Write("If this future tree is not connected with a mushroom, it will be tired and you will lose health.");
                    tutoId = 14;
                    arrow.SetActive(false);
                }
            }
            else if (tutoId == 14)
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
                    Write("Now go! And make a glorious forest!");
                    tutoId = 15;
                    arrow.SetActive(false);                }
            }
            else if (tutoId == 15)
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
                    tutoId = 16;
                    PlayerPrefs.SetInt("tuto", 1);
                    HideTuto();
                }
            }
        }
    }

    public void EndTuto2()
    {
        keyEnter.SetActive(false);
        timer = 0;
        id = 0;
        clearDialog();
        Write("Good job! Now let's go underground. Click on this button or press Space.");
        arrow.GetComponent<SpriteRenderer>().flipY = true;
        arrow.transform.position = FindObjectOfType<OnOffSwitch>().transform.position + new Vector3(3f, -1.25f, 0);
        tutoId = 3;
    }

    public void EndTuto3()
    {
        keyEnter.SetActive(false);
        timer = 0;
        id = 0;
        clearDialog();
        Write("Nice! Now click on this button to get paths. Do it until we don't have anymore spore left!");
        arrow.GetComponent<SpriteRenderer>().flipY = false;
        ShopItem shop = FindObjectsOfType<ShopItem>(true).ToList().Find(shop => shop.type == "path");
        arrow.transform.position = shop.transform.position + new Vector3(0.75f, 0.5f, 0);
        tutoId = 4;
    }

    public void EndTuto4()
    {
        keyEnter.SetActive(false);
        timer = 0;
        id = 0;
        clearDialog();
        Write("Great! Now click on this path.");
        NewPath np = FindObjectsOfType<NewPath>(true).ToList().Find(n => n.type == "line");
        arrow.transform.position = np.transform.position + new Vector3(0, 0.75f, 0);
        tutoId = 5;
    }

    public void EndTuto5()
    {
        keyEnter.SetActive(false);
        timer = 0;
        id = 0;
        clearDialog();
        targetTile.isTuto = true;
        Write("Rotate it by right clicking on it, then place it there to connect the mushroom and the tree.");
        arrow.transform.position = targetTile.transform.position + new Vector3(0, 0.75f, 0);
        tutoId = 6;
    }

    public void EndTuto6()
    {
        keyEnter.SetActive(false);
        timer = 0;
        id = 0;
        clearDialog();
        Write("Yeah!! Connected!");
        arrow.SetActive(false);
        tutoId = 7;
    }

    public void EndTuto11()
    {
        keyEnter.SetActive(false);
        timer = 0;
        id = 0;
        clearDialog();
        Write("Alright ! Our tree is starting to generate a seed, let's wait for it...");
        arrow.SetActive(false);
        tutoId = 12;
    }

    public void EndTuto12()
    {
        FindObjectOfType<Tree>().maxSeed = 45f;
        keyEnter.SetActive(false);
        timer = 0;
        id = 0;
        clearDialog();
        Write("Here! the seed! You can click on it to grow a tree! But, be careful...");
        arrow.SetActive(false);
        tutoId = 13;
    }

    public void LaunchTuto()
    {
        if (resetTuto)
        {
            PlayerPrefs.SetInt("tuto", 0);
        }
        if (PlayerPrefs.GetInt("tuto", 0) == 0)
        {
            isTuto = true;
            tutoId = 0;
            Write("Hey! Welcome to the world of Trees and Shrooms!");
        } else
        {
            tutoId = 16;
            HideTuto();
        }
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

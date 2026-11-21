using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : MonoBehaviour
{
    public static OnOffSwitch current;
    [SerializeField] private GameObject UIOutside;
    [SerializeField] private GameObject UIUnderground;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private SpriteRenderer panel;
    [SerializeField] private Sprite iconOut;
    [SerializeField] private Sprite iconUnder;
    [SerializeField] private Sprite panelOut;
    [SerializeField] private Sprite panelUnder;
    [SerializeField] private GameObject hover;

    public string mode = "outside";
    private string oldMode = "underground";
    private bool transitioning = false;
    private List<SpriteRenderer> srs = new List<SpriteRenderer>();
    public float targetAlpha = 0.2f;
    private float speed = 2f;
    private List<SpriteRenderer> label;

    public OnOffSwitch() { current = this; }

    // Start is called before the first frame update
    void Start()
    {
        label = Font.current.WriteLine("OUTSIDE", 1f, -0.25f, transform);
        label.ForEach(l => l.color = new Color(0,0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Transition();
        }
        if (transitioning)
        {
            srs = srs.FindAll(s => s != null && s.gameObject != null);
            srs.ForEach(s => s.color = new Color(1f, 1f, 1f, s.color.a + Time.deltaTime * speed));
            if ((srs[0].color.a <= targetAlpha && mode == "underground") || (srs[0].color.a >= targetAlpha && mode == "outside"))
            {
                srs.ForEach(s => s.color = new Color(1f, 1f, 1f, targetAlpha));
                transitioning = false;
                srs
                    .FindAll(sr => sr.gameObject.CompareTag(oldMode + "tile"))
                    .ForEach(s => s.color = new Color(1f, 1f, 1f, 0));
                if (mode == "underground")
                {
                    UIOutside.SetActive(false);
                    UIUnderground.SetActive(true);
                    SetLabel("UNDERGROUND");
                    icon.sprite = iconUnder;
                    panel.sprite = panelUnder;
                    Utils.current.getAllTiles().ForEach(t => t.GetComponent<BoxCollider2D>().enabled = true);
                } else
                {
                    UIOutside.SetActive(true);
                    UIUnderground.SetActive(false);
                    SetLabel("OUTSIDE");
                    icon.sprite = iconOut;
                    panel.sprite = panelOut;
                    Utils.current.getAllTiles().ForEach(t => t.GetComponent<BoxCollider2D>().enabled = false);
                }
            }
        }
    }

    private void Transition()
    {
        if (!transitioning)
        {
            transitioning = true;
            if (mode == "outside")
            {
                SwitchToUnderground();
            }
            else
            {
                SwitchToOutside();
            }
        }
    }

    private void OnMouseDown()
    {
        Transition();
    }

    private void OnMouseEnter()
    {
        hover.SetActive(true);
    }

    private void OnMouseExit()
    {
        hover.SetActive(false);
    }

    private void SwitchToOutside()
    {
        speed = 2f;
        targetAlpha = 1f;
        mode = "outside";
        oldMode = "underground";
        srs = Utils.current.GetAllOutsideSprites();
    }

    private void SwitchToUnderground()
    {
        speed = -2f;
        targetAlpha = 0.4f;
        mode = "underground";
        oldMode = "outside";
        srs = Utils.current.GetAllOutsideSprites();
    }

    private void SetLabel(string txt)
    {
        label.ForEach(l => Destroy(l.gameObject));
        label.Clear();
        label = Font.current.WriteLine(txt, 1f, -0.25f, transform);
        label.ForEach(l => l.color = new Color(0, 0, 0));
    }

}

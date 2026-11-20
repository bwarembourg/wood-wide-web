using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : MonoBehaviour
{
    [SerializeField] private GameObject UIOutside;
    [SerializeField] private GameObject UIUnderground;

    private string mode = "outside";
    private string oldMode = "underground";
    private bool transitioning = false;
    private List<SpriteRenderer> srs = new List<SpriteRenderer>();
    private float targetAlpha = 0.2f;
    private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!transitioning && Input.GetKeyDown(KeyCode.Space))
        {
            transitioning = true;
            if (mode == "outside")
            {
                SwitchToUnderground();
            } else
            {
                SwitchToOutside();
            }
        }
        if (transitioning)
        {
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
                } else
                {
                    UIOutside.SetActive(true);
                    UIUnderground.SetActive(false);
                }
            }
        }
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite[] progress;
    [SerializeField] Color lowColor;
    [SerializeField] Color midColor;
    [SerializeField] Color highColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProgressBar(float value, float max)
    {
        float percent = value / max;
        int id = Mathf.RoundToInt(percent * 10);
        sr.sprite = progress[id];
        if (percent < 0.25f)
        {
            sr.color = lowColor;
        } else if (percent > 0.75f)
        {
            sr.color = highColor;
        } else
        {
            sr.color = midColor;
        }
    }

    public void UpdateUnderground()
    {
        sr.color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, OnOffSwitch.current.targetAlpha);
    }

}

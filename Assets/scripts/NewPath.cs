using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPath : MonoBehaviour
{
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    [SerializeField] public string type;
    [SerializeField] private Transform path;
    [SerializeField] private GameObject disabled;

    public bool conRight;
    public bool conLeft;
    public bool conTop;
    public bool conBottom;
    public int counter;
    private List<SpriteRenderer> counterTxt = new List<SpriteRenderer>();
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCounter();
        sr = GetComponent<SpriteRenderer>();
        if (type == "turn")
        {
            conLeft = true;
            conBottom = true;
        }
        if (type == "cross")
        {
            conLeft = true;
            conRight = true;
            conBottom = true;
            conTop = true;
        }
        if (type == "line")
        {
            conTop = true;
            conBottom = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (counter == 0)
            return;
        // CLIC GAUCHE
        if (UIUnderground.current.hasSelected())
        {
            UIUnderground.current.Reset();
        }
        OnOff(true);
        UIUnderground.current.SetSelected(this);
    }

    public void Rotate()
    {
        path.Rotate(0, 0, 90f);
        UpdateConnexions();
        if (UIUnderground.current.hasSelected())
        {
            UIUnderground.current.SetSelected(this);
        }
    }

    private void OnMouseOver()
    {
        if (counter == 0)
            return;
        if (Input.GetMouseButtonDown(1))
        {
            // CLIC DROIT
            Rotate();
            return;
        }
        OnOff(true);
    }

    public Quaternion GetRotation()
    {
        return path.rotation;
    }

    private void UpdateConnexions()
    {
        if (type == "line")
        {
            conRight = !conRight;
            conLeft = !conLeft;
            conTop = !conTop;
            conBottom = !conBottom;
        }
        if (type == "turn")
        {
            bool oldConRight = conRight;
            bool oldConLeft = conLeft;
            bool oldConTop = conTop;
            bool oldConBottom = conBottom;
            conRight = oldConBottom;
            conTop = oldConRight;
            conLeft = oldConTop;
            conBottom = oldConLeft;
        }
    }

    private void OnMouseExit()
    {
        if (counter == 0)
            return;
        if (UIUnderground.current.hasSelected())
            return;
        OnOff(false);
    }

    public void OnOff(bool onoff)
    {
        sr.sprite = onoff ? on : off;
    }

    public void Reset()
    {
        OnOff(false);
    }

    public void AddCounter()
    {
        counter++;
        UpdateCounter();
    }

    public void RemoveCounter()
    {
        counter--;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        disabled.SetActive(counter <= 0);
        counterTxt.ForEach(sr => Destroy(sr.gameObject));
        counterTxt.Clear();
        counterTxt = Font.current.WriteLine("x" + counter, 1f-0.25f-0.125f, 0.25f, transform);
    }
}

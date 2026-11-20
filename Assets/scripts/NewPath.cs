using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPath : MonoBehaviour
{
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    [SerializeField] public string type;
    [SerializeField] private Transform path;

    public bool conRight;
    public bool conLeft;
    public bool conTop;
    public bool conBottom;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
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
        // CLIC GAUCHE
        if (UIUnderground.current.hasSelected())
            return;
        OnOff(true);
        UIUnderground.current.SetSelected(this);
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // CLIC DROIT
            path.Rotate(0, 0, 90f);
            UpdateConnexions();
            return;
        }

        if (UIUnderground.current.hasSelected())
            return;
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
}

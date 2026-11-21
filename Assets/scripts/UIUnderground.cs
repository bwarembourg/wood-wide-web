using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnderground : MonoBehaviour
{
    public static UIUnderground current;

    public UIUnderground() { current = this; }

    private NewPath selectedPath = null;

    private void Start()
    {
        Font.current.WriteLine("BUY :", -2.5f+1f/16f, 2.5f, transform);
    }

    public void SetSelected(NewPath np)
    {
        selectedPath = np;
    }

    public bool hasSelected()
    {
        return selectedPath != null;
    }

    public NewPath GetSelected()
    {
        return selectedPath;
    }

    public void Reset()
    {
        selectedPath = null;
        NewPath[] nps = FindObjectsOfType<NewPath>();
        foreach(NewPath np in nps)
        {
            np.Reset();
        }
    }
}

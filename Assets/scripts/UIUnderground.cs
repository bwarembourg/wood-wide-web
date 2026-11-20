using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnderground : MonoBehaviour
{
    public static UIUnderground current;

    public UIUnderground() { current = this; }

    private NewPath selectedPath = null;

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

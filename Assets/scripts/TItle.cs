using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TItle : MonoBehaviour
{
    [SerializeField] private Blinker blinker;

    // Start is called before the first frame update
    void Start()
    {
        Font.current.Write("Press Enter to start...", -30f, -3f, blinker.transform, 2, true, false, 30f, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}

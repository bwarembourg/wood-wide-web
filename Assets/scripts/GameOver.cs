using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Blinker blinker;

    public static GameOver current;

    public bool on;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        
    }


    public void Init(string msg)
    {
        on = true;
        ui.SetActive(true);
        Font.current.WriteLine(msg, 0, 1f, ui.transform, 1000, true, true, 25f, "UI");
        Font.current.WriteLine("DAY " + Score.current.GetDay(), 11.75f, -0.25f, ui.transform, 1001, true, false, 0, "UI");
        Font.current.WriteLine(Score.current.getTree().ToString(), 12.5f, -1.8125f, ui.transform, 1001, true, false, 0, "UI");
        Font.current.WriteLine(Score.current.getShroom().ToString(), 8.5f, -1.8125f, ui.transform, 1001, true, false, 0, "UI");
        Font.current.WriteLine(Score.current.GetSpore().ToString(), 16.5f, -1.8125f, ui.transform, 1001, true, false, 0, "UI");
        Font.current.WriteLine("Press Enter to restart...", 0, -5f, blinker.transform, 1000, true, true, 25f, "UI");
    }

    private void Update()
    {
        if (on && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))) {
            SceneManager.LoadScene("main");
        }
    }
}

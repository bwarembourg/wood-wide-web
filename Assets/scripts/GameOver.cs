using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject ui;

    public static GameOver current;

    public bool on;

    private void Awake()
    {
        current = this;
    }


    public void Init()
    {
        on = true;
        ui.SetActive(true);
    }

    private void Update()
    {
        if (on && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))) {
            Debug.Log("RESET");
            SceneManager.LoadScene("main");
        }
    }
}

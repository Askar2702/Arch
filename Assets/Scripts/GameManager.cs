using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PanelStart;
    [SerializeField]
    private GameObject PanelEnD;
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        PanelStart.SetActive(false);
    }
    public void PanelEnd()
    {
        PanelEnD.SetActive(true);
    }
    public void Restart()
   {
        SceneManager.LoadScene("Game");
   }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public MenuControl[] menuControls;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        for (int i = 1; i < menuControls.Length; i++)
        {
            menuControls[i].isCompleted = PlayerPrefs.GetInt("LevelComplete_" + i) == 1;
            if(i>0)
            {
                menuControls[i].btn.GetComponent<Button>().interactable=menuControls[i-1].isCompleted;
            }
        }
    }
    public void ChangeScene(int idGame)
    {
        GameManager.idMap=idGame;
        SceneManager.LoadScene(1);
    }
}
[System.Serializable]
public class MenuControl
{
    public bool isCompleted;
    public string title;
    public GameObject btn;
    
}

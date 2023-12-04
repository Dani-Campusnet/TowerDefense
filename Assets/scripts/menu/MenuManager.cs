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
        for (int i = 1; i < menuControls.Length; i++)
        {
            if (menuControls[i - 1].isCompleted)
            {
                menuControls[i].btn.GetComponent<Button>().interactable = true;
            }
            else
            {
                menuControls[i].btn.GetComponent<Button>().interactable = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

static public class PlayerValues 
{
    static public int lifes;
    
    static public void InitLifes(int idMap)
    {
        switch (idMap) { 
            case 0: lifes = 3; coins = 150; break;
            case 1: lifes = 2; coins = 150; break;
            case 2: lifes = 3; coins = 150; break;
        }
    }
    static public void RemoveLifes(int damage=1)
    {
        lifes -= damage;
        if (lifes<=0)
        {
            SceneManager.LoadScene(2);
        }
    }
    
    
    static public int coins;
    static public void RemoveCoins(int money)
    {
        coins -= money;
    }
    static public void AddCoins(int money)
    {
        coins += money;
    }

}

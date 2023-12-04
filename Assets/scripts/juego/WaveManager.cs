using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Wave",menuName ="Wave")]
public class WaveManager : ScriptableObject
{
    public List<Wave> waves;
    public List<Wave> CloneWave()
    {
        List<Wave> result= new List<Wave>();
        for(int i = 0; i < waves.Count; i++)
        {
            Wave wave = new Wave();
            List<Wave.Enemies> enemies=new List<Wave.Enemies>();
            for (int j = 0; j < waves[i].enemies.Count; j++){
                Wave.Enemies getEnemy= new Wave.Enemies();
                getEnemy.enemy = waves[i].enemies[j].enemy;
                getEnemy.time= waves[i].enemies[j].time;
                enemies.Add(getEnemy);
            } 
            wave.enemies = enemies;
            result.Add(wave);
        }
        return result;
    }
}

[System.Serializable]
public class Wave
{
    public List<Enemies> enemies;

    [System.Serializable]
    public class Enemies
    {
        public GameObject enemy;
        public float time;
    }
}

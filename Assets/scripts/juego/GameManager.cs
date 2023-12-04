using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     TextAsset map;
    static public int idMap;
    public Transform mapPosition;

    List<List<int>> allMap;
    List<Vector2> corners;
    public List<Wave> waves;
    float waveTime;
    int currentWave, currentEnemy;
    GameObject currentTurret;
    public List <int> walkable;
    public List<TurretManager> allTurrets;
    public TextMeshProUGUI coinsText;
    public Image clockImage;
    TurretManager turretEvolve;

    // Start is called before the first frame update
    void Start()
    {
        
        map = Resources.Load<TextAsset>($"Map{idMap}");
        allMap = new List<List<int>>();
        string[] row = map.text.Split('\n');
        for (int i = 0; i < row.Length; i++)
        {
            List<int> ids = new List<int>();
            string[] column=row[i].Split(",");
            for (int j = 0; j < column.Length-1; j++)
            {
                ids.Add(int.Parse(column[j]));
                Sprite tile = Resources.Load<Sprite>("map/" + column[j]);
                GameObject newTile = new GameObject($"{i}-{j}");
                newTile.transform.SetParent(mapPosition);
                newTile.transform.localPosition = new Vector2(j,-i);
                newTile.AddComponent<SpriteRenderer>().sprite=tile;
            }
            allMap.Add(ids);
        }
        TextAsset corn = Resources.Load<TextAsset>($"corners{idMap}");
        corners = new List<Vector2>();
        string[] cornRow = corn.text.Split("\n");
        
         for (int i = 0;i < cornRow.Length; i++)
            {
            string[] cornColumn= cornRow[i].Split(",");
            corners.Add(new Vector2(float.Parse(cornColumn[0]), -float.Parse(cornColumn[1])));
            }

        waves = Resources.Load<WaveManager>($"Waves/Wave{idMap}").CloneWave();
        waveTime = 0;currentWave = 0;currentEnemy = 0;
        PlayerValues.InitLifes(idMap);
        PrintCoins();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave< waves.Count)
        {
            if(Input.GetMouseButton(0) && turretEvolve!=null)
            {
                clockImage.fillAmount = Mathf.MoveTowards(clockImage.fillAmount, 1,1*Time.deltaTime);
                Vector3 clockpos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clockpos.z=clockImage.transform.position.z;
                clockImage.transform.position=clockpos;

                if (clockImage.fillAmount == 1)
                {
                    SetTurretEvolve();
                }
            }
            if (Input.GetMouseButtonUp(0) && turretEvolve!= null)
            {
                turretEvolve= null;
                clockImage.fillAmount=0;
            }
            if (currentEnemy < waves[currentWave].enemies.Count) { 
                waveTime += Time.deltaTime;
                if (waveTime >= waves[currentWave].enemies[currentEnemy].time)
                {
                waveTime = 0;
                GameObject newEnemy = Instantiate(waves[currentWave].enemies[currentEnemy].enemy);
                waves[currentWave].enemies[currentEnemy].enemy = newEnemy;
                newEnemy.transform.SetParent(mapPosition);
                newEnemy.GetComponent<EnemyManager>().Init(corners, this);
                currentEnemy++;
                } 
            }
            else
            {
                if (EnemyInScene() == 0)
                {
                    currentWave++;
                    waveTime = 0;
                    currentEnemy = 0;
                }
            }
        }
        else { print("He ganado"); }

        if (currentTurret!=null)
        {
            if(Input.GetMouseButton(0))
            {
                Vector2 turretPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                turretPosition = GetPositionInGrid(turretPosition);
                currentTurret.transform.position = turretPosition;
            }
            else if (Input.GetMouseButtonUp(0)) {
                TurretManager temp = currentTurret.GetComponent<TurretManager>();

                if (PlayerValues.coins < temp.cost)
                {
                    Destroy(currentTurret);
                    return;
                }

                int tile = allMap[-(int)currentTurret.transform.localPosition.y][(int)currentTurret.transform.localPosition.x];
                if(walkable.Contains(tile)==false)
                {
                    Destroy(currentTurret);
                    return;
                }
                for (int i = 0; i < allTurrets.Count; i++) {
                    if (allTurrets[i].transform.position== currentTurret.transform.position)
                    {
                        Destroy(currentTurret);
                        return;
                    }
                }

                PlayerValues.RemoveCoins(temp.cost);
                PrintCoins();
                temp.InitTurret(this);
                allTurrets.Add(temp);
                currentTurret=null;
            }
        }
        Vector2 GetPositionInGrid(Vector2 position)
        {
            return new Vector2(Mathf.Round(position.x),Mathf.Round(position.y));
        }
    }
    public int EnemyInScene()
    {
        int countEnemies = 0;
        for (int i = 0; i < waves[currentWave].enemies.Count; i++) {
            if (waves[currentWave].enemies[i].enemy!=null)
            {
                countEnemies++;
            }
        }
        return countEnemies;
    }
    public void DestroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
        PrintCoins();
    }
    public void CreateTurret(GameObject turret)
    {
        
        currentTurret=Instantiate(turret, mapPosition);
    }

    public void GetTurretEvolve(TurretManager turret)
    {
        turretEvolve = turret;
    }
    public void SetTurretEvolve()
    {
        GameObject newEvolve= Instantiate(turretEvolve.evolve, mapPosition);
        newEvolve.transform.localPosition = turretEvolve.transform.localPosition;
        newEvolve.GetComponent<TurretManager>().InitTurret(this);
        PlayerValues.RemoveCoins(newEvolve.GetComponent<TurretManager>().cost);
        PrintCoins();
        Destroy(turretEvolve.gameObject);
        clockImage.fillAmount = 0;
    }
    void PrintCoins()
    {
        coinsText.text = $"Coins : {PlayerValues.coins}";
    }
}

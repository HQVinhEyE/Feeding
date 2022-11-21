using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int Pool = 10;
    public List<GameObject> EnemiesList = new List<GameObject>();
    [SerializeField] GameObject[] enemy;
    private float delay = 0.1f;

    #region halfsingleton

    public static EnemySpawner Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Pool; i++)
        {
            GameObject enemytolist = SpawnEnemy();
            enemytolist.SetActive(false);
            EnemiesList.Add(enemytolist);
        }
        InvokeRepeating("SpawnFromList", 0, delay);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public GameObject SpawnEnemy()
    {
        int choice = Random.Range(0, 15);
        int enemychoice;
        if (choice <= 4) { enemychoice = 0; }
        else if (choice <= 8) { enemychoice = 1; }
        else if (choice <= 11) { enemychoice = 2; }
        else if (choice < 13) enemychoice = 3;
        else enemychoice = 4;

        GameObject en = Instantiate(enemy[enemychoice]);
        return en;
    }
    //private GameObject EnemyinList()
    //{
    //    for (int i = 0; i < Pool; i++)
    //    {
    //        if (EnemiesList[i] != null)
    //        {
    //            return EnemiesList[i];
    //        }
    //        else return null;
    //    }
    //}
    private void SpawnFromList()
    {
        if (EnemiesList.Count > 0)
        {
            int i = Random.Range(0, EnemiesList.Count);
            float randX = Random.Range(-50f, 50f);
            float randY = 10f;
            float randZ = Random.Range(-50f, 50f);
            Vector3 landingspot = new(randX, randY, randZ);
            EnemiesList[i].SetActive(true);
            EnemiesList[i].transform.position = landingspot;
            EnemiesList[i].transform.rotation = Quaternion.identity;
            EnemiesList.RemoveAt(i);
        }
    }
}

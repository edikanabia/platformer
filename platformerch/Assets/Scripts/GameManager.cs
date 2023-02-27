using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int candies = 0;
    static int nextScene = 0;
    bool canProceed = false;
    public bool playerIsDead = false;
    public int spawnerNumber = 0;

    public GameObject[] spawners;
    public GameObject player;
    public CameraController cameraControl;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            SpawnPlayer();
            
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            cameraControl.TargetPlayer();
        }
    }

    public void SpawnPlayer()
    {
        Instantiate(player, spawners[spawnerNumber].transform.position, Quaternion.identity);
    }

    public void CandyGet()
    {
        candies++;
    }
}

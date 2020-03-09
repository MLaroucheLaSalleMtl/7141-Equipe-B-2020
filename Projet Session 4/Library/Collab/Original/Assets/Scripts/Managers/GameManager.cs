using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player _Player;
    public static int NumberOfEnemy = 0;
    public bool victory = false;
    public static bool gameOver = false;
    ProceduralGenerationManager _PGM;
    UI_Manager _UIM;
    void Awake()
    {
        _PGM = GameObject.Find("ProceduralGeneratorManager").GetComponent<ProceduralGenerationManager>();
        _UIM = GameObject.Find("UIManager").GetComponent<UI_Manager>();
        _Player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Start()
    {
        _PGM.InitializeGame();   
    }

    // Update is called once per frame
    void Update()
    {
        if(_Player.HealthCurrent == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        NumberOfEnemy = 0;
        _UIM.PanelToggle(8);
    }

}

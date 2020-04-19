using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Player _Player;
    public static int NumberOfEnemy = 0;
    [Header("Altar")]
    [SerializeField] private GameObject altarPortal = null;
    public static int NumberOfActiveAltar = 0;
    private int NumberOfAltar = 3;
    public static bool victory = false;
    public static bool gameOver = false;
    private bool endGame = false;
    ProceduralGenerationManager _PGM;
    UI_Manager _UIM;
    private AudioManager audioManager = null;
    [SerializeField] private AudioClip newMusic = null;
    void Awake()
    {
        _PGM = GameObject.Find("ProceduralGeneratorManager").GetComponent<ProceduralGenerationManager>();
        _UIM = GameObject.Find("UIManager").GetComponent<UI_Manager>();
        _Player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Start()
    {
        _PGM.GenerateTheDungeon();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Player.Health.CurrentIsEmpty() && !gameOver)
        {
            GameOver();
            gameOver = true;
        }
        if (victory && !endGame)
        {
            Victory();
            endGame = true;
        }

        if (NumberOfActiveAltar == NumberOfAltar)
        SummonPortal(altarPortal);
    }
    public static void GameInitialization()
    {
        gameOver = false;
        victory = false;
        NumberOfEnemy = 0;
        NumberOfActiveAltar = 0;
    }
    public void GameOver()
    {
        NumberOfEnemy = 0;
        _UIM.PanelToggle(3);
    }

    public void Victory()
    {
        NumberOfEnemy = 0;
        audioManager.VictoryMusic(newMusic);
        _UIM.PanelToggle(4);

    }

    public void SummonPortal(GameObject portal)
    {
        if (portal.gameObject.activeSelf == true) return;
        if(portal != null)
        {
            portal.SetActive(true);
        }
    }
}

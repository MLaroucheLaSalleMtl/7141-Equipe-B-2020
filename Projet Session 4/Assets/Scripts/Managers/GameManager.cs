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
    private int NumberOfAltar = 1;
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
        _PGM.GenerateTheDungeon();   
    }

    // Update is called once per frame
    void Update()
    {
        if (_Player.Health.CurrentIsEmpty())
        {
            GameOver();
        }
        if(NumberOfActiveAltar == NumberOfAltar)
        SummonPortal(altarPortal);
    }

    public void GameOver()
    {
        NumberOfEnemy = 0;
        _UIM.PanelToggle(8);
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

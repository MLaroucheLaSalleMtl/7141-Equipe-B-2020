using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ProceduralGenerationManager _PGM;
    void Awake()
    {
        _PGM = GameObject.Find("ProceduralGeneratorManager").GetComponent<ProceduralGenerationManager>();
    }
    void Start()
    {
        _PGM.InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

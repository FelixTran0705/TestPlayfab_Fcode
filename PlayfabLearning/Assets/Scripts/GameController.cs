using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayfabManager playfabManager;
    public Button BtnAddScore;
    public Button BtnShowLeaderboard;
    private int _score;
    // Start is called before the first frame update
    void Start()
    {
        BtnAddScore.onClick.AddListener(IncreaseScore);
        BtnShowLeaderboard.onClick.AddListener(playfabManager.GetLeaderboard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IncreaseScore()
    {
        _score += 1;
       playfabManager.SendLeaderboard(_score);
    }

    


}

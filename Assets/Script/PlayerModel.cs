using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Player,
    AI
};

public class PlayerModel
{
    string name = "";
    int money = 0;
    int score = 0;
    int stayFieldId = 0;
    PlayerType playerType;

    public PlayerModel(string name, int money, int score, PlayerType playerType)
    {
        this.name = name;
        this.money = money;
        this.score = score;
        this.playerType = playerType;
    }

    public PlayerType GetPlayerType
    {
          get { return playerType; } 
    }

    public string GetMoneyAsString
    {
        get
        {
            return this.money.ToString();
        }
    }

    public int GetMoney
    {
        get
        {
            return this.money;
        }
    }

    public string GetName
    {
        get
        {
            return this.name;
        }
    }
    
    public int GetFieldID
    {
        get
        {
            return stayFieldId;
        }
    }

    public void AddScore(int score)
    {
        this.score = score;
    }
    /*
     * Add money for Player
     * */
    public void AddMoney(int money)
    {
        this.money += money;
    }
    /*
     * Set Field Id for Play Chip
     * */
    public void SetFieldPosition(int id)
    {
        this.stayFieldId = id;
        /*if(this.playerType == PlayerType.Player)
            Debug.Log(id);*/
    }
}

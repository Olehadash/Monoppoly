using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    AnimationController animationController;
    [SerializeField]
    Bonuses bonus;

    InfoPanel panel;
    Dice dice;
    PlayerModel player_model, ai_model;
    PlayerView player_view, ai_view;
    PopupViewer popup;
    List<GameFieldViewer> fields;

    PlayerType turn = PlayerType.Player;

    int diceValue = 0;
    /*
     * Set Parametrs From FieldBulder
     * */
    public void SetParametrs(InfoPanel panel, Dice dice, PlayerModel player_model, PlayerModel ai_model, PlayerView player_view, PlayerView ai_view, PopupViewer popup, List<GameFieldViewer> fields)
    {
        this.panel = panel;
        this.dice = dice;
        this.player_model = player_model;
        this.ai_model = ai_model;
        this.player_view = player_view;
        this.ai_view = ai_view;
        this.popup = popup;
        this.fields = fields;
    }

    public void StartGame()
    {
        popup.Show(PopupType.Start, ShowDiePopup);
    }

    void ShowDiePopup()
    {
        popup.Show(PopupType.Die, GetPlayerDie);
        
    }

    void GetPlayerDie()
    {
        dice.SetDiceDelegate(GetAnamyDie);
        dice.ActivateDice(true);
    }

    void GetAnamyDie(int value)
    {
        Debug.Log("Player Value = " + value.ToString());
        dice.ActivateDice(false);
        diceValue = value;
        dice.SetDiceDelegate(GetDieAndCompare);
        dice.Drop(true);
    }

    void GetDieAndCompare(int value)
    {
        Debug.Log("Anemy Value = " + value.ToString());

        dice.SetDiceDelegate(StartSteps);
        if (diceValue > value)
        {
            turn = PlayerType.Player;
            dice.ActivateDice(true);
        }
        else
        {
            turn = PlayerType.AI;
            dice.ActivateDice(false);
            dice.Drop(true);
        }
        
        panel.SetTurn(turn);
    }

    PlayerView GetPlayerView
    {
        get
        {
            return turn == PlayerType.Player ? player_view : ai_view;
        }
    }

    void StartSteps(int value)
    {
        animationController.SetAfterAnimationDelegate(EndStep);
        animationController.SeverallSteps(GetPlayerView, fields ,value);
    }

    void EndStep()
    {
        DetectFieldTypeAmdAction(fields[GetPlayerView.GetModel.GetFieldID]);
    }

    void DetectFieldTypeAmdAction(GameFieldViewer field)
    {
        
        switch(field.GetFieldType)
        {
            case FieldType.Free:
                Debug.Log("Free Field");
                if(turn == PlayerType.Player)
                {
                    popup.Show(PopupType.Buy, GetBuyField, field.GetFieldModel.cost);
                }
                else
                {
                    GetBuyField(true);
                }
                break;
            case FieldType.Bonus:
                Debug.Log("Bonus Field");
                int bon = bonus.ShowBonusAnimation(field.GetPosition);
                GetPlayerView.GetModel.AddMoney(bon);
                panel.SetPlayerInfo(GetPlayerView.GetModel);
                SwitchTurn();
                break;
            case FieldType.Player1:
                Debug.Log("Player1 Field");
                if(turn == PlayerType.Player)
                {
                    field.PayTax(player_model, ai_model, panel);
                    int tax = field.GetFieldModel.tax;
                    animationController.TextAnimation(field.GetPosition, "-" + tax.ToString());
                    animationController.TextAnimation(ai_view.GetPosition, "+" + tax.ToString());
                }
                SwitchTurn();
                break;
            case FieldType.Player2:
                Debug.Log("Player2 Field");
                if (turn == PlayerType.AI)
                {
                    field.PayTax(ai_model, player_model, panel);
                    int tax = field.GetFieldModel.tax;
                    animationController.TextAnimation(field.GetPosition, "-" + tax.ToString());
                    animationController.TextAnimation(player_view.GetPosition, "+" + tax.ToString());
                }
                SwitchTurn();
                break;
            case FieldType.Start:
                ShowStartFieldGetBonus();
                SwitchTurn();
                break;
        }

        
    }

    public void ShowStartFieldGetBonus()
    {
        GetPlayerView.GetModel.AddMoney(200);
        panel.SetPlayerInfo(GetPlayerView.GetModel);
        animationController.TextAnimation(fields[0].GetPosition, "+200");
    }

    void GetBuyField(bool isBuy)
    {
        if(isBuy)
        {
            if (fields[GetPlayerView.GetModel.GetFieldID].GetFieldModel.cost < GetPlayerView.GetModel.GetMoney)
            {
                fields[GetPlayerView.GetModel.GetFieldID].Buy(GetPlayerView.GetModel);
                panel.SetPlayerInfo(GetPlayerView.GetModel);
                int cost = fields[GetPlayerView.GetModel.GetFieldID].GetFieldModel.cost;
                animationController.TextAnimation(fields[GetPlayerView.GetModel.GetFieldID].GetPosition, "-" + cost.ToString());
                
            }
        }
        SwitchTurn();

    }

    public void SwitchTurn()
    {
        turn = turn == PlayerType.Player ? PlayerType.AI : PlayerType.Player;
        panel.SetTurn(turn);
        dice.SetDiceDelegate(StartSteps);
        if (turn == PlayerType.Player)
        {
            dice.ActivateDice(true);
        }
        else
        {
            dice.ActivateDice(false);
            dice.Drop(true);
        }
    }

}

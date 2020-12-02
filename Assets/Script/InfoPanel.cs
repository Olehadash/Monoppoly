using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Informatiom panel
 * ThisPanel show 
 * 
 * */
public class InfoPanel : MonoBehaviour
{
    GraphickObject mainBg, activate;
    GraphickObject playerName1, playerName2, activeText;
    GraphickObject playerMoney1, playerMoney2;
    

    Sprite player_amounts, active_player;

    Vector3 redposit = new Vector3(-2.23f, 2.3f, 0);
    Vector3 redTXTposit = new Vector3(-3.16f, 2.45f, 0);
    Vector3 blueposit = new Vector3(2.23f, 2.3f, 0);
    Vector3 blueTXTposit = new Vector3(1.15f, 2.45f, 0);
    Color activeColor = new Color(1.0f, 0.55f, 0);

    bool isFirstActivateRotation = true;

    public void Build(GameObject parent)
    {
        player_amounts = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(0);
        active_player = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(1);

        mainBg = new GraphickObject();
        mainBg.Build(parent, "player_amount");
        mainBg.setImage(player_amounts);
        mainBg.SetLayer(2);
        mainBg.setScale(new Vector3(0.1015f, 0.096f,0));
        mainBg.setPosion(new Vector3(0, 2.99f, 0));

        activate = new GraphickObject();
        activate.Build(parent, "Turn");
        activate.setImage(active_player);
        activate.SetLayer(2);
        activate.setScale(new Vector3(0.1f, 0.1f, 0));
        activate.setPosion(redposit);

        activeText = new GraphickObject();
        activeText.BuildText(parent, "NOW TURN");
        activeText.SetText("NOW PLAYING", 0.2f);
        activeText.SetTextColor(activeColor);
        activeText.setPosion(redTXTposit);

        playerName1 = new GraphickObject();
        playerName1.BuildText(mainBg.GetGameObject(), "PlayerName(1)");
        playerName1.SetText("Player Name", 0.12f);
        playerName1.setPosion(new Vector3(-3.4f, 3.4f, 0));

        playerName2 = new GraphickObject();
        playerName2.BuildText(mainBg.GetGameObject(), "PlayerName(2)");
        playerName2.SetText("Player Name", 0.12f);
        playerName2.SetTextAlgment(TextAlignment.Left);
        playerName2.setPosion(new Vector3(2.45f, 3.4f, 0));


        playerMoney1 = new GraphickObject();
        playerMoney1.BuildText(mainBg.GetGameObject(), "PlayerName(1)");
        playerMoney1.SetText("3500 $", 0.2f);
        playerMoney1.setPosion(new Vector3(-3.4f, 3.0f, 0));

        playerMoney2 = new GraphickObject();
        playerMoney2.BuildText(mainBg.GetGameObject(), "PlayerName(2)");
        playerMoney2.SetText("3500 $", 0.2f);
        playerMoney2.SetTextAlgment(TextAlignment.Left);
        playerMoney2.setPosion(new Vector3(2.6f, 3.0f, 0));

    }

    /*
     * Show liltle panel which show us whos turn
     * */
    public void ActiviteActivate(bool active)
    {
        activate.SetActive(active);
        activeText.SetActive(active);
    }

    /*
     * Setinformation on the panel
     * */
    public void SetPlayerInfo(PlayerModel model)
    {
        switch(model.GetPlayerType)
        {
            case PlayerType.Player:
                playerMoney2.SetText(model.GetMoney + " $", 0.2f);
                playerName2.SetText(model.GetName, 0.12f);
                break;
            case PlayerType.AI:
                playerMoney1.SetText(model.GetMoney + " $", 0.2f);
                playerName1.SetText(model.GetName, 0.12f);
                break;
        }
    }

    /*
     * Switch position off liltle panel which show us whos turn
     * */
    public void SetTurn(PlayerType type)
    {
        ActiviteActivate(true);
        switch (type)
        {
            case PlayerType.AI:
                activeText.setPosion(redTXTposit);
                activate.setPosion(redposit);
                if (!isFirstActivateRotation)
                    activate.Rotate(new Vector3(0, 0, 180));
                break;
            case PlayerType.Player:
                activeText.setPosion(blueTXTposit);
                activate.setPosion(blueposit);
                activate.Rotate(new Vector3(0, 0, 180));
                isFirstActivateRotation = false;
                break;
        }
    }

}

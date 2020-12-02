using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuilder : MonoBehaviour
{

    //All Sptires what you need to use in your game
    [SerializeField]
    Sprite blackSquare;
    [SerializeField]
    Sprite WhiteSquare;
    [SerializeField]
    Sprite free_sprite;
    [SerializeField]
    Sprite player1_sprite;
    [SerializeField]
    Sprite player2_sprite;
    [SerializeField]
    Sprite start_sprite;
    [SerializeField]
    Sprite bonus_sprite;
    [SerializeField]
    Sprite player_Amounts;
    [SerializeField]
    Sprite active_player;
    [SerializeField]
    Sprite dice;
    [SerializeField]
    Sprite diceone;
    [SerializeField]
    Sprite dicetwo;
    [SerializeField]
    Sprite dicethre;
    [SerializeField]
    Sprite dicefour;
    [SerializeField]
    Sprite dicefive;
    [SerializeField]
    Sprite dicesix;
    [SerializeField]
    Sprite playerView1;
    [SerializeField]
    Sprite palyerView2;
    [SerializeField]
    Sprite popupBack;
    [SerializeField]
    Sprite popupButton;
    [SerializeField]
    Sprite button;

    [SerializeField]
    GameController gameController;

    GraphickObject backGround;
    GraphickObject whiteCenter;
    InfoPanel panel = new InfoPanel();
    Dice dicer = new Dice();

    PlayerModel player_model, ai_model;
    PlayerView player_view, ai_view;
    PopupViewer popup;

    float x_resolution, y_resolution;

    List<GameFieldViewer> fields = new List<GameFieldViewer>();

    private void OnValidate()
    {
       /* x_resolution = ScreenResolution.CameraWidth;
        y_resolution = ScreenResolution.CameraHeight;
        Build();*/
    }

    // Start is called before the first frame update
    void Start()
    {
        x_resolution = ScreenResolution.CameraWidth;
        y_resolution = ScreenResolution.CameraHeight;

        player_model = new PlayerModel("Player", 1000, 0, PlayerType.Player);
        ai_model = new PlayerModel("Anemy", 1000, 0, PlayerType.AI);

        Build();
    }
    // Build your Field after Starting game
    void Build()
    {
        BuildBAckGround();
        BuildField();
        SetPlayer();
        BuildPopup();

        gameController.SetParametrs(panel, dicer, player_model, ai_model, player_view, ai_view, popup, fields);
        gameController.StartGame();
    }

    /*
     * Build Popup
     * */

    void BuildPopup()
    {
        popup = new PopupViewer();
        popup.Build(this.gameObject);
        popup.Hide();
        popup = popup.GetPopUP;
    }

    /*
     * Build all Game Chips, and add it on GamePlay
     */
    void SetPlayer()
    {
        player_view = new PlayerView();
        player_view.Build(this.gameObject, player_model);
        player_view.PutOnTheField(fields[0]);

        ai_view = new PlayerView();
        ai_view.Build(this.gameObject, ai_model);
        ai_view.PutOnTheField(fields[0]);
    }
    /*
     * Build GameField and Add it all on gameplay
     * */
    void BuildField()
    {
        int kof = 0;
        for (int i = 0; i < 24; i++)
        {
            GameFieldViewer field = new GameFieldViewer();
            FieldType type = FieldType.None;
            Sprite sprite;
            float x_pos = 0, y_pos = 0;
            Vector3 scale;
            Vector3 rotation;
            if (i == 0)
            {
                type = FieldType.Start;
                sprite = start_sprite;
                scale = new Vector3(1f, 1f, 0);
                field.SetModel(0, 0);
            }
            else if (i == 6  || i == 12|| i==18)
            {
                type = FieldType.Bonus;
                sprite = bonus_sprite;
                kof = 0;
                scale = new Vector3(1f, 1f, 0);
                field.SetModel(0, 0);
            }
            else
            {
                type = FieldType.Free;
                sprite = free_sprite;
                scale = new Vector3(0.47f, 0.5f, 0);
                field.SetModel(100 + i * 10, (100 + i * 10) / 10);
            }
            if (i < 6)
            {
                x_pos = x_resolution - .7f - kof * 1.42f;
                y_pos = -y_resolution + 0.7f;
                rotation = new Vector3(0, 0, 0);
            }
            else if(i<12)
            {
                x_pos = x_resolution - .75f - 6 * 1.42f;
                y_pos = -y_resolution + 0.7f + kof * 1.42f;
                rotation = new Vector3(0, 0, -90.0f);
            }
            else if (i < 18)
            {
                x_pos = x_resolution - .75f - (6 - kof) * 1.42f;
                y_pos = -y_resolution + 0.75f + 6 * 1.42f;
                rotation = new Vector3(0, 0, 180.0f);
            }
            else
            {
                x_pos = x_resolution - .7f;
                y_pos = -y_resolution + 0.75f + (6 - kof) * 1.42f;
                rotation = new Vector3(0, 0, 90.0f);
            }
            field.Build(this.gameObject, type, sprite);
            field.SetPosition(new Vector3(x_pos, y_pos, 0));
            field.SetScale(scale);
            field.Rotate(rotation);
            field.SetLayer(2);
            fields.Add(field);
            kof++;
        }
    }
    /*
     * Build BackGround Elemnts, Info Panel, Dice -  and Add it all on gameplay
     * */
    void BuildBAckGround()
    {
        backGround = new GraphickObject();
        backGround.Build(this.gameObject, "BackGround");
        backGround.setImage(blackSquare);
        backGround.setScale(new Vector3(7, 7, 0));

        whiteCenter = new GraphickObject();
        whiteCenter.Build(this.gameObject, "WhiteCenter");
        whiteCenter.setImage(WhiteSquare);
        whiteCenter.setScale(new Vector3(4.75f, 4.75f, 0));
        whiteCenter.SetLayer(1);

        panel.Build(whiteCenter.GetGameObject());
        panel.ActiviteActivate(false);
        panel.SetPlayerInfo(player_model);
        panel.SetPlayerInfo(ai_model);
        dicer.Build(whiteCenter.GetGameObject());
        dicer = dicer.GetDice;
    }

    /*
     * Take all Sprites And send each to some GamePlay object(like InfoPanel.cs,  for Building
     * */
    public Sprite GetSprite(int kof)
    {
        switch (kof)
        {
            case 0:
                return player_Amounts;
                break;
            case 1:
                return active_player;
                break;
            case 2:
                return dice;
                break;
            case 3:
                return diceone;
                break;
            case 4:
                return dicetwo;
                break;
            case 5:
                return dicethre;
                break;
            case 6:
                return dicefour;
                break;
            case 7:
                return dicefive;
                break;
            case 8:
                return dicesix;
                break;
            case 9:
                return playerView1;
                break;
            case 10:
                return palyerView2;
                break;
            case 11:
                return popupBack;
                break;
            case 12:
                return popupButton;
                break;
            case 13:
                return button;
                break;
            case 14:
                return player1_sprite;
                break;
            case 15:
                return player2_sprite;
                break;

        }
        return null;
    }


}

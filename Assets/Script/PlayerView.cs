using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class describe Chip view on game field
 * */

public class PlayerView : MonoBehaviour
{
    Sprite sprite;

    GraphickObject viewer;
    PlayerModel model;

    public void Build(GameObject parent, PlayerModel model)
    {
        switch(model.GetPlayerType)
        {
            case PlayerType.Player:
                sprite = parent.GetComponent<FieldBuilder>().GetSprite(10);
                break;
            case PlayerType.AI:
                sprite = parent.GetComponent<FieldBuilder>().GetSprite(9);
                break;
        }

        viewer = new GraphickObject();
        viewer.Build(parent, "Player");
        viewer.setImage(sprite);
        viewer.setScale(new Vector3(0.35f, 0.35f, 0));
        viewer.SetLayer(4);
        this.model = model;
    }

    /*
     * REturn GameObject from Main GrafickObject
     * */
    public GameObject GetGameObject
    {
        get
        {
            return viewer.GetGameObject();
        }
    }
    public Vector3 GetPosition
    {
        get
        {
            return GetGameObject.transform.position;
        }
    }
    public PlayerModel GetModel
    {
        get
        {
            return this.model;
        }
    }

    public void PutOnTheField(GameFieldViewer field)
    {
        switch (model.GetPlayerType)
        {
            case PlayerType.Player:
                this.viewer.setPosion(field.GetPosition + new Vector3(0.2f,0.2f,0));
                break;
            case PlayerType.AI:
                this.viewer.setPosion(field.GetPosition - new Vector3(0.2f, 0.2f, 0));
                break;
        }
        
        this.model.SetFieldPosition(field.GetID);
    }

}

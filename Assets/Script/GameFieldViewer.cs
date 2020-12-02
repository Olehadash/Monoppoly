using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldType{
    None,
    Bonus,
    Free,
    Player1,
    Player2,
    Start
};
/*Field information Model
 * **/
public class FieldModel
{
    public int cost = 0; // price for baying 
    public int tax = 0;
    
}

/*
 * 
 * 
 * */

public class GameFieldViewer : MonoBehaviour
{
    FieldModel model;

    GraphickObject obj;
    FieldType fieldtype = FieldType.None;
    int ID = 0;

    public void Build(GameObject parent, FieldType type, Sprite sprite)
    {
        obj = new GraphickObject();
        obj.Build(parent, "Field");
        fieldtype = type;
        obj.setImage(sprite);
    }

    
    /*Set Field Model
     * */
    public void SetModel(int cost, int tax)
    {
        model = new FieldModel();
        model.cost = cost;
        model.tax = tax;
    }

    public FieldModel GetFieldModel
    {
        get
        {
            return model;
        }
    }

    /*
     * Buy Any Free GameField by Player
     * player model = the model with data obut player
     * info panel? for renw informatiom
     * */
    public void Buy(PlayerModel pmodel)
    {
        int fone = pmodel.GetPlayerType == PlayerType.AI ? 14 : 15;
        fieldtype = pmodel.GetPlayerType == PlayerType.AI ? FieldType.Player1 : FieldType.Player2;
        pmodel.AddMoney(-model.cost);
        obj.setImage(obj.GetGameObject().transform.parent.GetComponent<FieldBuilder>().GetSprite(fone));
    }
    /*
     * Pay tax when player chip is on anemy field
     */
    public void PayTax(PlayerModel pmodel, PlayerModel smodel, InfoPanel panel)
    {
        pmodel.AddMoney(-model.tax);
        smodel.AddMoney(model.tax);
        panel.SetPlayerInfo(pmodel);
        panel.SetPlayerInfo(smodel);
    }

    public Vector3 GetPosition
    {
        get { return obj.GetGameObject().transform.position; }
    }

    public FieldType GetFieldType
    {
        get
        {
            return fieldtype;
        }
    }

    public int GetID
    {
        get { return this.ID; }
    }

    public void SetPosition (Vector3 position)
    {
        obj.setPosion(position);
    }
    public void SetLayer(int layer)
    {
        obj.SetLayer(layer);
    }
    public void SetScale (Vector3 scale)
    {
        obj.setScale(scale);
    }

    public void Rotate(Vector3 angle)
    {
        obj.Rotate(angle);
    }


}

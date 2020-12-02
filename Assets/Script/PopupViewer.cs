using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PopupDelegate();
public delegate void PopupDelegateWithBool(bool IsBuy);
public enum PopupType
{
    Start,
    Die,
    Buy,
    Bonus
}
/*PopupMessages Model
 * */
public class Message
{
    public string msg;
    public Vector3 position;

    public Message(string msg, Vector3 pos)
    {
        this.msg = msg;
        this.position = pos;
    }
}

/*
 * Central Popup For Messages
 */

public class PopupViewer : MonoBehaviour
{
    GraphickObject backGround, button, text;
    GraphickObject NoBut, YesBut;
    GraphickObject noButTxt, yesButTxt;
    PopupDelegate popupdelegate;
    PopupDelegateWithBool popupdelegatewithBool;

    Dictionary<PopupType, Message> models = new Dictionary<PopupType, Message>()
    {
        {PopupType.Start, new Message("Let's Start new Game!!", new Vector3(-1.2f, 0.25f,0)) },
        {PopupType.Die, new Message("Click on Dice to detect turn", new Vector3(-1.3f, 0.25f,0)) },
        {PopupType.Buy, new Message("Do you want to buy for", new Vector3(-1.4f, 0.25f,0)) },
        {PopupType.Bonus, new Message("I will get a bonus", new Vector3(-1.4f, 0.25f,0)) }
    };

    /*
     * Build Popup Elements
     * */
    public void Build(GameObject parent)
    {
        backGround = new GraphickObject();
        backGround.Build(parent, "Popup");
        backGround.setImage(parent.GetComponent<FieldBuilder>().GetSprite(11));
        backGround.setScale(new Vector3(0.5f, 0.5f, 0));
        backGround.SetLayer(5);

        button = new GraphickObject();
        button.Build(backGround.GetGameObject(), "PopupButton");
        button.setImage(parent.GetComponent<FieldBuilder>().GetSprite(12));
        button.setPosion(new Vector3(0, -1, 0));
        button.setScale(new Vector3(0.8f, 0.8f, 0));
        button.SetBoxCollider();
        button.SetLayer(5);
        button.SetActive(false);

        NoBut = new GraphickObject();
        NoBut.Build(backGround.GetGameObject(), "PopupNoButton");
        NoBut.setImage(parent.GetComponent<FieldBuilder>().GetSprite(13));
        NoBut.setPosion(new Vector3(-1.5f, -1, 0));
        NoBut.setScale(new Vector3(2f, 2f, 0));
        NoBut.SetBoxCollider();
        NoBut.SetLayer(5);
        NoBut.SetActive(false);

        YesBut = new GraphickObject();
        YesBut.Build(backGround.GetGameObject(), "PopupYesButton");
        YesBut.setImage(parent.GetComponent<FieldBuilder>().GetSprite(13));
        YesBut.setPosion(new Vector3(1.5f, -1, 0));
        YesBut.setScale(new Vector3(2f, 2f, 0));
        YesBut.SetBoxCollider();
        YesBut.SetLayer(5);
        YesBut.SetActive(false);

        text = new GraphickObject();
        text.BuildText(parent, "Message");
        text.SetText("Message", 0.2f, 6);
        text.SetTextColor(Color.black);
        text.SetTextAlgment(TextAlignment.Center);

        noButTxt = new GraphickObject();
        noButTxt.BuildText(NoBut.GetGameObject(), "NO");
        noButTxt.SetText("NO", 0.1f, 6);
        noButTxt.SetTextColor(Color.black);
        noButTxt.SetTextAlgment(TextAlignment.Center);
        noButTxt.setPosion(new Vector3(-1.7f, -0.85f, 0));
        noButTxt.setScale(new Vector3(2f, 2f, 0));

        yesButTxt = new GraphickObject();
        yesButTxt.BuildText(YesBut.GetGameObject(), "YES");
        yesButTxt.SetText("YES", 0.1f, 6);
        yesButTxt.SetTextColor(Color.black);
        yesButTxt.SetTextAlgment(TextAlignment.Center);
        yesButTxt.setPosion(new Vector3(1.25f, -0.85f, 0));
        yesButTxt.setScale(new Vector3(2f, 2f, 0));

        backGround.AddPopupViewComponent().ResetPArametr(this.backGround, this.button, this.text, this.YesBut, this.NoBut);
    }

    /*
     * Button View Componets 
     * here you have to options
     * 1) With one button (false) --OK--
     * 2) With 2 buttons (true) --No-- --Yes--
     * */

    public void YesNoPopupActive(bool active)
    {
        button.SetActive(!active);
        NoBut.SetActive(active);
        YesBut.SetActive(active);
    }
    /* 
     * Resrt parametrs for inserting This Component inside Gameobject
     * */
    public void ResetPArametr(GraphickObject back, GraphickObject button, GraphickObject text, GraphickObject yes, GraphickObject no)
    {
        this.backGround = back;
        this.button = button;
        this.text = text;
        this.YesBut = yes;
        this.NoBut = no;
    }
    /*
     * Get Popup Component From GameObject 
     * */
    public PopupViewer GetPopUP
    {
        get
        {
            return backGround.GetGameObject().GetComponent<PopupViewer>();
        }
    }
    /*
     * Show Hide PopupComponents (Controllers)
     * */

    public void Show(PopupType type, PopupDelegate deleg)
    {
        YesNoPopupActive(false);
        text.SetText(models[type].msg, 0.2f, 6);
        text.setPosion(models[type].position);
        Show();
        popupdelegate = deleg;
    }

    public void Show(PopupType type, PopupDelegateWithBool deleg, int cost)
    {
        YesNoPopupActive(true);
        button.SetActive(false);
        text.SetText(models[type].msg + " " + cost.ToString() + " $", 0.2f, 6);
        text.setPosion(models[type].position);
        Show();
        popupdelegatewithBool = deleg;
    }

    public void Show()
    {
        Activate(true);
    }

    public void Hide()
    {
        Activate(false);
    }

    void Activate(bool active)
    {
        backGround.SetActive(active);
        //button.SetActive(active);
        text.SetActive(active);
    }

    /*Aftar Popup Buttons click events
     */

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit && hit.collider.gameObject.name == "PopupButton")
            {
                Hide();
                popupdelegate();
            }else if(hit && hit.collider.gameObject.name == "PopupNoButton")
            {
                Hide();
                popupdelegatewithBool(false);
            }
            else if (hit && hit.collider.gameObject.name == "PopupYesButton")
            {
                Hide();
                popupdelegatewithBool(true);
            }
        }
    }
}

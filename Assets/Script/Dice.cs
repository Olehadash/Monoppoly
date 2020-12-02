using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SetDicedelegate(int value);
public class Dice : MonoBehaviour
{
    Sprite dice;
    Sprite[] dicesNum = new Sprite[6];

    GraphickObject back, num = new GraphickObject();
    SetDicedelegate dicedelegate;

    bool diceActive = false;
    bool is_dropping = false;
    int dropeednum = 1;

    public void ResetParametrs(Sprite dice, Sprite[] dicesNum, GraphickObject back, GraphickObject num)
    {
        Debug.Log("ResetParametrs " + num);
        this.dice = dice;
        this.dicesNum = dicesNum;
        this.back = back;
        this.num = num;
    }

    public void SetDiceDelegate(SetDicedelegate deleg)
    {
        dicedelegate = deleg;
    }

    public void Build(GameObject parent)
    {

        dice = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(2);
        dicesNum[0] = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(3);
        dicesNum[1] = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(4);
        dicesNum[2] = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(5);
        dicesNum[3] = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(6);
        dicesNum[4] = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(7);
        dicesNum[5] = parent.transform.parent.GetComponent<FieldBuilder>().GetSprite(8);

        back = new GraphickObject();
        back.Build(parent, "Dice");
        back.setImage(dice);
        back.SetLayer(3);
        back.setPosion(new Vector3(0,2.15f, 0));
        back.setScale(new Vector3(0.1f, 0.1f, 0));
        back.AddCollider();
        

        
        num.Build(back.GetGameObject(), "DiceNum");
        num.setImage(dicesNum[3]);
        num.SetLayer(4);
        num.setPosion(new Vector3(0, 2.15f, 0));
        num.setScale(new Vector3(1f, 1f, 0));
        Debug.Log("Rbefore " + num);
        back.AddDiceComponent().ResetParametrs(dice, dicesNum, back, num);
    }


    public Dice GetDice
    {
        get
        {
            return back.GetDice;
        }
    }

    /*
     * On Off Click mode On dice
     * */
    public void ActivateDice(bool active)
    {
        diceActive = active;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !is_dropping && diceActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit && hit.collider.gameObject.name == "Dice")
            {
                is_dropping = true;
                Drop();
            }
        }
    }

    public void Drop(bool is_anemyDroped = false)
    {
        StartCoroutine(DropDice(is_anemyDroped));
    }

    IEnumerator DropDice(bool is_anemyDroped = false)
    {
        if(is_anemyDroped)
            yield return new WaitForSeconds(3f);
        float time = 0;
        while (time < 2)
        {
            dropeednum = Random.Range(0, 6);
            this.gameObject.GetComponent<Dice>().num.setImage(dicesNum[dropeednum]);
            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
        }
        is_dropping = false;
        if(dicedelegate != null)
            dicedelegate(dropeednum+1);
    }

}

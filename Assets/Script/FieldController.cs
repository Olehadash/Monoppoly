using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public List<GameFieldViewer> fields;

    [SerializeField]
    GameObject ChipPrepab;

    PlayerView playerView;
    PlayerView aiView;
    // Start is called before the first frame update
    void Start()
    {
        Build();
        SetPlayer();
    }

    void Build()
    {
        for(int i = 0; i<fields.Count; i++)
        {
            FieldType type = FieldType.Free;
            
            switch(i)
            {
                case 0:
                    type = FieldType.Start;
                    break;
                case 6:
                case 12:
                case 18:
                    type = FieldType.Bonus;
                    break;
            }
            //fields[i].SetType(type);
            //fields[i].setID(i);
        }
    }

    void SetPlayer()
    {
        GameObject player = Instantiate(ChipPrepab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        player.transform.localScale = new Vector3(0.26f, 0.26f, 0.26f);
        playerView = player.GetComponent<PlayerView>();

        GameObject ai = Instantiate(ChipPrepab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        ai.transform.localScale = new Vector3(0.26f, 0.26f, 0.26f);
        aiView = player.GetComponent<PlayerView>();
    }
    
}

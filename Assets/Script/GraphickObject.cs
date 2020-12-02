using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphickObject : MonoBehaviour
{
    GameObject mainGO;
    SpriteRenderer renderer;
    MeshRenderer mesh;
    TextMesh text;
    CircleCollider2D collider;

    public void Build(GameObject parent, string name)
    {
        mainGO = new GameObject(name);
        renderer = mainGO.AddComponent<SpriteRenderer>();
        mainGO.transform.parent = parent.transform;

    }


    public void BuildText(GameObject parent, string name)
    {
        mainGO = new GameObject(name);
        mesh = mainGO.AddComponent<MeshRenderer>();
        text = mainGO.AddComponent<TextMesh>();
        mainGO.transform.parent = parent.transform;
    }

    public void AddCollider()
    {
        collider = mainGO.AddComponent<CircleCollider2D>();
    }

    public Dice AddDiceComponent()
    {
        return mainGO.AddComponent<Dice>();
    }

    public Dice GetDice
    {
        get
        {
            return mainGO.GetComponent<Dice>();
        }
    }

    public PopupViewer AddPopupViewComponent()
    {
        return mainGO.AddComponent<PopupViewer>();
    }

    public void SetText(string caption, float size = 1.0f, int layer = 3)
    {
        mesh.sortingOrder = layer;
        text.text = caption;
        text.characterSize = size;
    }

    public void SetTextColor(Color color)
    {
        text.color = color;
    }

    public void SetTextAlgment(TextAlignment aligment)
    {
        text.alignment = aligment;
    }

    public void setImage(Sprite sprite)
    {
        renderer.sprite = sprite;
    }

    public void setScale(Vector3 scale)
    {
        mainGO.transform.localScale = scale;
    }

    public void Rotate(Vector3 rotate)
    {
        mainGO.transform.Rotate(rotate);
    }

    public void setPosion(Vector3 position)
    {
        mainGO.transform.position = position;
    }

    public void SetBoxCollider()
    {
        mainGO.AddComponent<BoxCollider2D>();
    }
    public void SetActive(bool active)
    {
        mainGO.SetActive(active);
    }

    public void SetLayer(int layer)
    {
        renderer.sortingOrder = layer;
    }
    public SpriteRenderer GetRenderer()
    {
        return renderer;
    }

    public GameObject GetGameObject()
    {
        return mainGO;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AfterAnimationDelegate();

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    GameController gameController;

    AfterAnimationDelegate afterAnimationDelegate;

    /*Show Text with pluss position
     * position - from what position we start animation
     * text - message of ANimation
     * */
    public void TextAnimation(Vector3 position, string text)
    {
        GraphickObject animateText = new GraphickObject();
        animateText.BuildText(this.gameObject, "AnimateText");

        animateText.setPosion(position);
        animateText.SetText(text, 0.2f, 10);
        animateText.SetActive(true);
        StartCoroutine(ShowAnimation(position, animateText));

    }

    IEnumerator ShowAnimation(Vector3 position, GraphickObject animateText)
    {
        OneStepAnimation(animateText.GetGameObject(), position + new Vector3(0, 0.5f, 0));
        yield return new WaitForSeconds(.5f);
        animateText.SetActive(false);
        Destroy(animateText.GetGameObject());
    }

    public void SetAfterAnimationDelegate(AfterAnimationDelegate value)
    { 
        afterAnimationDelegate = value;
    }


    /* OneStepAnimation - animation of movement from choosen object coords to 'lasPosition'
    * obj - gameobject which need to move;
    * lasPosition - Finl coords
    * speed - speed of animation
    * */
    public void OneStepAnimation(GameObject obj, Vector3 lasPosition,float speed = .1f)
    {
        StartCoroutine(MoveToOneField(obj, lasPosition, speed));
        
    }

    IEnumerator MoveToOneField(GameObject obj, Vector3 lasPosition, float time)
    {
        int i = 0;
        while(i<30)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, lasPosition, time);
            yield return null;
            i++;
        }
    }
    /*Animation 
     * obj - Plaerwiev what we need to move
     * fields - All fields viewer;
     * steps - number of steps from Dice
     * speed - sped of Each movement
     * */
    public void SeverallSteps(PlayerView obj, List<GameFieldViewer> fields, int steps, float speed = 0.1f)
    {
        StartCoroutine(PlayStepAnimation(obj, fields, steps, speed));
    }

    IEnumerator PlayStepAnimation(PlayerView obj, List<GameFieldViewer> fields, int steps, float time = 0.1f)
    {
        int step = 0;
        while(step <steps)
        {
            int s = obj.GetModel.GetFieldID + 1;
            if (s == 24)
            {
                s = 0;
                gameController.ShowStartFieldGetBonus();
            }
            OneStepAnimation(obj.GetGameObject, fields[s].GetPosition, time);
            yield return new WaitForSeconds(time+0.4f);
            obj.GetModel.SetFieldPosition(s);
            step++;
        }

        if (afterAnimationDelegate != null)
            afterAnimationDelegate();
    }
}

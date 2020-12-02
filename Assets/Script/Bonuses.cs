using System.Collections;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    /*
     * Here Is test Task realization
     * with to arrays:
     * bonus_array -        50, 40, 60, 30, 70, 90, 10 $
     * chance_array -    0, 50, 60, 70, 80, 90, 95, 100 %
     * */
    [SerializeField]
    AnimationController animation;
    int[] bonus_array = new int[]     { 50, 40, 60, 30, 70, 90, 10 };
    int[] chance_array = new int [] {0, 50, 60, 70, 80, 90, 95, 100 };


    /*
     * Calcate Bonus Method and return int bonus 
     * */
    int CalculateBonus()
    {
        int chance = Random.Range(0, 100);

        int i = 0, j = 0;
        
        for (i = 1; i< chance_array.Length; i++)
        {
            if(chance_array[i-1] <= chance && chance_array[i] > chance)
            {
                return bonus_array[j];
            }
            j++;
        }
        return 0;
    }
    /*
     * Show Bonus Animation and return bonus
     * */
    public int ShowBonusAnimation(Vector3 position)
    {
        int value = CalculateBonus();
        animation.TextAnimation(position, "+"+value.ToString());
        return value;
    }
}

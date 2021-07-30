using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{
    public GameObject playerHomingRecticule;
    public Image playerRegularShieldBar;
    public Image playerOverchargeShieldBar;


    private void OnEnable()
    {
        References.theCanvas = this;
    }

    public void HoneInOnTarget(GameObject target, float percComplete)
    {
        playerHomingRecticule.SetActive(true);

        //Move from the player's reticule to the enemy's postion
        Vector2 startPosition = Camera.main.WorldToScreenPoint(References.thePlayer.myAimTarget.transform.position);
        Vector2 enemyPosition = Camera.main.WorldToScreenPoint(target.transform.position);
        playerHomingRecticule.transform.position = Vector2.Lerp(startPosition, enemyPosition, percComplete); 

    }

    public void StopHone()
    {
        playerHomingRecticule.SetActive(false);
    }

    public void ShowShieldFraction(float fraction)
    {
        float regularFraction = Mathf.Min(fraction, 1);
        playerRegularShieldBar.rectTransform.localScale = new Vector3(1, regularFraction, 1);




        float overchargeFraction = playerRegularShieldBar.rectTransform.rect.height / playerOverchargeShieldBar.rectTransform.rect.height * fraction;
        playerOverchargeShieldBar.rectTransform.localScale = new Vector3(1, overchargeFraction, 1);

        
    }
}

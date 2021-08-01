using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasBehaviour : MonoBehaviour
{
    [Header("Health")]
    public Image playerHealthBar;
    public Image playerHealthFilled;


    [Header("Shield")]
    public GameObject playerHomingRecticule;
    public Image playerRegularShieldBar;
    public Image playerOverchargeShieldBar;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject currentMenu;

    [Header("Scenes")]
    public SceneAsset firstScene;

    private void OnEnable()
    {
        References.theCanvas = this;
    }


    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (currentMenu == mainMenu)
            {
                HideMenu();
            }
            else
            {
                ShowMenu(mainMenu);
            }
        }
    }

    public void ShowMainMenu()
    {
        ShowMenu(mainMenu);
    }


    public void HideMenu()
    {
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
        }
        currentMenu = null;
        Time.timeScale = 1;
    }

    public void ShowMenu(GameObject menuToShow)
    {
        HideMenu();
        currentMenu = menuToShow;
        if (menuToShow != null)
        {
            menuToShow.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void StartNewGame()
    {
        HideMenu();
        SceneManager.LoadScene(firstScene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }



    public void HoneInOnTarget(GameObject target, float percComplete)
    {
        if (target != null)
        {
            playerHomingRecticule.SetActive(true);

            //Move from the player's reticule to the enemy's postion
            Vector2 startPosition = Camera.main.WorldToScreenPoint(References.thePlayer.myAimTarget.transform.position);
            Vector2 enemyPosition = Camera.main.WorldToScreenPoint(target.transform.position);
            playerHomingRecticule.transform.position = Vector2.Lerp(startPosition, enemyPosition, percComplete);
        }
    }

    public void StopHone()
    {
        playerHomingRecticule.SetActive(false);
    }

    public void ShowShieldFraction(float fraction)
    {
        Debug.Log(fraction);
        
        float regularFraction = Mathf.Min(fraction, 1);
        playerRegularShieldBar.rectTransform.localScale = new Vector3(1, regularFraction, 1);

        float overchargeFraction = playerRegularShieldBar.rectTransform.rect.height / playerOverchargeShieldBar.rectTransform.rect.height * fraction;
        playerOverchargeShieldBar.rectTransform.localScale = new Vector3(1, overchargeFraction, 1);

        
    }


    public void ShowHealthFraction(float fraction)
    {
        playerHealthFilled.rectTransform.localScale = new Vector3(1, fraction, 1);
    }

}

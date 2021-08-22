using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasBehaviour : MonoBehaviour
{
    [Header("Health")]
    public Image playerHealthBar;
    public Image playerHealthFilled;

    [Header("Aiming")]
    public GameObject playerFrontAimingReticule;
    public GameObject playerBackAimingReticule;
    public GameObject playerHomingRecticule;

    [Header("Shield")]
    public Image playerRegularShieldBar;
    public Image playerOverchargeShieldBar;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject currentMenu;

    [Header("Debug")]
    [SerializeField]
    private bool showDebugOverlay;
    [SerializeField]
    private GameObject debugOverlay;
    [SerializeField]
    private TMP_Text debugText1;
    [SerializeField]
    private TMP_Text debugText2;
    [SerializeField]
    private TMP_Text debugText3;
    [SerializeField]
    private TMP_Text debugText4;

    [Header("Score")]
    public TMP_Text scoreText;


    private void OnEnable()
    {
        References.theCanvas = this;
    }

    private void Start()
    {
        ShowDebugOverlay();
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

        if (Input.GetButtonDown("Enable Debug Button 1"))
        {
            showDebugOverlay = !showDebugOverlay;
            ShowDebugOverlay();
        }

    }

    public void PrintToDebugOverlay(string textToPrint, int debugTextObjectNum)
    {
        TMP_Text targetTextObject = debugText1;
        switch (debugTextObjectNum)
        {
            case 2:
                targetTextObject = debugText2;
                break;
            case 3:
                targetTextObject = debugText3;
                break;
            case 4:
                targetTextObject = debugText4;
                break;
        }
        targetTextObject.text = textToPrint;
    }

    private void ShowDebugOverlay()
    {
        debugOverlay.SetActive(showDebugOverlay);
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
        SceneManager.LoadScene("OnRailsScene");
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
            Vector2 startPosition = References.theCamera.WorldToScreenPoint(References.thePlayer.myAimTargetFront.transform.position);
            Vector2 enemyPosition = References.theCamera.WorldToScreenPoint(target.transform.position);
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

    public void ShowTargettingReticule(GameObject reticule, Transform target)
    {
        reticule.transform.position = References.theCamera.WorldToScreenPoint(target.position);
    }

}

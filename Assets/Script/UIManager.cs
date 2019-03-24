using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject startScreen = null;
    [SerializeField]
    GameObject returnBtn = null;
    [SerializeField]
    List<GameObject> screenHistory = new List<GameObject>();
    public List<Sprite> introImageHistory = new List<Sprite>();
    public Image[] InputResultImage = new Image[4];
    public Sprite[] inputResultsSprite = new Sprite[6];
    public Image[] GameResultImage = new Image[2];
    public Sprite[] GameResultsSprite1 = new Sprite[6];
    public Sprite[] GameResultsSprite2 = new Sprite[6];
    int currentIntroImageNumber = 1;
    [SerializeField]
    Image introImage = null;
    public RawImage[] ArrowImage = new RawImage[2];


    void Start()
    {
        if (GameManager.state == "Start")
        {
            introImage = GameObject.Find("Introduction").GetComponent<Image>();
            screenHistory = new List<GameObject> { startScreen };
        }
    }
    //Start
    public void OnStartGameBtnClick()
    {   //進入關卡
        GameManager.m_GoState = 2;
    }
    public void OnMainGameBtnClick()
    {   //進入關卡
        GameManager.m_GoState = 1;
    }

    //Main
    public void ShowColorResult(List<PlayerInputInfo> inputresults, string playername)
    {
        foreach (var item in inputresults)
        {
            int index, k = 0;
            if (playername == "Player2") k = 2;
            if (item.inputCondition == EInputCondition.ONE)
                index = 0 + k;
            else index = 1 + k;
            InputResultImage[index].sprite = inputResultsSprite[(int)item.color];
        }
    }
    public void ShowColorResult(string playername)
    {
        int k = 0;
        if (playername == "Player2") k = 2;
        for (int i = 0; i < 2; i++)
            InputResultImage[i + k].sprite = null;
    }
    public void ShowGameResult(string winner, int goal)
    {
        if (winner == "Player1")
        {
            GameResultImage[0].sprite = GameResultsSprite1[goal];
            GameResultImage[1].sprite = GameResultsSprite2[5];
            GameResultImage[0].color = new Color(1, 1, 1, 1);
            GameResultImage[1].color = new Color(1, 1, 1, 1);
            Debug.Log("a11a");
        }
        else
        {
            GameResultImage[1].sprite = GameResultsSprite2[goal];
            GameResultImage[0].sprite = GameResultsSprite1[5];
            GameResultImage[1].color = new Color(1, 1, 1, 1);
            GameResultImage[0].color = new Color(1, 1, 1, 1);
        }
        returnBtn.SetActive(true);
    }

    public void ShowArrow(MapPosition playerMapPosition, MapPosition nearestItemMapPos, string playername)
    {
        int k = 0;
        if (playername == "Player2") k = 1;
        if (playerMapPosition.mapIndexC_X - nearestItemMapPos.mapIndexC_X > 2)
        {
            if (playerMapPosition.mapIndexR_Y - nearestItemMapPos.mapIndexR_Y > 2)
                ShowArrowGo(k, 45.0f, -0.65f, 0.75f);
            else if (playerMapPosition.mapIndexR_Y - nearestItemMapPos.mapIndexR_Y > -2)
                ShowArrowGo(k, 90.0f, -1f, 0f);
            else ShowArrowGo(k, 135.0f, -0.65f, -0.75f);
        }
        else if (playerMapPosition.mapIndexC_X - nearestItemMapPos.mapIndexC_X >= -2)
        {
            if (playerMapPosition.mapIndexR_Y - nearestItemMapPos.mapIndexR_Y > 2)
                ShowArrowGo(k, .0f, 0f, 1f);
            else if (playerMapPosition.mapIndexR_Y - nearestItemMapPos.mapIndexR_Y < -2)
                ShowArrowGo(k, 180.0f, 0f, -1f);
        }
        else if (playerMapPosition.mapIndexC_X - nearestItemMapPos.mapIndexC_X < -2)
        {
            if (playerMapPosition.mapIndexR_Y - nearestItemMapPos.mapIndexR_Y > 2)
                ShowArrowGo(k, -45.0f, 0.65f, 0.75f);
            else if (playerMapPosition.mapIndexR_Y - nearestItemMapPos.mapIndexR_Y > -2)
                ShowArrowGo(k, -90.0f, 1f, 0f);
            else { ShowArrowGo(k, -135.0f, 0.65f, -0.75f); }
        }
        else
        {
            HideArrow(playername);
        }
    }
    void ShowArrowGo(int k, float z, float m, float n)
    {
        ArrowImage[k].transform.rotation = Quaternion.Euler(.0f, .0f, z);//轉向
        ArrowImage[k].rectTransform.anchoredPosition = new Vector2(0 + m * 260.0f, (-60.0f) + n * 280.0f);
        ArrowImage[k].color = new Color(1, 1, 1, 1);
    }

    public void HideArrow(string playername)
    {
        int k = 0;
        if (playername == "Player2") k = 1;
        ArrowImage[k].color = new Color(1, 1, 1, 0);
    }
    //
    public void ToScreen(GameObject target)
    {
        GameObject current = screenHistory[screenHistory.Count - 1];
        if (target == null || target == current) return;
        PlayScreen(current, target, false, screenHistory.Count);
        screenHistory.Add(target);
    }

    public void GoBack(GameObject target)
    {
        if (target != null) target.SetActive(true);
        if (screenHistory.Count > 1)
        {
            int currentIndex = screenHistory.Count - 1;
            PlayScreen(screenHistory[currentIndex], screenHistory[currentIndex - 1], true, currentIndex - 2);
            screenHistory.RemoveAt(currentIndex);
        }
    }

    private void PlayScreen(GameObject current, GameObject target, bool isBack, int order)
    {

        //current.GetComponent<Animator>().SetTrigger(outTrigger);

        if (isBack)
        {
            current.GetComponent<Canvas>().sortingOrder = order;
        }
        else
        {
            current.GetComponent<Canvas>().sortingOrder = order - 1;
            target.GetComponent<Canvas>().sortingOrder = order;
        }

        target.SetActive(true);
        GoToInit();
    }


    public void ToNextImage(GameObject target)
    {
        if (currentIntroImageNumber == introImageHistory.Count - 1 && target != null) target.SetActive(false);
        if (currentIntroImageNumber > introImageHistory.Count || introImageHistory.Count <= 1) return;
        PlayScreen(false, currentIntroImageNumber);
        currentIntroImageNumber++;

    }
    public void GoBackImage()
    {
        if (currentIntroImageNumber > 1 || introImageHistory.Count > 1)
        {
            PlayScreen(true, currentIntroImageNumber);
            currentIntroImageNumber--;
        }
    }
    private void PlayScreen(bool isBack, int currentImageNumber)
    {
        if (currentImageNumber > introImageHistory.Count || introImageHistory.Count < 1) return;
        if (isBack)
        {
            introImage.sprite = introImageHistory[currentImageNumber - 2];
        }
        else
        {
            introImage.sprite = introImageHistory[currentImageNumber];
        }
    }
    public void GoToInit()
    {
        currentIntroImageNumber = 1;
        introImage.sprite = introImageHistory[0];
    }
}
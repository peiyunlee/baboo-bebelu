using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startScreen = null;
    [SerializeField]
    private List<GameObject> screenHistory;
    public List<Sprite> introImageHistory = new List<Sprite>();
    public Image[] InputResultImage = new Image[4];
    public Sprite[] inputResultsSprite = new Sprite[6];
    public Image[] GameResultImage = new Image[2];
    public Sprite[] GameResultsSprite1 = new Sprite[6];
    public Sprite[] GameResultsSprite2 = new Sprite[6];
    int currentIntroImageNumber = 1;
    Image introImage = null;

    void Awake()
    {
        if (GameManager.state == "Start")
        {
            this.screenHistory = new List<GameObject> { this.startScreen };
            introImage = GameObject.Find("Introduction").GetComponent<Image>();
        }
    }
    void Start()
    {
        if (GameManager.state == "Main")
        {

        }
    }

    //Start
    public void OnStartGameBtnClick()
    {   //進入關卡
        GameManager.m_GoState = 2;
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
    }
    //
    public void ToScreen(GameObject target)
    {
        GameObject current = this.screenHistory[this.screenHistory.Count - 1];
        if (target == null || target == current) return;
        this.PlayScreen(current, target, false, this.screenHistory.Count);
        this.screenHistory.Add(target);
    }

    public void GoBack(GameObject target)
    {
        if (target != null) target.SetActive(true);
        if (this.screenHistory.Count > 1)
        {
            int currentIndex = this.screenHistory.Count - 1;
            this.PlayScreen(this.screenHistory[currentIndex], this.screenHistory[currentIndex - 1], true, currentIndex - 2);
            this.screenHistory.RemoveAt(currentIndex);
        }
    }

    private void PlayScreen(GameObject current, GameObject target, bool isBack, int order)
    {

        //current.GetComponent<Animator>().SetTrigger(this.outTrigger);

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
        if (currentIntroImageNumber == this.introImageHistory.Count - 1 && target != null) target.SetActive(false);
        if (currentIntroImageNumber > this.introImageHistory.Count || this.introImageHistory.Count <= 1) return;
        this.PlayScreen(false, currentIntroImageNumber);
        currentIntroImageNumber++;

    }
    public void GoBackImage()
    {
        if (currentIntroImageNumber > 1 || this.introImageHistory.Count > 1)
        {
            this.PlayScreen(true, currentIntroImageNumber);
            currentIntroImageNumber--;
        }
    }
    private void PlayScreen(bool isBack, int currentImageNumber)
    {
        if (currentImageNumber > this.introImageHistory.Count || this.introImageHistory.Count < 1) return;
        if (isBack)
        {
            introImage.sprite = this.introImageHistory[currentImageNumber - 2];
        }
        else
        {
            introImage.sprite = this.introImageHistory[currentImageNumber];
        }
    }
    public void GoToInit()
    {
        currentIntroImageNumber = 1;
        introImage.sprite = this.introImageHistory[0];
    }
}
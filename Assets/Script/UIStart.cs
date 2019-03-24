using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStart : MonoBehaviour
{
    [SerializeField]
    GameObject startScreen = null;
    [SerializeField]
    List<GameObject> screenHistory = new List<GameObject>();
    public List<Sprite> introImageHistory = new List<Sprite>();
    int currentIntroImageNumber = 1;
    [SerializeField]
    Image introImage=null;

    void Start()
    {
            screenHistory = new List<GameObject> { startScreen };
    }
    //Start
    public void OnStartGameBtnClick()
    {   //進入關卡
        GameManager.m_GoState = 2;
    }
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
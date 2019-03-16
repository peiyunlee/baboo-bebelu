using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startScreen;
    [SerializeField]
    private List<GameObject> screenHistory;
    public List<Sprite> introImageHistory = new List<Sprite>();
    int currentIntroImageNumber = 1;
    Image introImage ;
    void Awake()
    {
        this.screenHistory = new List<GameObject> { this.startScreen };
        introImage = GameObject.Find("Introduction").GetComponent<Image>();
    }

    public void ToScreen(GameObject target)
    {
        GameObject current = this.screenHistory[this.screenHistory.Count - 1];
        if (target == null || target == current) return;
        this.PlayScreen(current, target, false, this.screenHistory.Count);
        this.screenHistory.Add(target);
    }

    public void GoBack(GameObject target)
    {
        if(target!=null) target.SetActive(true);
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
        if(currentIntroImageNumber==this.introImageHistory.Count-1&&target!=null) target.SetActive(false);
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
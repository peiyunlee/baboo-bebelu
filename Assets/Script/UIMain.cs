using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    [SerializeField]
    GameObject returnBtn = null;
    public Image[] InputResultImage = new Image[4];
    public Sprite[] inputResultsSprite = new Sprite[6];
    public Image[] GameResultImage = new Image[2];
    public Sprite[] GameResultsSprite1 = new Sprite[6];
    public Sprite[] GameResultsSprite2 = new Sprite[6];
    public RawImage[] ArrowImage = new RawImage[2];

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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{

    [SerializeField]
    float deviation = 0.15f; //誤差範圍
    float bpm = 100.0f;  //每分多少拍
    float secPerBeat;  //1拍幾秒
    float secRound;
    int conditionType;
    public int GetConditionType { get { return conditionType; } }
    public float SongPosition;
    float[] inputTimeRange = new float[4];

    EColor colorResult;

    // Start is called before the first frame update
    void Start()
    {
        secPerBeat = 60.0f / bpm;
        secRound = secPerBeat * 8.0f;
        for (var i = 0; i < 4; i++)
        {
            inputTimeRange[i] = (2.0f + 0.5f * (float)i) * secPerBeat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsSongPlay)
        {    //倒數完
            Condition(SongPosition);
            Debug.Log(conditionType);
        }
    }
    private void Condition(float songPosition)
    {
        float fTime = songPosition % secRound;
        if (fTime >= 0f && fTime < 2.0f * secPerBeat - deviation)
            conditionType = (int)EConditionType.READY;
        else if (fTime >= 2.0f * secPerBeat - deviation && fTime < (4.0f * secPerBeat + deviation))
            conditionType = (int)EConditionType.INPUT;
        else if (fTime >= (4.0f * secPerBeat - deviation) && fTime < (8.0f * secPerBeat))
            conditionType = (int)EConditionType.ANIMATION;
    }

    public List<PlayerInputInfo> GetInputResult(List<PlayerInputInfo> playerInput) //判斷是否在輸入成功區間
    {
        List<PlayerInputInfo> timeResult = new List<PlayerInputInfo>();
        List<PlayerInputInfo> result = new List<PlayerInputInfo>();
        timeResult = InputCondition(playerInput); //先判斷時間成功最多四個結果
        for (var i = 0; i < timeResult.Count; i++)   //區間判斷
        {  
            int j = 0;
            switch (timeResult[i].inputCondition)
            {
                case EInputCondition.ONE:
                    j = 1;
                    break;
                case EInputCondition.THREE:
                    j = 3;
                    break;
                default: //2or4
                    j = 0;
                    break;
            }
            if (j == 0) continue; //繼續下一資料的判斷 過濾2or4
            if (i == timeResult.Count - 1)  //後面沒有資料
            {  
                result.Add(timeResult[i]); //輸出result
                break; //跳出迴圈
            }
            else    //有資料判斷是否是EInputCondition.Two or Four
            {   
                if ((int)timeResult[i + 1].inputCondition == j + 1)
                {
                    result.Add(new PlayerInputInfo(InputColorResult(timeResult[i].color, timeResult[i + 1].color), timeResult[i].inputTime, timeResult[i].inputCondition));
                    i += 1; //跳過下一個資料for
                }
                else    //後面沒有2/4的資料
                {   
                    result.Add(timeResult[i]); //輸出result
                }
            }
        }
        return result;

    }

    //////////半拍版本
    List<PlayerInputInfo> InputCondition(List<PlayerInputInfo> playerInput) //先判斷時間成功最多四個結果
    {
        float playerInputTime;
        bool[] bAlreadyInput = new bool[4];
        List<PlayerInputInfo> timeResult = new List<PlayerInputInfo>();
        for (var i = 0; i < playerInput.Count; i++)
        {
            playerInputTime = playerInput[i].inputTime % secRound;
            for (var j = 0; j < 4; j++) //輪迴區間
            {
                if (playerInputTime >= inputTimeRange[j] - deviation && playerInputTime <= inputTimeRange[j] + deviation) //有符合該區間
                {
                    if (!bAlreadyInput[j]) //該區間尚未成功
                    {
                        timeResult.Add(new PlayerInputInfo(playerInput[i].color, playerInput[i].inputTime, (EInputCondition)(j + 1)));
                        bAlreadyInput[j] = true;
                    }
                    break; //檢查下一資料
                }
                //沒有符合繼續檢查下一區間
            }
        }
        return timeResult;
    }

    // ////////一起案版本
    // List<PlayerInputInfo> InputCondition(List<PlayerInputInfo> playerInput)
    // {//先判斷時間成功最多四個結果
    //     float playerInputTime;
    //     bool[] bAlreadyInput = new bool[4];
    //     List<PlayerInputInfo> timeResult = new List<PlayerInputInfo>();
    //     for (int i = 0; i < playerInput.Count; i++)
    //     {
    //         playerInputTime = playerInput[i].inputTime % secRound;
    //         for (int j = 0; j < 4; j+=2) //輪迴區間
    //         {
    //             if (playerInputTime >= inputTimeRange[j] - Deviation && playerInputTime <= inputTimeRange[j] + Deviation) //有符合該區間
    //             {
    //                 if (!bAlreadyInput[j]) //奇數區間尚未成功，屬於該拍第一個輸入
    //                 {
    //                     timeResult.Add(new PlayerInputInfo(playerInput[i].color, playerInput[i].inputTime, (EInputCondition)(j + 1)));
    //                     bAlreadyInput[j] = true;
    //                     //Debug.Log("區間" + (j + 1) + "成功");
    //                 }
    //                 else if(!bAlreadyInput[j+1])   //奇數區間已成功且第二個輸入尚未成功，屬於該拍第二個輸入
    //                 {
    //                     timeResult.Add(new PlayerInputInfo(playerInput[i].color, playerInput[i].inputTime, (EInputCondition)(j + 2)));
    //                     bAlreadyInput[j+1] = true;
    //                 }
    //                 //else //第一輸入跟第二輸入都有了，失敗捨棄輸入
    //                 break; //檢查下一資料
    //             }
    //             //沒有符合繼續檢查下一區間
    //         }
    //     }
    //     return timeResult;
    // }

    EColor InputColorResult(EColor color1, EColor color2)   //回傳顏色結果
    {
        switch (color1) //判斷顏色組合
        { 
            case EColor.RED:
                if (color2 == EColor.BLUE)
                    colorResult = EColor.PURPLE;
                else if (color2 == EColor.YELLOW)
                    colorResult = EColor.ORANGE;
                else
                    colorResult = color1;
                break;
            case EColor.YELLOW:
                if (color2 == EColor.BLUE)
                    colorResult = EColor.GREEN;
                else if (color2 == EColor.RED)
                    colorResult = EColor.ORANGE;
                else
                    colorResult = color1;
                break;
            case EColor.BLUE:
                if (color2 == EColor.YELLOW)
                    colorResult = EColor.GREEN;
                else if (color2 == EColor.RED)
                    colorResult = EColor.PURPLE;
                else
                    colorResult = color1;
                break;
            default:
                break;
        }
        return colorResult;
    }
}

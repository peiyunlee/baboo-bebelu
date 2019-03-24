using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int time_int = 3;
    [SerializeField]
    Text time_UI;
    void Start(){     //一開始就執行倒數計時。

          
    }

    

    public void DecreaseTimer(){ 
        time_int -= 1;
        if (time_int == 0) {
            time_UI.text = "Start !";
        }
        else if(time_int == -1)
        {
            CancelInvoke("DecreaseTimer");
            time_UI.text = "";
            GameManager.IsGameStartflag=true;
        }
        else{
            time_UI.text = time_int + "";
        }
    }
    public void PlayTimer(){ 
        InvokeRepeating("DecreaseTimer", .5f, 1f);
    }
    public void StopTimer(){ 
        

    }
}

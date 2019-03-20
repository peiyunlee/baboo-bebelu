using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int time_int = 3;
    public Text time_UI;
    void Start(){     //一開始就執行倒數計時。

          
    }

    

    public void DecreaseTimer(){ 
        time_int -= 1;
        time_UI.text = time_int + "";
        if (time_int == 0) {
            time_UI.text = ""+time_int;
            CancelInvoke("timer");
        }
        Debug.Log("DecreaseTimer");
    }
    public void PlayTimer(){ 
        
    }
    public void StopTimer(){ 
        

    }
}

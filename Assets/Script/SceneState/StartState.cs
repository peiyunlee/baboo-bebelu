using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller):base(Controller){
        this.StateName="StartState";
    }

    //開始
    public override void StateBegin() {
        Button tmpStartBtn=GameObject.Find ("StartButton").GetComponent<Button>();
        if(tmpStartBtn!=null)
            tmpStartBtn.onClick.AddListener(OnStartGameBtnClick);
        Button tmpHelpBtn=GameObject.Find ("StartButton").GetComponent<Button>();
        if(tmpHelpBtn!=null)
            tmpHelpBtn.onClick.AddListener(OnHelpGameBtnClick);
        
    }

    //開始
    public override void StateEnd() {
    }

    //更新
    public override void StateUpdate(){
    }

    public void OnStartGameBtnClick(){   //進入關卡
       m_Controller.SetState(new MainState(m_Controller),"Main");
    }

     public void OnHelpGameBtnClick(){   //教學
    }
}

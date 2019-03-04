using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController Controller):base(Controller){
        this.StateName="StartState";
        Debug.Log("StartState建構");
    }

    //開始
    public override void StateBegin() {
        Button tmpBtn=GameObject.Find ("StartButton").GetComponent<Button>();
        if(tmpBtn!=null)
            tmpBtn.onClick.AddListener(OnStartGameBtnClick);
    }

    //開始
    public override void StateEnd() {
        Debug.Log("StartStateEnd");
    }

    //更新
    public override void StateUpdate(){
        Debug.Log("StateUpdate");
        
    }

    public void OnStartGameBtnClick(){
        //教學
       m_Controller.SetState(new MainState(m_Controller),"Main");
    }
}

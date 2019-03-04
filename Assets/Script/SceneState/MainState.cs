using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : ISceneState
{
    public MainState(SceneStateController Controller):base(Controller){
        this.StateName="MainState";
    }

    //開始
    public override void StateBegin() {}

    //開始
    public override void StateEnd() {}

    //更新
    public override void StateUpdate(){
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : ISceneState
{
    public MainState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "MainState";
    }

    //開始
    public override void StateBegin()
    {
        
        
    }

    //開始
    public override void StateEnd() { }

    //更新
    public override void StateUpdate()
    {
    }
    // int RandomFunction(int min,int max){
    //     int k;
    //     k=Random.Range(min,max);
    //     if(x==k) RandomFunction(int min,int max);
    //     return k;
    // } 
}
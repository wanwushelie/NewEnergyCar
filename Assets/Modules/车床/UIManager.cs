using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;

public abstract class State
{
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
public class StateMachine
{
    private State currentState;

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
public class IdleState : State
{
    private StateMachine stateMachine;
    private bool isdianyuan,isdianji,iskapan,isdaoju,isdaojia,isyuanjian;
    public lingjianused lingjianused1;
    GameObject obj;
    public IdleState(StateMachine stateMachine, lingjianused lingjianused1, GameObject obj = null,bool isdianyuan=false, bool isdianji=false,bool isdaojia=false,bool isdaoju=false,bool iskapan=false,bool isyuanjian=false)
    {
        this.stateMachine = stateMachine;
        this.isdianyuan = isdianyuan;
        this.isdianji = isdianji;
        this.obj = obj;
    }//定义函数

    public override void Enter()
    {
       
    }//仅调用一次相当于start

    public override void Update()
    {
        lingjianused1.showed(obj);
        if (obj != null)
        {

        }
        if (isdianji&&!isdianyuan)
        {
            stateMachine.ChangeState(new ErrorState());
        }
        if(isdianyuan&&isdianji)
        {
            stateMachine.ChangeState(new dianjiState());
        }
        if(!iskapan&&isdaoju&&isdaojia&&isyuanjian)
        {
            stateMachine.ChangeState(new YuanjianState());
        }
    }

    public override void Exit()
    {
       
    }
}
public class ErrorState : State
{
    public bool iskapan, isdianji;
    public float distance;
    public ErrorState (bool isdianji=false,bool iskapan=false,float distance=5.0f)
    {
        this.iskapan = iskapan;
        this.isdianji = isdianji;
        this.distance = distance;
    }
    public override void Enter()
    {
     if(isdianji||iskapan||distance<=0.1f)
    {
            playanim();
            if(iskapan)
            {

            }
            else if(isdianji)
            {

            }
            else
            {

            }
     }
    
    }//仅调用一次相当于start

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
    public void playanim()
    {
        Debug.Log("播放动画");
     
    }
   
}
public class dianjiState :State
{
    public override void Enter()
    {
        
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
       
    }
}
public class YuanjianState : State
{
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
public class RotateState : State
{
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
public class UIManager : MonoBehaviour
{
    public GameObject hit;
    public lingjianused lingjianused1;
    private StateMachine stateMachine;
    public bool isdianyuan = false, isdianji = false, isrotate = false, isdaojia = false, isdaoju = false, iskapan = false, isyuanjian = false;
    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState(stateMachine,lingjianused1,hit));//赋初值
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

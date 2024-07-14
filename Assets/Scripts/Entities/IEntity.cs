using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect{
    public string name;
    public float duration;
    public float timeRemaining;
    public bool stackable;
    public int stackCount;
    public bool infinite;
    public bool active;

    public StatusEffect(string name, float duration, bool stackable, int stackCount, bool infinite){
        this.name = name;
        this.duration = duration;
        this.stackable = stackable;
        this.stackCount = stackCount;
        this.infinite = infinite;
        this.active = true;
    }
    
    public void ApllyStatus(){}
    public void RemoveStatus(){}
}

public interface IEntity
{
    Queue<StatusEffect> statusEffects {get; set;}
}


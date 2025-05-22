using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest: ScriptableObject
{
    public string questName;
    [TextArea]
    public string description;
    public List<QuestRequirement> requirements;
    public int rewardGold;
    public bool isCompleted = false;
    public float timeLimit;   
    [HideInInspector]
    public float timeRemaining;        
    [HideInInspector]
    public bool isActive = false;       
    [HideInInspector]
    public bool isFailed = false;
}

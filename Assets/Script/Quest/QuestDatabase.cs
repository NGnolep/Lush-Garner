using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Database", menuName = "Quests Database")]
public class QuestDatabase: ScriptableObject
{
    public List<Quest> quests;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillNode : ScriptableObject
{
    // Start is called before the first frame update

    public CalculatedAttributes atts;
    public BaseSkill addskill;
    public BaseSkill replaceSkill;
    public string description;
    public string nodename;
    public bool isUnlocked = false;


}
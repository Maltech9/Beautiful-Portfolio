using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "", order = 1)]
public class SkillTree : ScriptableObject
{
    // Start is called before the first frame update
    public BaseActorItem link;

    public BaseSkill[] linkedSkills;
    public SkillNode[] startingnode;
    [Tooltip("The Branches for each tree. Ideally 3")]
    public SkillBranch[] branches;
    public string nameofTree;


    public void share()
    {
        Debug.Log(getTree());
    }
    public string getTree()
    {
        string prototype = "";

        foreach(var branch in branches)
        {
            //Middle, left, right for each branch to build the prototype
            prototype += "0-0-0/";
        }
        string[] skillbranch = prototype.Split('/');

        //now we look into each branch and count the unlocked nodes: It will be Left/Center/Right
        int iterator = 0;
        foreach (var branch in branches)
        {
            string[] subbranch  = skillbranch[iterator].Split('-');
            int branchcounter = 0;
            foreach (var sprout in branch.childrenNodes)
            {
                int x = 0;
                int counter = 0;
                foreach (var leaf in sprout.childeren)
                {
                    if (leaf.isUnlocked) counter += 1;
                    x += 1;
                }
                subbranch[branchcounter] = counter.ToString();
            }
            skillbranch[iterator] = string.Join("-", subbranch);
            iterator++;
            
        }
        
        return string.Join("/", skillbranch);


      
    }
    
    public CalculatedAttributes loadTree(string key)
    {
        CalculatedAttributes stats = new CalculatedAttributes();
        string[] trees = key.Split('/');
        int branchiterator = 0;
        foreach(string branch in trees)
        {
            if (branch.Length < 1) break;
            int leavesiterator = 0;
            string[] leaves = branch.Split('-');
            foreach(string node in leaves)
            {
                int counter = int.Parse(node);
                foreach(SkillNode x in branches[branchiterator].childrenNodes[leavesiterator].childeren)
                {
                    if(counter > 0)
                    {
                        stats += x.atts;
                    }
                    counter--;
                }
                leavesiterator++;
            }
            branchiterator++;
        }

        return stats;

    }
    //Grabs the atts of all childeren and feeds them into a struct to be passed along
    public CalculatedAttributes getTreeStats()
    {
        CalculatedAttributes totalAtts = new CalculatedAttributes();

        foreach (var branch in branches)
        {
            foreach (var sprout in branch.childrenNodes)
            {
                int x = 0;
                foreach (var leaf in sprout.childeren)
                {
                    if (leaf.isUnlocked)
                    {
                        totalAtts += leaf.atts;

                        x += 1;
                    }
                }
            }

        }
        Debug.Log(getTree());
        return totalAtts;
    }
}

[System.Serializable]
//Every node on the tree MUST somehow be referenced in this class. Use isActivated() to gather the depth of each branch
public struct SkillBranch
{
    public string BranchName;
    [Tooltip("SKill node of the branch. The order should be left, center, right")]
    public subBranch[] childrenNodes;
}

[System.Serializable]
public struct subBranch
{
    public string branchName;
    public SkillNode[] childeren;
}



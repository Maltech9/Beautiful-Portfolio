using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameplayRule : ScriptableObject
{
    public virtual float GetDamage(
        BaseCharacterEntity defender,
        BaseCharacterEntity attacker,
        Elemental attackerElemental,
        Elemental defenderElemental,
        CalculatedAttributes attackerAttributes,
        CalculatedAttributes defenderAttributes,
        out float stealHp,
        float pAtkRate = 1f,
        float mAtkRate = 1f,
        int hitCount = 1,
        int fixDamage = 0)

    {
        stealHp = 0;
        if (hitCount <= 0)
            hitCount = 1;
        /* The damage system works by evaluating the defenders stats and comparing them to the integer, 10000, the damage dealt is inversely proportional 
        to the defender's defence over 10000. */

        var gameDb = GameInstance.GameDatabase;
    var pDmg = (attackerAttributes.pAtk * pAtkRate) - ((attackerAttributes.pAtk * pAtkRate) * (defenderAttributes.pDef / 10000));
    stealHp += pDmg* attackerAttributes.bloodStealRateByPAtk;

        if (defender.isSlave && hitCount == 7 && attackerAttributes.eva == 0)
            pDmg = +(defenderAttributes.pAtk* 2) - ((defenderAttributes.pAtk* 2) * (defenderAttributes.pDef / 10000));


#if !NO_MAGIC_STATS
        var mDmg = (attackerAttributes.mAtk * mAtkRate) - ((attackerAttributes.mAtk * mAtkRate) * (defenderAttributes.mDef / 10000));
        if (attackerAttributes.soul == 2)
        {
            mDmg += ((float).04 * attackerAttributes.hp);
        }
stealHp += mDmg * attackerAttributes.bloodStealRateByMAtk;

if (defender.isSlave && hitCount == 7 && attackerAttributes.eva == 0)
    mDmg = +(defenderAttributes.mAtk * 2) - ((defenderAttributes.mAtk * 2) * (defenderAttributes.mDef / 10000));
#endif
if (pDmg < 0)
    pDmg = 0;
#if !NO_MAGIC_STATS
if (mDmg < 0)
    mDmg = 0;
#endif
float phspro = pDmg / (pDmg + mDmg);
float mpro = mDmg / (mDmg + pDmg);

var totalDmg = pDmg;
#if !NO_MAGIC_STATS
totalDmg += mDmg;
#endif
//calculate passives

Debug.Log("soul is: " + attackerAttributes.soul);
// Soul is determined by the soul counters on prefabs listed numerically
if (attackerAttributes.soul == 7)
{
    float greed;

    greed = (float).07 * (attackerAttributes.hp - attacker.Hp);
    attacker.Hp += (float).07 * Mathf.Abs((attackerAttributes.hp - attacker.Hp));

    attacker.Manager.SpawnHealText((int)greed, attacker);
}

if (attackerAttributes.soul == 3)
{
    if (defender.Hp <= (defenderAttributes.hp * .4))
    {
        totalDmg = (totalDmg * (float)1.3);
    }
}

if (attackerAttributes.soul == 5)
{
    totalDmg += (float)((.01 * attackerAttributes.mAtk) + (.01 * defenderAttributes.pDef));
}

if (defenderAttributes.soul == 6)
{
    attacker.Hp -= (float)(.1 * defenderAttributes.pDef);
    attacker.Manager.SpawnPoisonText(((int)(.1 * defenderAttributes.pDef)), attacker);
}
// Increase damage based on the difference in defense of the two characters
if (attackerAttributes.eva == 6900)
{
    if (hitCount == 3)
    {

        if (defenderAttributes.hp > attackerAttributes.hp)
            totalDmg = ((defenderAttributes.hp / attacker.Hp) * totalDmg);


    }
    // Quadruple the damage because  this is the limit
    if (hitCount == 4 && attacker.Hp <= (attackerAttributes.hp / 3))
    {
        Debug.Log("LIMIT");
        Debug.Log(totalDmg);
        Debug.Log("max hp: " + attackerAttributes.hp);
        Debug.Log("Curr HP: " + attacker.Hp);
        totalDmg = (totalDmg * 4);
        Debug.Log("New Total Dmg: " + totalDmg);
    }
}

//binding (Read sandy Description)
if (attackerAttributes.eva == 719)
{
    if (hitCount == 3)
    {
        defender.isBound = true;
        attacker.Sandy = attacker;
        defender.Sandy = attacker;
        attacker.isBound = true;
        Debug.Log("Bound");

        if (attacker.bindCount == 0)
        {
            attacker.firstBind = defender;
            attacker.bindCount += 1;
            Debug.Log("Bind Count is 1");
        }
        else if (attacker.bindCount == 1)
        {
            attacker.secondBind = defender;
            attacker.bindCount += 1;
            Debug.Log("Bind count is now 2");

        }
        else if (attacker.bindCount == 2)
        {
            attacker.firstBind.isBound = false;
            attacker.firstBind = null;
            attacker.firstBind = attacker.secondBind;
            attacker.secondBind = defender;
            Debug.Log("I may be stupid");
        }

    }
    if (hitCount == 6)
    {
        attacker.Sandyfrustrarion += 50;
        attacker.Bennycounter += 50;
    }
    if (hitCount == 4)
    {
        int missinghp = 0;
        int heal = 0;
        missinghp = (int)(attackerAttributes.hp - attacker.Hp);
        Debug.Log("Missing HP is " + missinghp);
        if ((int)(1.75 * attacker.Sandyfrustrarion) > missinghp)
        {
            heal = (int)((1.75 * attacker.Sandyfrustrarion) - missinghp);
            attacker.Hp += missinghp;
            defender.Hp += heal;
            attacker.Manager.SpawnHealText(missinghp, attacker);
            defender.Manager.SpawnHealText(heal, defender);
        }
        else
        {
            heal = (int)(1.75 * attacker.Sandyfrustrarion);
            attacker.Hp += heal;
            attacker.Manager.SpawnHealText(heal, attacker);
        }
        attacker.Sandyfrustrarion = 0;
    }
    if (hitCount == 5)
    {
        totalDmg = 2 * attacker.Sandyfrustrarion;
        if (defender.isBound)
        {
            defender.isBound = false;
            attacker.bindCount -= 1;
            defender.Sandy = null;
            if (attacker.firstBind == defender && attacker.secondBind != null)
            {
                attacker.firstBind = attacker.secondBind;
                attacker.secondBind = null;
                Debug.Log("Hope This works...!");
            }
            else if (attacker.secondBind == defender && attacker.firstBind != null)
            {
                attacker.secondBind = null;
            }
            else attacker.firstBind = null;
            defender.Hp -= totalDmg;
            defender.Manager.SpawnCriticalText((int)totalDmg, defender);

            if (attacker.firstBind == null && attacker.secondBind != null)
            {
                Debug.Log("Bruh");
                attacker.firstBind = attacker.secondBind;
                attacker.secondBind = null;


            }
            return 0;


        }
        else
        {
            totalDmg = totalDmg * (defenderAttributes.pDef / 10000);
        }
    }
}
// Increase / Decrease damage by effectiveness
var effectiveness = 1f;
if (attackerElemental != null && attackerElemental.CacheElementEffectiveness.TryGetValue(defenderElemental, out effectiveness))
    totalDmg *= effectiveness;
//lifesteal 

if (attackerAttributes.lifesteal != 0)

{
    float lifeStolen = (attackerAttributes.lifesteal) * (phspro * totalDmg);
    attacker.Hp += (int)lifeStolen;
    if (lifeStolen > 0)
    {

        attacker.Manager.SpawnHealText((int)lifeStolen, attacker);
    }
    else
        attacker.Manager.SpawnPoisonText((int)lifeStolen, attacker);
}
//Spell vamp


if (attackerAttributes.spellvamp != 0)

{
    float vamped = (attackerAttributes.spellvamp) * (mpro * totalDmg);
    attacker.Hp += (int)vamped;
    if (vamped > 0)
    {

        attacker.Manager.SpawnHealText((int)vamped, attacker);
    }
    else
        attacker.Manager.SpawnPoisonText((int)vamped, attacker);
}


totalDmg += Mathf.CeilToInt(totalDmg * Random.Range(gameDb.minAtkVaryRate, gameDb.maxAtkVaryRate)) + fixDamage;
return totalDmg;
    }

    public virtual bool IsCrit(CalculatedAttributes attackerAttributes, CalculatedAttributes defenderAttributes)
{
    return Random.value <= attackerAttributes.critChance;
}

public virtual float GetCritDamage(CalculatedAttributes attackerAttributes, CalculatedAttributes defenderAttributes, float damage)
{
    return damage * attackerAttributes.critDamageRate;
}

public virtual bool IsBlock(CalculatedAttributes attackerAttributes, CalculatedAttributes defenderAttributes)
{
    return Random.value <= defenderAttributes.blockChance;
}

public virtual float GetBlockDamage(CalculatedAttributes attributes, CalculatedAttributes defenderAttributes, float damage)
{
    return 0;
}

public virtual bool IsHit(CalculatedAttributes attackerAttributes, CalculatedAttributes defenderAttributes)
{
#if !NO_EVADE_STATS
    var hitChance = 1f;
    if (attackerAttributes.acc > 0 && defenderAttributes.eva > 0)
        hitChance = 1;
    hitChance = 1;

    return !(hitChance < 0 || Random.value > hitChance);
#else
        return true;
#endif
}



    public virtual int GetBattlePoint(PlayerItem item)
    {
        float battlePoint = 0;
        battlePoint += item.Attributes.hp / 25f;
        battlePoint += item.Attributes.pAtk;
        battlePoint += item.Attributes.pDef;
#if !NO_MAGIC_STATS
        battlePoint += item.Attributes.mAtk;
        battlePoint += item.Attributes.mDef;
#endif
        battlePoint += item.Attributes.spd;
#if !NO_EVADE_STATS
        battlePoint += item.Attributes.acc;
        battlePoint += item.Attributes.eva;
#endif
        return (int)battlePoint;
    }
}

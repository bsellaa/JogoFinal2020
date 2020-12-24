using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Core;

public class LoadPlayerStats : MonoBehaviour
{
    DamageableGameObject damageableGameObject;
    InstantAttackerGameObject instantAttackerGameObject;

    void Awake()
    {
        damageableGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageableGameObject>();
        instantAttackerGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<InstantAttackerGameObject>();

        if(PlayerPrefs.HasKey("maxHealth")) damageableGameObject.defenseStats.maxHealth=PlayerPrefs.GetInt("maxHealth");
        if(PlayerPrefs.HasKey("atkSpeed")) instantAttackerGameObject.offenseStats.attackSpeed=PlayerPrefs.GetFloat("atkSpeed");
        if(PlayerPrefs.HasKey("damage")) instantAttackerGameObject.offenseStats.damage=PlayerPrefs.GetInt("damage");

        damageableGameObject.currentHealth=damageableGameObject.defenseStats.maxHealth;
    }

    void Update()
    {
        PlayerPrefs.SetInt("maxHealth", damageableGameObject.defenseStats.maxHealth);
        PlayerPrefs.SetFloat("atkSpeed", instantAttackerGameObject.offenseStats.attackSpeed);
        PlayerPrefs.SetInt("damage", instantAttackerGameObject.offenseStats.damage);
    }
}

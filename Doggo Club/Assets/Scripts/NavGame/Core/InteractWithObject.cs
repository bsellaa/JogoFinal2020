using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Core;

public class InteractWithObject : MonoBehaviour
{
    public GameObject floatingPoints;
    bool isInTrigger = false;
    string currentObject;
    DamageableGameObject damageableGameObject;
    InstantAttackerGameObject instantAttackerGameObject;

    public bool isPunching { get; private set; }
    public bool isRunning { get; private set; }
    public bool isCrunching { get; private set; }
    public bool startedInteraction { get; private set; }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Interactable")
        {
            isInTrigger = true;
            if(other.name == "punchBag")
            {
                currentObject = "punchBag";
                isPunching = true;
            }
            else if(other.name == "runningMachine")
            {
                currentObject = "runningMachine";
                isRunning = true;
            }
            else if(other.name == "mattress")
            {
                currentObject = "mattress";
                isCrunching = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Interactable")
        {
            isInTrigger = false;
            isPunching = false;
            isRunning = false;
            isCrunching = false;
            startedInteraction = false;
        }
    }

    void Awake()
    {
        damageableGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageableGameObject>();
        instantAttackerGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<InstantAttackerGameObject>();
    }

    void Update()
    {
        if(isInTrigger)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                startedInteraction = true;
                GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject;
                if(currentObject == "punchBag")
                {
                    points.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text="punch!!!";
                    instantAttackerGameObject.offenseStats.damage++;
                    Debug.Log("punchBag stat upgraded " + instantAttackerGameObject.offenseStats.damage);
                }
                else if(currentObject == "runningMachine")
                {
                    points.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text="run!!!";
                    instantAttackerGameObject.offenseStats.attackSpeed+=0.1f;
                    Debug.Log("runningMachine stat upgraded " + instantAttackerGameObject.offenseStats.attackSpeed);
                }
                else if(currentObject == "mattress")
                {
                    points.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text="crunch!!!";
                    damageableGameObject.defenseStats.maxHealth++;
                    Debug.Log("Matress stat upgraded " + damageableGameObject.defenseStats.maxHealth);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartShow : MonoBehaviour
{
    public GameObject man;
    public GameObject shockMan;
    public GameObject slime;
    public GameObject spider;
    public GameObject cerberus;
    public GameObject boneworm;

    public TextMeshProUGUI targetText;

    public Animator hitAnimator;

    public float moveSpeed = 1f;
    public float showTime = 0;

    public bool firstAttackDone = false;
    public bool secondAttackDone = false;


    void Start()
    {
        
    }

    void Update()
    {
        showTime += Time.deltaTime;

        if(showTime <= 3f)
        {
            man.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

        if(showTime >= 3f && showTime <= 6f)
        {
            slime.transform.Translate(-moveSpeed*Time.deltaTime, 0, 0);
            if(showTime >= 4f) targetText.gameObject.SetActive(true);
        }

        if(showTime >= 7f && showTime <= 8f)
        {
            man.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            targetText.gameObject.SetActive(false);
        }

        if (showTime >= 9f && !firstAttackDone)
        {
            hitAnimator.SetTrigger("doAttack");
            firstAttackDone = true;
        }

        if (showTime >= 10f && !secondAttackDone)
        {
            hitAnimator.SetTrigger("doAttack");
            secondAttackDone = true;
            slime.gameObject.SetActive(false);
        }

        if(showTime >= 11f)
        {
            spider.gameObject.SetActive(true);
        }

        if (showTime >= 11.5f)
        {
            cerberus.gameObject.SetActive(true);
        }

        if (showTime >= 12f)
        {
            boneworm.gameObject.SetActive(true);
        }

        if (showTime >= 12.5f && showTime <= 13f)
        {
            shockMan.transform.position = man.transform.position;
            man.gameObject.SetActive(false);
            shockMan.gameObject.SetActive(true);
        }

        if( showTime >=13f &&  showTime <= 14f)
        {
            shockMan.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }

        if (showTime >= 14f)
        {
            shockMan.transform.localScale = new Vector3(-1, 1, 1);
            shockMan.transform.Translate(-moveSpeed * 1.5f * Time.deltaTime, 0, 0);
            spider.transform.Translate(-moveSpeed * 1.5f * Time.deltaTime, 0, 0);
            cerberus.transform.Translate(-moveSpeed * 1.5f * Time.deltaTime, 0, 0);
            boneworm.transform.Translate(-moveSpeed * 1.5f * Time.deltaTime, 0, 0);
        }

        if(cerberus.transform.position.x <= -3f)
        {
            shockMan.transform.position = new Vector3(3f, shockMan.transform.position.y, shockMan.transform.position.z);
            spider.transform.position = new Vector3(5f, spider.transform.position.y, spider.transform.position.z);
            cerberus.transform.position = new Vector3(6f, cerberus.transform.position.y, cerberus.transform.position.z);
            boneworm.transform.position = new Vector3(5f, boneworm.transform.position.y, boneworm.transform.position.z);
        }
    }
}

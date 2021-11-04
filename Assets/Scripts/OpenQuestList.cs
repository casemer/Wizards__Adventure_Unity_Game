using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenQuestList : MonoBehaviour {
    public GameObject QuestList;
    public Image darkRootCross;
    public Image springWaterCross;
    public Image vegetableCross;
    public Image secretCross;

    public bool SkeleBossDead;
    public bool WitchBossDead;
    public bool NiceManMet;
    public bool God;

    private int skeleBoss;

    void Start()
    {
        darkRootCross.enabled = false;
        springWaterCross.enabled = false;
        vegetableCross.enabled = false;
        secretCross.enabled = false;
    }


    void Update()
    {
        


        if (Input.GetButtonDown("Quest"))
        {
            QuestList.SetActive(true);



            if (PlayerPrefs.GetInt("SkeletonBoss") > 0)
            {
                darkRootCross.enabled = true;
            }

            if (PlayerPrefs.GetInt("WitchBoss") > 0)
            {
                springWaterCross.enabled = true;
            }

            if (PlayerPrefs.GetInt("Bjorgen") > 0)
            {
                vegetableCross.enabled = true;
            }

            if (PlayerPrefs.GetInt("GodBoss") > 0)
            {
                secretCross.enabled = true;
            }


        }

        if (Input.GetButtonUp("Quest"))
        {
            QuestList.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

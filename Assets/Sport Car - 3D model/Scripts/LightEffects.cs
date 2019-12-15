using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffects : MonoBehaviour {

    public bool on = false;
    public bool on2 = false;
    public bool on3 = false;

    public bool BrakeL = false;
    public bool ReverseL = false;
    public bool FrontL = false;
    public bool TailL = false;
    public bool IndicatorL = false;
    public bool IndicatorR = false;
    public bool INDICATORS = false;

    public GameObject FL;
    public GameObject BL;
    public GameObject TL;
    public GameObject RL;
    public GameObject IL;
    public GameObject IR;

    public Material FLM;
    public Material TLM;
    public Material RLM;
    public Material ILM;
    public Material IRM;

    private float timer = 0.5f;
    private float timer2 = 0.5f;
    private float btimer = 0.5f;
    private float btimer2 = 0.5f;

    private void Start()
    {
        FL = GameObject.Find("FrontLights");
        BL = GameObject.Find("BrakeLights");
        TL = GameObject.Find("TailLights");
        RL = GameObject.Find("ReverseLights");
        IL = GameObject.Find("LeftIndicators");
        IR = GameObject.Find("RightIndicators");

        FL.SetActive(false);
        BL.SetActive(false);
        TL.SetActive(false);
        RL.SetActive(false);
        IL.SetActive(false);
        IR.SetActive(false);

        FLM.DisableKeyword("_EMISSION");
        TLM.DisableKeyword("_EMISSION");
        RLM.DisableKeyword("_EMISSION");
        ILM.DisableKeyword("_EMISSION");
        IRM.DisableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (IndicatorL)
        {
            if (timer >= 0f)
            {
                timer -= Time.deltaTime;
                IL.SetActive(true);
                ILM.EnableKeyword("_EMISSION");
                timer2 = 0.5f;
            }
            if(timer <= 0f)
            {
                IL.SetActive(false);
                ILM.DisableKeyword("_EMISSION");
                timer2 -= Time.deltaTime;
                if (timer2 <= 0f) timer = 0.5f;
            }
        }
        else
        {
            IL.SetActive(false);
            ILM.DisableKeyword("_EMISSION");
        }

        if (IndicatorR)
        {
            if (btimer >= 0f)
            {
                btimer -= Time.deltaTime;
                IR.SetActive(true);
                IRM.EnableKeyword("_EMISSION");
                btimer2 = 0.5f;
            }
            if (btimer <= 0f)
            {
                IR.SetActive(false);
                IRM.DisableKeyword("_EMISSION");
                btimer2 -= Time.deltaTime;
                if (btimer2 <= 0f) btimer = 0.5f;
            }
        }
        else
        {
            IR.SetActive(false);
            IRM.DisableKeyword("_EMISSION");
        }
            
    }
}

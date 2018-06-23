using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
//using System.Collections.Generic;

public class MenuScript : MonoBehaviour {

    public Manager manager;

    public Vector2 constStarMass;
    public Vector2 constInitAmount;
    public Vector2 constDistRange;
    public Vector2 constMassRange;
    public Vector2 constVelRange;
    public Vector2 constAngDev;
    public Vector2 constBiDir;
    public Vector2 constG;
    public Vector2 constPmerge;

    private string[] inputs = new string[12];
    private float[] constraints = new float [24];

	// Use this for initialization
	void Start () {
        //manager = GameObject.Find("Manager").GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () {

        //generate input array
        inputs[0] = transform.Find("Panel/Text 1/InputField").GetComponent<InputField>().text;
        inputs[1] = transform.Find("Panel/Text 4/InputField 1").GetComponent<InputField>().text;
        inputs[2] = transform.Find("Panel/Text 5/InputField 2").GetComponent<InputField>().text;
        inputs[3] = transform.Find("Panel/Text 5/InputField 3").GetComponent<InputField>().text;
        inputs[4] = transform.Find("Panel/Text 6/InputField 4").GetComponent<InputField>().text;
        inputs[5] = transform.Find("Panel/Text 6/InputField 5").GetComponent<InputField>().text;
        inputs[6] = transform.Find("Panel/Text 7/InputField 6").GetComponent<InputField>().text;
        inputs[7] = transform.Find("Panel/Text 7/InputField 7").GetComponent<InputField>().text;
        inputs[8] = transform.Find("Panel/Text 8/InputField 8").GetComponent<InputField>().text;
        inputs[9] = transform.Find("Panel/Text 9/InputField 9").GetComponent<InputField>().text;
        inputs[10] = transform.Find("Panel/Text 11/InputField 10").GetComponent<InputField>().text;
        inputs[11] = transform.Find("Panel/Text 12/InputField 11").GetComponent<InputField>().text;

        //generate constraints array
        constraints[0] = constStarMass.x;
        constraints[1] = constStarMass.y;
        constraints[2] = constInitAmount.x;
        constraints[3] = constInitAmount.y;
        constraints[4] = constDistRange.x;
        constraints[5] = constDistRange.y;
        constraints[6] = constraints[6];
        constraints[7] = constDistRange.y;
        constraints[8] = constMassRange.x;
        constraints[9] = constMassRange.y;
        constraints[10] = constraints[10];
        constraints[11] = constMassRange.y;
        constraints[12] = constVelRange.x;
        constraints[13] = constVelRange.y;
        constraints[14] = constraints[14];
        constraints[15] = constVelRange.y;
        constraints[16] = constAngDev.x;
        constraints[17] = constAngDev.y;
        constraints[18] = constBiDir.x;
        constraints[19] = constBiDir.y;
        constraints[20] = constG.x;
        constraints[21] = constG.y;
        constraints[22] = constPmerge.x;
        constraints[23] = constPmerge.y;
           

        //clamp all
        for (int i = 0;i<12;i++)
        {
            if (inputs[i] != null)
            {
                inputs[i] = Mathf.Clamp(float.Parse(inputs[i], System.Globalization.CultureInfo.InvariantCulture), constraints[i * 2], constraints[i * 2 + 1]).ToString();
            }
        }


        //send values
        transform.Find("Panel/Text 1/InputField").GetComponent<InputField>().text = inputs[0];
        transform.Find("Panel/Text 4/InputField 1").GetComponent<InputField>().text = inputs[1];
        transform.Find("Panel/Text 5/InputField 2").GetComponent<InputField>().text = inputs[2];
        transform.Find("Panel/Text 5/InputField 3").GetComponent<InputField>().text = inputs[3];
        transform.Find("Panel/Text 6/InputField 4").GetComponent<InputField>().text = inputs[4];
        transform.Find("Panel/Text 6/InputField 5").GetComponent<InputField>().text = inputs[5];
        transform.Find("Panel/Text 7/InputField 6").GetComponent<InputField>().text = inputs[6];
        transform.Find("Panel/Text 7/InputField 7").GetComponent<InputField>().text = inputs[7];
        transform.Find("Panel/Text 8/InputField 8").GetComponent<InputField>().text = inputs[8];
        transform.Find("Panel/Text 9/InputField 9").GetComponent<InputField>().text = inputs[9];
        transform.Find("Panel/Text 11/InputField 10").GetComponent<InputField>().text = inputs[10];
        transform.Find("Panel/Text 12/InputField 11").GetComponent<InputField>().text = inputs[11];
 
 	}

    public void SetValues()
    {
        manager.starM = float.Parse(inputs[0], System.Globalization.CultureInfo.InvariantCulture);
        manager.starFixed = transform.Find("Panel/Text 2/Toggle").GetComponent<Toggle>().isOn;
        manager.amount = int.Parse(inputs[1], System.Globalization.CultureInfo.InvariantCulture);
        manager.Rdist = new Vector2 (float.Parse(inputs[2], System.Globalization.CultureInfo.InvariantCulture),
                                    float.Parse(inputs[3], System.Globalization.CultureInfo.InvariantCulture));
        manager.Rmass = new Vector2(float.Parse(inputs[4], System.Globalization.CultureInfo.InvariantCulture),
                                    float.Parse(inputs[5], System.Globalization.CultureInfo.InvariantCulture));
        manager.Rvel = new Vector2(float.Parse(inputs[6], System.Globalization.CultureInfo.InvariantCulture),
                                    float.Parse(inputs[7], System.Globalization.CultureInfo.InvariantCulture));
        manager.Rdev = float.Parse(inputs[8], System.Globalization.CultureInfo.InvariantCulture);
        manager.Prev = float.Parse(inputs[9], System.Globalization.CultureInfo.InvariantCulture);
        manager.G = float.Parse(inputs[10], System.Globalization.CultureInfo.InvariantCulture);
        manager.Pmerge = float.Parse(inputs[11], System.Globalization.CultureInfo.InvariantCulture);
    }
}
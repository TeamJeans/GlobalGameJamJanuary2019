using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCooking : MonoBehaviour {
    //the small sections of meat which together make up the entire meat
    public GameObject meatSection1;
    public GameObject meatSection2;
    public GameObject meatSection3;
    public GameObject meatSection4;

    public Color startColour;
    public Color endColour;

    float rotate;

    //cooking level of each section
    float[] cooking = new float[4];
    public float cookedLevel { get { return cooking[0] + cooking[1] + cooking[2] + cooking[3]; } }

    void Update()
    {
        //get left and right from the joystick and rotate the bigMeat by that amount plus a multiplier
        if (Input.GetAxis("XboxRightJSHorizontal") != 0)
        {
            transform.Rotate(0, 0, Input.GetAxis("XboxRightJSHorizontal") * 50 * Time.deltaTime);
            rotate += Input.GetAxis("XboxRightJSHorizontal") * Time.deltaTime;
        }
        //Debug.Log("rotation" + rotate);

        if (rotate < 0.0f) rotate = 7.1f;
        if (rotate > 7.1f) rotate = 0.0f;

        if (rotate >= 0.0f && rotate < 1.8f) cooking[1] += 0.1f * Time.deltaTime;
        if (rotate >= 1.8f && rotate < 3.5f) cooking[2] += 0.1f * Time.deltaTime;
        if (rotate >= 3.5f && rotate < 5.3f) cooking[3] += 0.1f * Time.deltaTime;
        if (rotate >= 5.3f && rotate <= 7.1) cooking[0] += 0.1f * Time.deltaTime;


        //Debug.Log("1: " + cooking[0]);
        //Debug.Log("2: " + cooking[1]);
        //Debug.Log("3: " + cooking[2]);
        //Debug.Log("4: " + cooking[3]);


        //change material colour
        meatSection1.GetComponent<Renderer>().material.color = Color.Lerp(startColour, endColour, cooking[0]);
        meatSection2.GetComponent<Renderer>().material.color = Color.Lerp(startColour, endColour, cooking[1]);
        meatSection3.GetComponent<Renderer>().material.color = Color.Lerp(startColour, endColour, cooking[2]);
        meatSection4.GetComponent<Renderer>().material.color = Color.Lerp(startColour, endColour, cooking[3]);


    }
}

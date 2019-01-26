﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;

    private string[] sentences;
    private int index;
    public float typingSpeed;
    public float timer;
    public float sentenceTime;

    public int currentNight; //TODO put into static script so aidan can change day
    public int currentTalker;
    public int[] talkingOrder = new int[10];

    //the order of who talks each night
    private int[] night1 = new int[10] { 4, 2, 1, 1, 3, 2, 1, 1, 2, 1 };
    private int[] night2 = new int[10] { 2, 2, 1, 1, 2, 2, 1, 1, 2, 1 };
    private int[] night3 = new int[10] { 1, 2, 1, 1, 2, 2, 1, 1, 2, 1 };
    private int[] night4 = new int[10] { 1, 2, 1, 1, 2, 2, 1, 1, 2, 1 };
    private int[] night5 = new int[10] { 1, 2, 1, 1, 2, 2, 1, 1, 2, 1 };


    //sentences
    [TextArea(3, 10)]
    public string[] nightWords1 = new string[10];
    [TextArea(3, 10)]
    public string[] nightWords2 = new string[10];
    [TextArea(3, 10)]
    public string[] nightWords3 = new string[10];
    [TextArea(3, 10)]
    public string[] nightWords4 = new string[10];
    [TextArea(3, 10)]
    public string[] nightWords5 = new string[10];

    public GameObject TalkerIcon1;
    public GameObject TalkerIcon2;
    public GameObject TalkerIcon3;
    public GameObject TalkerIcon4;

    void Start()
    {
        currentNight = 1;
        StartCoroutine(Type());
        sentenceTime = 5;
        currentTalker = -1;



        //only display current talker
        currentTalker++;
        switch (talkingOrder[currentTalker])
        {
            case 1:
                {
                    TalkerIcon1.SetActive(true);
                    TalkerIcon2.SetActive(false);
                    TalkerIcon3.SetActive(false);
                    TalkerIcon4.SetActive(false);
                    break;
                }
            case 2:
                {
                    TalkerIcon1.SetActive(false);
                    TalkerIcon2.SetActive(true);
                    TalkerIcon3.SetActive(false);
                    TalkerIcon4.SetActive(false);
                    break;
                }
            case 3:
                {
                    TalkerIcon1.SetActive(false);
                    TalkerIcon2.SetActive(false);
                    TalkerIcon3.SetActive(true);
                    TalkerIcon4.SetActive(false);
                    break;
                }
            case 4:
                {
                    TalkerIcon1.SetActive(false);
                    TalkerIcon2.SetActive(false);
                    TalkerIcon3.SetActive(false);
                    TalkerIcon4.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }

    //timer for next sentence
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= sentenceTime) NextSentence();
    }

    //reset the text bubble
    IEnumerator Type()
    {
        //check which night it is and set the icons to follow the conversation
        switch (currentNight)
        {
            case 1:
                {
                    talkingOrder = night1;
                    sentences = nightWords1;
                    break;
                }
            case 2:
                {
                    talkingOrder = night2;
                    sentences = nightWords2;
                    break;
                }
            case 3:
                {
                    talkingOrder = night3;
                    sentences = nightWords3;
                    break;
                }
            case 4:
                {
                    talkingOrder = night4;
                    sentences = nightWords4;
                    break;
                }
            case 5:
                {
                    talkingOrder = night5;
                    sentences = nightWords5;
                    break;
                }


            default:
                break;
        }

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    //move on to the next sentence if the current one has completed and the timer has reached the end
    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            currentTalker++;
            timer = 0;

            switch (talkingOrder[currentTalker])
            {
                case 1:
                    {
                        TalkerIcon1.SetActive(true);
                        TalkerIcon2.SetActive(false);
                        TalkerIcon3.SetActive(false);
                        TalkerIcon4.SetActive(false);
                        break;
                    }
                case 2:
                    {
                        TalkerIcon1.SetActive(false);
                        TalkerIcon2.SetActive(true);
                        TalkerIcon3.SetActive(false);
                        TalkerIcon4.SetActive(false);
                        break;
                    }
                case 3:
                    {
                        TalkerIcon1.SetActive(false);
                        TalkerIcon2.SetActive(false);
                        TalkerIcon3.SetActive(true);
                        TalkerIcon4.SetActive(false);
                        break;
                    }
                case 4:
                    {
                        TalkerIcon1.SetActive(false);
                        TalkerIcon2.SetActive(false);
                        TalkerIcon3.SetActive(false);
                        TalkerIcon4.SetActive(true);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
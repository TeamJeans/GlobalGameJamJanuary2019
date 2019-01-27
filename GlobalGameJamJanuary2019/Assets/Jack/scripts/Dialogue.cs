﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    
    public GameObject bigMeat;
    public TextMeshProUGUI textDisplay;

    private string[] sentences;
    private int index;
    public float typingSpeed;
    public float timer;
    public float sentenceTime;
    private int chatting;
    public bool endScene;

    public int currentNight; //TODO put into static script to change day
    public int currentTalker;
    public int[] talkingOrder = new int[10];

    //the order of who talks each night
    private int[] night1 = new int[22] { 2, 1, 3, 1, 2, 1, 2, 1, 2, 3, 2, 4, 2, 1, 3, 1, 1, 2, 2, 3, 1, 2 };
    private int[] night2 = new int[23] { 2, 1, 3, 2, 1, 1, 1, 2, 2, 3, 3, 2, 1, 2, 3, 4, 1, 2, 1, 2, 4, 2, 3 };
    private int[] night3 = new int[22] { 1, 2, 1, 2, 4, 2, 1, 1, 1, 1, 1, 1, 1, 4, 2, 1, 1, 2, 1, 4, 2, 1 };
    private int[] night4 = new int[22] { 2, 4, 3, 4, 3, 3, 1, 4, 3, 4, 2, 3, 3, 2, 4, 1, 4, 3, 2, 4, 2, 4 };
    private int[] night5 = new int[26] { 2, 1, 2, 1, 2, 1, 3, 3, 3, 1, 2, 4, 3, 4, 1, 2, 3, 1, 2, 1, 2, 1, 2, 1, 2, 1 };




    //sentences
    [TextArea(3, 10)]
    public string[] nightWords1 = new string[22];
    [TextArea(3, 10)]
    public string[] nightWords2 = new string[20];
    [TextArea(3, 10)]
    public string[] nightWords3 = new string[20];
    [TextArea(3, 10)]
    public string[] nightWords4 = new string[20];
    [TextArea(3, 10)]
    public string[] nightWords5 = new string[20];

    //reaction to cooking, 1.raw, 2.well done, 3.burnt
    [TextArea(3, 10)]
    public string cookingReaction1;
    public string cookingReaction2;
    public string cookingReaction3;
    private int[] cookingReactionIcon = new int[4] { 1, 2, 3, 4 };

    public GameObject TalkerIcon1;
    public GameObject TalkerIcon2;
    public GameObject TalkerIcon3;
    public GameObject TalkerIcon4;

    void Start()
    {
       
        endScene = false;
        chatting = 0;
        currentNight = StaticValueHolder.CurrentNight;

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


        StartCoroutine(Type());
        sentenceTime = 7;
        currentTalker = 0;

        switch (talkingOrder[currentTalker])
        {
            case 1:
                {
                    TalkerIcon1.SetActive(true);
                    TalkerIcon2.SetActive(false);
                    TalkerIcon3.SetActive(false);
                    TalkerIcon4.SetActive(false);
                    Debug.Log("TALKER1: " + currentTalker);
                    break;
                }
            case 2:
                {
                    TalkerIcon1.SetActive(false);
                    TalkerIcon2.SetActive(true);
                    TalkerIcon3.SetActive(false);
                    TalkerIcon4.SetActive(false);
                    Debug.Log("TALKER2: " + currentTalker);
                    break;
                }
            case 3:
                {
                    TalkerIcon1.SetActive(false);
                    TalkerIcon2.SetActive(false);
                    TalkerIcon3.SetActive(true);
                    TalkerIcon4.SetActive(false);
                    Debug.Log("TALKER3: " + currentTalker);
                    break;
                }
            case 4:
                {
                    TalkerIcon1.SetActive(false);
                    TalkerIcon2.SetActive(false);
                    TalkerIcon3.SetActive(false);
                    TalkerIcon4.SetActive(true);
                    Debug.Log("TALKER4: " + currentTalker);
                    break;
                }
            default:
                break;
        }

        //only display current talker
        currentTalker++;
    }

    //timer for next sentence
    private void Update()
    {
        //while still talking/cooking
        if (!endScene)
        {
            timer += Time.deltaTime;
            if (timer >= sentenceTime)
            {
                NextSentence();
                chatting++;
                timer = 0;
            }
            Debug.Log("Chatting: " + chatting);
            Debug.Log("sentences.Length: " + sentences.Length);

            if (chatting >= sentences.Length)
            {
                for (int i = 0; i < sentences.Length; i++) sentences[i] = "";

                if (bigMeat.GetComponent<MeatCooking>().cookedLevel >= 0.0f && bigMeat.GetComponent<MeatCooking>().cookedLevel < 4.0f)
                {
                    sentences[1] = cookingReaction1;
                }
                if (bigMeat.GetComponent<MeatCooking>().cookedLevel >= 4.0f && bigMeat.GetComponent<MeatCooking>().cookedLevel < 8.0)
                {
                    sentences[1] = cookingReaction2;
                }
                if (bigMeat.GetComponent<MeatCooking>().cookedLevel >= 8.0f && bigMeat.GetComponent<MeatCooking>().cookedLevel <= 12.0)
                {
                    sentences[1] = cookingReaction3;
                }
                Debug.Log("CookedLevel: " + bigMeat.GetComponent<MeatCooking>().cookedLevel);
                currentTalker = 0;
                index = 0;
                NextSentence();
                chatting = 0;
                endScene = true;
                timer = 0;
                StaticValueHolder.CurrentNight ++;
            }
        }

        //once finished talking/cooking
        
    }

    //reset the text bubble
    IEnumerator Type()
    {
        //print out each letter with slight delay to give typing effect
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    //move on to the next sentence if the current one has completed and the timer has reached the end
    public void NextSentence()
    {
        if (index < sentences.Length-1)
        {
            index++;
            textDisplay.text = "";

            Debug.Log("talkingOrder: " + talkingOrder.Length);
            Debug.Log("currentTalker: " + currentTalker);
            //check current talker and show their icon
            switch (talkingOrder[currentTalker])
            {
                case 1:
                    {
                        TalkerIcon1.SetActive(true);
                        TalkerIcon2.SetActive(false);
                        TalkerIcon3.SetActive(false);
                        TalkerIcon4.SetActive(false);
                        Debug.Log("TALKER1: " + currentTalker);
                        break;
                    }
                case 2:
                    {
                        TalkerIcon1.SetActive(false);
                        TalkerIcon2.SetActive(true);
                        TalkerIcon3.SetActive(false);
                        TalkerIcon4.SetActive(false);
                        Debug.Log("TALKER2: " + currentTalker);
                        break;
                    }
                case 3:
                    {
                        TalkerIcon1.SetActive(false);
                        TalkerIcon2.SetActive(false);
                        TalkerIcon3.SetActive(true);
                        TalkerIcon4.SetActive(false);
                        Debug.Log("TALKER3: " + currentTalker);
                        break;
                    }
                case 4:
                    {
                        TalkerIcon1.SetActive(false);
                        TalkerIcon2.SetActive(false);
                        TalkerIcon3.SetActive(false);
                        TalkerIcon4.SetActive(true);
                        Debug.Log("TALKER4: " + currentTalker);
                        break;
                    }
                default:
                    break;
            }


            StartCoroutine(Type());
            currentTalker++;
        }

    }
}
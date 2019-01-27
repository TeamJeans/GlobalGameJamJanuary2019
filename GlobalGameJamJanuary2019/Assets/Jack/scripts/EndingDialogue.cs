﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class EndingDialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    private string[] sentences;
    private int index;
    public float typingSpeed;
    public float timer;
    public float sentenceTime;
    public bool endScene;

    public int currentNight; //TODO put into static script to change day
    public int currentTalker;
    public int[] talkingOrder = new int[10];

    //the order of who talks each night
    private int[] night1 = new int[9] { 3, 2, 1, 2, 1, 4, 3, 4, 0 };

    //sentences
    [TextArea(3, 10)]
    public string[] nightWords1 = new string[9];

    public GameObject TalkerIcon1;
    public GameObject TalkerIcon2;
    public GameObject TalkerIcon3;
    public GameObject TalkerIcon4;

    void Start()
    {
        endScene = false;
        talkingOrder = night1;
        sentences = nightWords1;

        StartCoroutine(Type());
        sentenceTime = 5;
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
                timer = 0;
            }
            Debug.Log("sentences.Length: " + sentences.Length);
        }       
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampSpeechBubbleUI : MonoBehaviour {

	[SerializeField]
	Image speechBubble;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 bubblePos = Camera.main.WorldToScreenPoint(this.transform.position);
		bubblePos = new Vector3(bubblePos.x,bubblePos.y + 50,bubblePos.z);
		speechBubble.transform.position = bubblePos;
	}
}

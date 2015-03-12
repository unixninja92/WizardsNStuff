﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;

public class CharSelector : MonoBehaviour {
    public int playerNum = 1;
    enum CurElement{L, R, P};
    CurElement selected = CurElement.L;
    public ElementSelector left;
    public ElementSelector right;
    
    int counter = 0;
    const int time = 20;
    
    private const float modifier = 0.3f;

	// Use this for initialization
	void Start () {
//        Debug.Log(XCI.GetNumPluggedCtrlrs());
//        if(playerNum > 2 && XCI.GetNumPluggedCtrlrs() <= 2 && playerNum > XCI.GetNumPluggedCtrlrs()) {
//            this.gameObject.SetActive(false);
//            return;
//        }
        //        left.modifier = modifier;
        left.playerNum = playerNum;
        right.playerNum = playerNum;
        left.other = right;
        right.other = left;
	    left.selected = true;
        left.saveElement();
//        right.modifier = modifier;
        right.selected = false;
        right.playerNum = playerNum;
        right.saveElement();
        Debug.Log(GameInit.getPlayerElement(playerNum, 0).getName());
	}
	
	// Update is called once per frame
    void OnGUI () {
    	Play play = GameInit.playButton;
	    float y = XCI.GetAxis(XboxAxis.LeftStickY, playerNum);
        if(y < -0.4) {
			if(selected == CurElement.L) {
			   selected = CurElement.R;
                left.selected = false;
                right.selected = true;
                right.background.color = new Color(right.background.color.r + modifier, right.background.color.g + modifier, right.background.color.b + modifier);
                left.background.color = new Color(left.background.color.r - modifier, left.background.color.g - modifier, left.background.color.b - modifier);
            //                right.background.color.r = right.background.color.r + 10;
                counter = time;
            }
//	            else if(selected == CurElement.R
        }
		else if(y > 0.4) {
			if(selected == CurElement.R) {
				selected = CurElement.L;
                right.selected = false;
                left.selected = true;
                left.background.color = new Color(left.background.color.r + modifier, left.background.color.g + modifier, left.background.color.b + modifier);
                right.background.color = new Color(right.background.color.r - modifier, right.background.color.g - modifier, right.background.color.b - modifier);
                counter = time;
            }
        }
	}
}

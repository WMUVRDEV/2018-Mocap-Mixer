using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Colorful;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;
using UnityEngine.Playables;

[ExecuteInEditMode]
public class SceneControl : MonoBehaviour {

//public ArrayList camMoveTimeArray = new ArrayList();

public KeyCode ExecuteKey;
public bool jumpToCue = false;
public int jumpCueNumber = 0;
public float[] cueTimes;
public float nextCueTime;
public int lastCueNumber;


public int cueToRun;
	
public bool inPlayMode;	
	
public AudioClip music;
public float musicVolume = 1.0f;

public float audioFadeOutTime = 2.0f;


public enum MocapClips {Phrase1, Phrase2, Phrase3, Phrase4, Phrase5}
public enum CameraMovement {DollyIn, DollyOut, OrbitLeft, OrbitRight}
public enum CameraEffects {Bloom, Glitch, WaveDistortion, Kuwahara, AnalogTV, LoFi, Blur, Pixelate}
public enum FloorTex {Tiles, Streaks, Circles, Glitch}

public FloorTex[] floorTexture; // gives us the menu of possible floor textures

public CameraMovement[] cameraMove; 
public  Vector3[] cameraLoc; 

public  Vector3[] light1Loc; 
public  Vector3[] light2Loc; 
public  Vector3[] light3Loc; 

public float[] cameraMoveTime; 
public float[] cameraMoveAmount; 

public float[] cameraVerticalMoveAmount;
public CameraEffects[] cameraEffect;

public bool[] camerafollowing;


public Color[] light1Color;
public Color[] light2Color;
public Color[] light3Color;
public float[] lightFadeTime;
public float[] lightVerticalMoveAmount;

public Color noChangesBelowThis;

public AudioSource audioControl;

public GameObject lightParent;

public GameObject[] lights;
public Light[] lightLights;
public GameObject theCamera;
public GameObject lightOne;
public GameObject lightTwo;
public GameObject lightThree;
public Camera theCameraComponent;
public SmoothLookAt cameraLookAt;
public SmoothLookAt secondCameraLookAt;

public bool fadeCamBG = false;
public float camFadeDuration;
public Color[] camColors;

public Text uiText;

public Color[] cameraBackground;

public Material[] floorTextures;
public GameObject floor;
public GameObject mocapHip;

public Vector3 camMoveDiff;

public Transform cube;
public AudioSource danceMusic;

private GameObject dirtyObject;
public GameObject[] allDirty;
public PlayableDirector timeline;
	


	void Start () {

	DOTween.Init(false, true);
	//DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(100000, 50);;


	for (int l = 0; l<lightLights.Length; l++){
		lightLights[l].DOColor(Color.black, 2.0f);
	} 



	 audioControl.clip = music;
	 audioControl.volume = musicVolume;

	//  floor.GetComponent<Renderer>().material = floorTextures[0];


	 	switch(floorTexture[cueToRun])
		 {
	     case FloorTex.Tiles: 
	    	 floor.GetComponent<Renderer>().material = floorTextures[0];
    	 break; 
    	 case FloorTex.Streaks: 
	    	 floor.GetComponent<Renderer>().material = floorTextures[1];
    	 break; 
    	 case FloorTex.Circles: 
	    	 floor.GetComponent<Renderer>().material = floorTextures[2];
    	 break; 
    	 case FloorTex.Glitch: 
	     	floor.GetComponent<Renderer>().material = floorTextures[3];
    	 break;
    	 default:
	       Debug.Log("NOTHING");
	        break;
		}


	}
	
public void StartPlayback(){
        	if (!audioControl.isPlaying){
            Debug.Log("Playing from SC");
 
     
         
            audioControl.Play();
           // timeline.time = cueTimes[cueToRun];
            timeline.Play();

            /*	mocapHip = GameObject.FindWithTag("hip");
				cameraLookAt.target = mocapHip.transform;
				secondCameraLookAt.target = mocapHip.transform;

				lights[0].GetComponent<SmoothLookAt>().target = mocapHip.transform;
				lights[1].GetComponent<SmoothLookAt>().target = mocapHip.transform;
				lights[2].GetComponent<SmoothLookAt>().target = mocapHip.transform;*/
        }
    }

    public void JumpToCue(int cueToJumpTo){
				cueToRun = cueToJumpTo;
				GetComponent<AudioSource>().time = cueTimes[cueToJumpTo];
				audioControl.Play();
}







	void Update () {

		//if(cueToRun<=lastCueNumber && GetComponent<AudioSource>().time >= cueTimes[cueToRun] ){
		if(cueToRun<=lastCueNumber && timeline.time >= cueTimes[cueToRun] ){
			if (jumpToCue){
				cueToRun = jumpCueNumber;
				GetComponent<AudioSource>().time = cueTimes[jumpCueNumber];
				audioControl.Play();
				jumpToCue = false;
			}	

			uiText.text = "Scene Cue: " + cueToRun;

			if (cueToRun == lastCueNumber){
				danceMusic.DOFade(0.0f, audioFadeOutTime);
			}
			

			//	for (int l = 0; l<lightLights.Length; l++){
			//			lightLights[l].DOColor(light1Color[cueToRun], 5.0f);
			//		} 



			lightLights[0].DOColor(light1Color[cueToRun], lightFadeTime[cueToRun]);
			lightLights[1].DOColor(light2Color[cueToRun], lightFadeTime[cueToRun]);
			lightLights[2].DOColor(light3Color[cueToRun], lightFadeTime[cueToRun]);			
				
			theCamera.transform.DOMove(cameraLoc[cueToRun], cameraMoveTime[cueToRun]);

			lights[0].transform.DOMove(light1Loc[cueToRun], cameraMoveTime[cueToRun]);
			lights[1].transform.DOMove(light2Loc[cueToRun], cameraMoveTime[cueToRun]);
			lights[2].transform.DOMove(light3Loc[cueToRun], cameraMoveTime[cueToRun]);



		      if(!camerafollowing[cueToRun]){ 
		        theCamera.GetComponent<SmoothLookAt>().damping = 0;
		 		}
		 		else{
		 			theCamera.GetComponent<SmoothLookAt>().damping = 100;
		 		}



		 switch(cameraEffect[cueToRun]){
		 	 case CameraEffects.Bloom: 
		 	 	theCamera.GetComponent<BloomOptimized>().enabled = true;
		 	 	theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	theCamera.GetComponent<Glitch>().enabled = false;
		 	 	theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	theCamera.GetComponent<Pixelate>().enabled = false;
 			break;

 			 case CameraEffects.Glitch: 
 			  	theCamera.GetComponent<BloomOptimized>().enabled = false;
		 		theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	theCamera.GetComponent<Glitch>().enabled = true;
		 	 	theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	theCamera.GetComponent<Pixelate>().enabled = false;
 			break; 

 			case CameraEffects.WaveDistortion: 
 			 	theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	theCamera.GetComponent<WaveDistortion>().enabled = true;
		 	 	theCamera.GetComponent<Glitch>().enabled = false;
		 	 	theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	theCamera.GetComponent<Pixelate>().enabled = false;
 			break; 			
 			case CameraEffects.Kuwahara: 
 			 	theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	theCamera.GetComponent<Glitch>().enabled = false;
		 	 	theCamera.GetComponent<Kuwahara>().enabled = true;
		 	 	theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	theCamera.GetComponent<Pixelate>().enabled = false;
 			break; 			
 			case CameraEffects.AnalogTV: 
 			 	theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	theCamera.GetComponent<Glitch>().enabled = false;
		 	 	theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	theCamera.GetComponent<AnalogTV>().enabled = true;
		 	 	theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	theCamera.GetComponent<Pixelate>().enabled = false;
 			break; 			
 			case CameraEffects.LoFi: 
 			 	theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	theCamera.GetComponent<Glitch>().enabled = false;
		 	 	theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	theCamera.GetComponent<LoFiPalette>().enabled = true;
		 	 	theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	theCamera.GetComponent<Pixelate>().enabled = false;
 			break;		

 			case CameraEffects.Blur: 
 			 	theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	theCamera.GetComponent<Glitch>().enabled = false;
		 	 	theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	theCamera.GetComponent<GaussianBlur>().enabled = true;
		 	 	theCamera.GetComponent<Pixelate>().enabled = false;
 			break;	

 		
			case CameraEffects.Pixelate: 
 			 	theCamera.GetComponent<BloomOptimized>().enabled = false;
		 	 	theCamera.GetComponent<WaveDistortion>().enabled = false;
		 	 	theCamera.GetComponent<Glitch>().enabled = false;
		 	 	theCamera.GetComponent<Kuwahara>().enabled = false;
		 	 	theCamera.GetComponent<AnalogTV>().enabled = false;
		 	 	theCamera.GetComponent<LoFiPalette>().enabled = false;
		 	 	theCamera.GetComponent<GaussianBlur>().enabled = false;
		 	 	theCamera.GetComponent<Pixelate>().enabled = true;
 			break;

		 	 default:
		         Debug.Log("NOTHING");
		         break;
		 	}

		switch(floorTexture[cueToRun])
		 {
	     case FloorTex.Tiles: 
	    	 floor.GetComponent<Renderer>().material = floorTextures[0];
	    //	 Debug.Log("Floor Texture");
    	 break; 
    	 case FloorTex.Streaks: 
	    	 floor.GetComponent<Renderer>().material = floorTextures[1];
	   // 	 Debug.Log("Floor Texture");
    	 break; 
    	 case FloorTex.Circles: 
	    	 floor.GetComponent<Renderer>().material = floorTextures[2];
	    //	 Debug.Log("Floor Texture");
    	 break; 
    	 case FloorTex.Glitch: 
	     	floor.GetComponent<Renderer>().material = floorTextures[3];
	     //	Debug.Log("Floor Texture");
    	 break;
    	 default:
	       Debug.Log("NOTHING");
	        break;
		}

		 	if (cueToRun < lastCueNumber){
			cueToRun++;
		}
		}
	}
}


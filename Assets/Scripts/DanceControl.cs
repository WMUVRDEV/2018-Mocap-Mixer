using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]

public class DanceControl : MonoBehaviour {

	public enum Themes {IdentityTheft, TechAddiction, InvasionOfPrivacy, CommunityEmpowerment};
	public enum MotionClip {Motion1, Motion2, Motion3, Motion4}//, Motion5, Motion6}
	public enum BodyMats {Glass, Brick, Circuits, Lines, Metal, Glitch, White}

	public Themes Theme;
	public MotionClip[] MotionClips;
	public float[] ClipSpeeds;
	public float[] ClipOffsets;
	public BodyMats[] bodyMaterials;
	public float[] cueTimes;
	
	public KeyCode ExecuteKey;
	public bool jumpToCue = false;
	public int jumpCueNumber =0;

	public Color noChangeBelowThis;

	public AudioSource music;

	private int clipOne  = Animator.StringToHash("toFirst");
	private int clipTwo = Animator.StringToHash("toSecond");
	private int clipThree = Animator.StringToHash("toThird");
	private int clipFour  = Animator.StringToHash("toFourth");
	private int clipFive = Animator.StringToHash("toFifth");
	private int clipSix  = Animator.StringToHash("toSixth");


	int cSpeed = Animator.StringToHash("clipSpeed");
	int cOffset = Animator.StringToHash("clipOffset");

	Animator anim;

	public GameObject[] MocapFigure;
	public GameObject mocapParent;
	public GameObject mocapBody;
	public GameObject startLoc;

	string[] cues;
	public int cueToRun;
	public Material[] mats;

	public Text uiText;
	public Text pctText;

	public SceneControl sceneControl;


	// Use this for initialization
	void Start () {
		MocapFigure[0].SetActive(true);
		MocapFigure[1].SetActive(true);
		MocapFigure[2].SetActive(true);
		MocapFigure[3].SetActive(true);





		switch(Theme){
		case Themes.IdentityTheft:
		MocapFigure[1].SetActive(false);
		MocapFigure[2].SetActive(false);
		MocapFigure[3].SetActive(false);
		anim = MocapFigure[0].GetComponent<Animator>();
	//	Debug.Log("Surv");
		break;
		case Themes.TechAddiction:
		MocapFigure[0].SetActive(false);
		MocapFigure[2].SetActive(false);
		MocapFigure[3].SetActive(false);
		anim = MocapFigure[1].GetComponent<Animator>();
	//	Debug.Log("Glitch");
		break;
		case Themes.InvasionOfPrivacy:
		MocapFigure[0].SetActive(false);
		MocapFigure[1].SetActive(false);
		MocapFigure[3].SetActive(false);
		anim = MocapFigure[2].GetComponent<Animator>();
		//Debug.Log("Tech");
		break;case 
		Themes.CommunityEmpowerment:
		MocapFigure[0].SetActive(false);
		MocapFigure[1].SetActive(false);
		MocapFigure[2].SetActive(false);
		anim = MocapFigure[3].GetComponent<Animator>();
		//Debug.Log("Tech");
		break;
		default:
	    Debug.Log("NOTHING");
	    break;
		}
		
	 mocapBody = GameObject.FindWithTag("Figure");

	}
	
	// Update is called once per frame



	void Update () {


		if (Input.GetKeyDown(ExecuteKey)){
		//	if (jumpToCue){
		//		cueToRun = jumpCueNumber;
			//	jumpToCue = false;
		}

//		Debug.Log(music.time);

	//if(cueToRun<(cueTimes.Length) && music.time >= cueTimes[cueToRun]){
	if(cueToRun<=sceneControl.lastCueNumber && music.time >= cueTimes[cueToRun] ){

	if (jumpToCue){
				cueToRun = jumpCueNumber;
				jumpToCue = false;
			}	



			uiText.text = "Mocap Cue: " + cueToRun;

		 switch(MotionClips[cueToRun])
		 {
	     case MotionClip.Motion1: 
	     //   Debug.Log("Motion1");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0f, 0.0f);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0f, 0.0f);
			//mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
			anim.SetTrigger(clipOne);
	         break;
	     case MotionClip.Motion2:
	   //      Debug.Log("Motion2");
	         anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0f, 0.0f);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0f, 0.0f);
			//mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	         anim.SetTrigger(clipTwo);
	         break;
	     case MotionClip.Motion3:
	   //     Debug.Log("Motion3");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0f, 0.0f);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0f, 0.0f);
			//mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	        anim.SetTrigger(clipThree);
	         break;
	      case MotionClip.Motion4:
	      //  Debug.Log("Motion4");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0f, 0.0f);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0f, 0.0f);
		//	mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	        anim.SetTrigger(clipFour);
	         break;
/*	      case MotionClip.Motion5:
	        Debug.Log("Motion5");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0f, 0.0f);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0f, 0.0f);
		//	mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	        anim.SetTrigger(clipFive);
	         break;
	     case MotionClip.Motion6:
	        Debug.Log("Motion6");
	       anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0f, 0.0f);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0f, 0.0f);
		//	mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	        anim.SetTrigger(clipSix);
	         break;*/
	     default:
	         Debug.Log("NOTHING");
	         break;
		 }

		  switch(bodyMaterials[cueToRun])
		 {
	     case BodyMats.Glass: 
	     print (mocapBody.name);
	     mocapBody.GetComponent<Renderer>().material = mats[0];
    	 break; 
    	 case BodyMats.Brick: 
	     mocapBody.GetComponent<Renderer>().material = mats[1];
    	 break; 
    	 case BodyMats.Circuits: 
	     mocapBody.GetComponent<Renderer>().material = mats[2];
    	 break; 
    	 case BodyMats.Lines: 
	     mocapBody.GetComponent<Renderer>().material = mats[3];
    	 break;
    	 case BodyMats.Metal: 
	     mocapBody.GetComponent<Renderer>().material = mats[4];
    	 break;
    	 case BodyMats.Glitch: 
	     mocapBody.GetComponent<Renderer>().material = mats[5];
    	 break;
    	 case BodyMats.White: 
	     mocapBody.GetComponent<Renderer>().material = mats[6];
    	 break;
    	 default:
	       Debug.Log("NOTHING");
	        break;
		}

		
			cueToRun++;
		}
	
	}
}

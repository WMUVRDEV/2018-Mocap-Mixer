import UnityEngine.UI;

public var Theme : Themes;
public var ExecuteKey : KeyCode;
public var jumpToCue : boolean = false;
public var jumpCueNumber : int = 0;	
enum Themes {Survielance, Glitch, Dependence}
enum MotionClip {Motion1, Motion2, Motion3, Motion4, Motion5, Motion6}
enum BodyMats {Glass, Brick, Circuits, Lines, Metal, Glitch, White}


public var MotionClips : MotionClip[];
public var ClipSpeeds : float[];
public var ClipOffsets : float[];
//var reverseClip : boolean[];
public var bodyMaterials : BodyMats[];


public var noChangesBelowThis : Color;
//var bodyTextures : Texture2D[];


private var clipOne : int = Animator.StringToHash("toFirst");
private var clipTwo : int = Animator.StringToHash("toSecond");
private var clipThree : int = Animator.StringToHash("toThird");
private var clipFour : int = Animator.StringToHash("toFourth");
private var clipFive : int = Animator.StringToHash("toFifth");
private var clipSix : int = Animator.StringToHash("toSixth");

private var cSpeed = Animator.StringToHash("clipSpeed");
private var cOffset = Animator.StringToHash("clipOffset");
var anim : Animator;

var MocapFigure : GameObject[];
var mocapParent : GameObject;
var mocapBody : GameObject;
var startLoc : GameObject;


var cues : String[];
var cueToRun : int;
var mats : Material[];

var uiText : Text;
var pctText : Text;

function Start (){

	switch(Theme){
		case Themes.Survielance:
		MocapFigure[1].SetActive(false);
		MocapFigure[2].SetActive(false);
		anim = MocapFigure[0].GetComponent.<Animator>();
		break;
		case Themes.Glitch:
		MocapFigure[0].SetActive(false);
		MocapFigure[2].SetActive(false);
		anim = MocapFigure[1].GetComponent.<Animator>();
		break;
		case Themes.Dependence:
		MocapFigure[0].SetActive(false);
		MocapFigure[1].SetActive(false);
		anim = MocapFigure[2].GetComponent.<Animator>();
		break;
		default:
	    Debug.Log("NOTHING");
	    break;
		}
		
	 mocapBody = GameObject.FindWithTag("Figure");
}

function Update () {

		if (Input.GetKeyDown(ExecuteKey)){
			if (jumpToCue){
				cueToRun = jumpCueNumber;
				jumpToCue = false;
			}

			uiText.text = "Mocap Cue: " + cueToRun;

		 switch(MotionClips[cueToRun])
		 {
	     case MotionClip.Motion1: 
	        Debug.Log("Motion1");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
			//mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
			anim.SetTrigger(clipOne);
	         break;
	     case MotionClip.Motion2:
	         Debug.Log("Motion1");
	         anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
			//mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	         anim.SetTrigger(clipTwo);
	         break;
	     case MotionClip.Motion3:
	        Debug.Log("Motion1");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
			//mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	        anim.SetTrigger(clipThree);
	         break;
	      case MotionClip.Motion4:
	        Debug.Log("Motion1");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
		//	mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	        anim.SetTrigger(clipFour);
	         break;
	      case MotionClip.Motion5:
	        Debug.Log("Motion1");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
		//	mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	        anim.SetTrigger(clipFive);
	         break;
	     case MotionClip.Motion6:
	        Debug.Log("Motion1");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
		//	mocapBody.GetComponent.<Renderer>().material = bodyMaterials[cueToRun];
	        anim.SetTrigger(clipSix);
	         break;
	     default:
	         Debug.Log("NOTHING");
	         break;
		 }

		  switch(bodyMaterials[cueToRun])
		 {
	     case BodyMats.Glass: 
	     print (mocapBody.name);
	     mocapBody.GetComponent.<Renderer>().material = mats[0];
    	 break; 
    	 case BodyMats.Brick: 
	     mocapBody.GetComponent.<Renderer>().material = mats[1];
    	 break; 
    	 case BodyMats.Circuits: 
	     mocapBody.GetComponent.<Renderer>().material = mats[2];
    	 break; 
    	 case BodyMats.Lines: 
	     mocapBody.GetComponent.<Renderer>().material = mats[3];
    	 break;
    	 case BodyMats.Metal: 
	     mocapBody.GetComponent.<Renderer>().material = mats[4];
    	 break;
    	 case BodyMats.Glitch: 
	     mocapBody.GetComponent.<Renderer>().material = mats[5];
    	 break;
    	 case BodyMats.White: 
	     mocapBody.GetComponent.<Renderer>().material = mats[6];
    	 break;
    	 default:
	       Debug.Log("NOTHING");
	        break;
		}

		
			cueToRun++;
		}


}

var Theme : DanceTheme;
//var ExecuteKey : KeyCode;
enum DanceTheme {IdentityTheft, TechAddiction, InvasionOfPrivacy, CommunityEmpowerment}
enum ThemeMotionClip {Motion1, Motion2, Motion3, Motion4, Motion5, Motion6}


var MotionClips : ThemeMotionClip[];
var ClipSpeeds : float[];
var ClipOffsets : float[];

enum bMaterials {Glass, Brick, Circuits, Lines, Metal, Glitch, White}
enum floorTexers {Tiles, Streaks, Circles, Glitch}
var floorTexture : floorTexers;







var bodyMaterials : bMaterials[];
//var reverseClip : boolean[];
//var bodyTextures : Texture2D[];

var noChangesBelowThis : Color;

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
var mats : Material[];

var cues : String[];
var cueToRun : int;

var pctText : Text;
private var themeName : String;
private var clipNum : String;

var floorTextures : Material[] ;
var floor : GameObject;

//var survAnim : AnimatorController;
//var glitchAnim : AnimatorController;
//var dependAnim : AnimatorController;

function Start (){

		MocapFigure[0].SetActive(true);
		MocapFigure[1].SetActive(true);
		MocapFigure[2].SetActive(true);
		MocapFigure[3].SetActive(true);


	switch(Theme){
		case DanceTheme.IdentityTheft:
		MocapFigure[1].SetActive(false);// = false;
		MocapFigure[2].SetActive(false);// = false;
		MocapFigure[3].SetActive(false);// = false;
		anim = MocapFigure[0].GetComponent.<Animator>();
		themeName = "Identity Theft";
		break;
		case DanceTheme.TechAddiction:
		MocapFigure[0].SetActive(false);// = false;
		MocapFigure[2].SetActive(false);// = false;
		MocapFigure[3].SetActive(false);// = false;
		anim = MocapFigure[1].GetComponent.<Animator>();
		themeName = "Tech Addiction";
		break;
		case DanceTheme.InvasionOfPrivacy:
		MocapFigure[0].SetActive(false);// = false;
		MocapFigure[1].SetActive(false);// = false;
		MocapFigure[3].SetActive(false);// = false;
		anim = MocapFigure[2].GetComponent.<Animator>();
		themeName = "Invasion Of Privacy";
		break;
		case DanceTheme.CommunityEmpowerment:
		MocapFigure[0].SetActive(false);// = false;
		MocapFigure[1].SetActive(false);// = false;
		MocapFigure[2].SetActive(false);// = false;
		anim = MocapFigure[3].GetComponent.<Animator>();
		themeName = "Community Empowerment";
		break;
		default:
	    Debug.Log("NOTHING");
	    break;
		}
		
	
		switch(floorTexture)
		 {
	     case floorTexers.Tiles: 
	    	 floor.GetComponent.<Renderer>().material = floorTextures[0];
    	 break; 
    	 case floorTexers.Streaks: 
	    	 floor.GetComponent.<Renderer>().material = floorTextures[1];
    	 break; 
    	 case floorTexers.Circles: 
	    	 floor.GetComponent.<Renderer>().material = floorTextures[2];
    	 break; 
    	 case floorTexers.Glitch: 
	     	floor.GetComponent.<Renderer>().material = floorTextures[3];
    	 break;
    	 default:
	       Debug.Log("NOTHING");
	        break;
		}
	

		switch(MotionClips[cueToRun])
		 {
	     case ThemeMotionClip.Motion1: 
	        Debug.Log("Motion1");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
			anim.SetTrigger(clipOne);
			clipNum = "Motion 1";
	         break;
	     case ThemeMotionClip.Motion2:
	         Debug.Log("Motion2");
	         anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
	         anim.SetTrigger(clipTwo);
	         clipNum = "Motion 2";
	         break;
	     case ThemeMotionClip.Motion3:
	        Debug.Log("Motion3");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
	        anim.SetTrigger(clipThree);
	        clipNum = "Motion 3";
	         break;
	      case ThemeMotionClip.Motion4:
	        Debug.Log("Motion4");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
	        anim.SetTrigger(clipFour);
	        clipNum = "Motion 4";
	         break;
	      case ThemeMotionClip.Motion5:
	        Debug.Log("Motion5");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
	        anim.SetTrigger(clipFive);
	        clipNum = "Motion 5";
	         break;
	     case ThemeMotionClip.Motion6:
	        Debug.Log("Motion6");
	        anim.SetFloat(cSpeed, ClipSpeeds[cueToRun], 0.0, 0.0);
			anim.SetFloat(cOffset, ClipOffsets[cueToRun], 0.0, 0.0);
	        anim.SetTrigger(clipSix);
	        clipNum = "Motion 6";
	         break;
	     default:
	         Debug.Log("NOTHING");
	         break;
		 }

		var mocapBody = GameObject.FindWithTag("Figure");

	  switch(bodyMaterials[cueToRun])
		 {
	     case bMaterials.Glass: 
	     print (mocapBody.name);
	     mocapBody.GetComponent.<Renderer>().material = mats[0];
    	 break; 
    	 case bMaterials.Brick: 
	     mocapBody.GetComponent.<Renderer>().material = mats[1];
    	 break; 
    	 case bMaterials.Circuits: 
	     mocapBody.GetComponent.<Renderer>().material = mats[2];
    	 break; 
    	 case bMaterials.Lines: 
	     mocapBody.GetComponent.<Renderer>().material = mats[3];
    	 break;
    	 case bMaterials.Metal: 
	     mocapBody.GetComponent.<Renderer>().material = mats[4];
    	 break;
    	 case bMaterials.Glitch: 
	     mocapBody.GetComponent.<Renderer>().material = mats[5];
    	 break;
    	 case bMaterials.White: 
	     mocapBody.GetComponent.<Renderer>().material = mats[6];
    	 break;
    	 default:
	       Debug.Log("NOTHING");
	        break;
		}
}

function Update(){
	//var clipInfo : AnimatorClipInfo[]  = anim.GetCurrentAnimatorClipInfo(0);

			//animation["anim"].time/animation["anim"].length

			var clipInfo : AnimatorStateInfo  = anim.GetCurrentAnimatorStateInfo(0);
			//AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(animationLayer);
 
 			var playbackTime = clipInfo.normalizedTime + ClipOffsets[0]; // 
 			//float playbackTime = clipInfo.normalizedTime;

			pctText.text = themeName + " " + clipNum +  ":   Clip Offset: " + playbackTime.ToString("F3");
}
 // Author:
 // Stefan Guntermann apocx@gmx.de 
 // 
 // Description: 
 // Attach Label Element to a GameObject. 
 // Use: 
 // gameObject.BroadcastMessage("setText", "foo"); 
 // From: Parent GameObject
 
 using UnityEngine; 
 using UnityEditor; 
 using System.Collections;
 
 [ExecuteInEditMode()] 
 public class TargetDancerSceneView : MonoBehaviour {
     public bool followDancer = false;
 
     public Transform focusObject;

   void Start()
    {
        this.enabled = false;
    }



    void Update () {

        Debug.Log("Light");
 
         if (followDancer) {

             if( SceneView.lastActiveSceneView != null ){
                 transform.LookAt(focusObject);
             }
         }
     }


}
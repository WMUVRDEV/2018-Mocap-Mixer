using UnityEngine;

public class LookAverage : MonoBehaviour
{

    public Transform Dancer1;
    public Transform Dancer2;

    void Update (){
        transform.position = (Dancer1.position + Dancer2.position) / 2;

        
        //transform.position.x = (Dancer1.position.x + Dancer2.position.x) / 2;
       // transform.position.y = (Dancer1.position.y + Dancer2.position.y) / 2;
        //transform.position.z = (Dancer1.position.z + Dancer2.position.z) / 2;
 }


}
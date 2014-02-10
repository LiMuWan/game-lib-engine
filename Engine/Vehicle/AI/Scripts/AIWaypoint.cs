using UnityEngine;
using System.Collections;

public class AIWaypoint : MonoBehaviour {

    public int speed = 100;
    public bool useTrigger = false;
    
    void Awake() {
        //nur zum testen:
        renderer.enabled = false;
        if (useTrigger) {
            //2012-08-05 -B
            //BoxCollider bc = gameObject.AddComponent<BoxCollider>();  
            //bc.isTrigger = true;
            //gameObject.layer = 2;
            
            BoxCollider bcTest = gameObject.GetComponent<BoxCollider>();
            if (bcTest == null) {
                BoxCollider bc = gameObject.AddComponent<BoxCollider>();    
                bc.isTrigger = true;
                gameObject.layer = 2;
            }
            else {
                bcTest.isTrigger = true;
                gameObject.layer = 2;
            }  
            //2012-08-05 -E
        }
        
    }
    
    void OnTriggerEnter(Collider other) {
        
        if (useTrigger) {
            
            AIWaypointEditor aiWaypointEditor;      
            aiWaypointEditor = other.gameObject.transform.root.gameObject.GetComponentInChildren<AIWaypointEditor>();
            
            if (aiWaypointEditor != null) {
                
                if (aiWaypointEditor.folderName == gameObject.transform.parent.name) {
                    
                    AIDriverController aIDriverController = other.gameObject.transform.root.gameObject.GetComponentInChildren<AIDriverController>();
                    
                    if (aIDriverController != null) {
                        if (aIDriverController.waypoints.Count > aIDriverController.currentWaypoint) { //2011-12-26                         
                            if (aIDriverController.waypoints[aIDriverController.currentWaypoint].gameObject.name == gameObject.name) {
                                aIDriverController.NextWaypoint();
                                
                            }
                        }
                        
                    }
                    
                }
                   
            }
            
        }
        
    }
    
}

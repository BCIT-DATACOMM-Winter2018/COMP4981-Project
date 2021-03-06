using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	TowerShotAbility - A script to provide the logic for
///                            the TowerShot ability.
/// 
/// PROGRAM: NetworkLibrary
///
/// FUNCTIONS:	void Start()
///				void Update()
///             void OnCollisionEnter()
///
/// DATE: 		April 5th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Cameron Roberts
///
/// PROGRAMMER: Cameron Roberts
///
/// NOTES:		
/// ----------------------------------------------
public class TowerShotAbility : Ability
{

    public float speed = 60f;
    [HideInInspector]
    public GameObject target;

    /// ----------------------------------------------
    /// FUNCTION:	Start
    /// 
    /// DATE:		April 5th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void Start()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function.
    ///             Called before the first Update().
    /// ----------------------------------------------
    void Start ()
    {

    }

    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		April 5th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void Update()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function. Called at a fixed interval.
    ///             Check how far the GameObject has moved from its
    ///             starting point and delete it if it has gone too far.
    /// ----------------------------------------------
    void Update(){
        if(target!= null){
            Vector3 targetLocation = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        } else{
            Destroy(gameObject);
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	OnTriggerEnter
    /// 
    /// DATE:		April 5th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void OnCollisionEnter(Collider col)
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		When the GameObject collides with the target send
    ///             a collision to the server.
    /// ----------------------------------------------
    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject != target){
            Physics.IgnoreCollision(GetComponent<Collider>(), col);
        } else{
            SendCollision(col.gameObject.GetComponent<Actor>().ActorId);
            Destroy(gameObject);
        }
    }
}
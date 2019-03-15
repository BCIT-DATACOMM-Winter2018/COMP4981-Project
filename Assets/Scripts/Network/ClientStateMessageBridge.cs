using System;
using NetworkLibrary;
using UnityEngine;
using System.Collections.Generic;

/// ----------------------------------------------
/// Interface: 	ClientStateMessageBridge - An class to allow message elements
/// 								  	   to update the game state.
/// 
/// PROGRAM: SKOM
///
/// CONSTRUCTORS:	public ClientStateMessageBridge (GameObjectController objectController)
/// 
/// FUNCTIONS:	TBD
/// 
/// DATE: 		March 14th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Cameron Roberts
///
/// PROGRAMMER: Cameron Roberts
///
/// NOTES:		The purpose of this class is to provide an interface through
///				which the server can alter the game state.
/// ----------------------------------------------
public class ClientStateMessageBridge : IStateMessageBridge
{
	private GameObjectController objectController;
	private const float POSITION_TOLERANCE = 8f;

	/// ----------------------------------------------
	/// CONSTRUCTOR: ClientStateMessageBridge
	/// 
	/// DATE: 		March 14th, 2019
	/// 
	/// REVISIONS:	
	/// 
	/// DESIGNER:	Cameron Roberts
	/// 
	/// PROGRAMMER:	Cameron Roberts
	/// 
	/// INTERFACE: 	public ClientStateMessageBridge (GameObjectController objectController)
	///					GameObjectController objectController: A GameObjectController through
	///										which the bridge can create and modify GameObjects.
	/// 
	/// NOTES:		
	/// ----------------------------------------------
	public ClientStateMessageBridge (GameObjectController objectController)
	{
		this.objectController = objectController;
	}

	/// ----------------------------------------------
    /// FUNCTION:	UpdateActorPosition
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	public void UpdateActorPosition (int actorId, float x, float z)
	///					int actorId: The actor id of the GameObject to change the position of
	///					float x: The x position to move the actor to
	///					float z: The z position to move the actor to
    /// 
    /// NOTES:		A function to change the position of an actor
    /// ----------------------------------------------
	public void UpdateActorPosition (int actorId, float x, float z){
		try{
			GameObject actor = objectController.GameActors[actorId];
			Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
			actor.transform.position = targetPosition;
		} catch	(KeyNotFoundException e){

		}
	}

	/// ----------------------------------------------
    /// FUNCTION:	UpdateActorHealth
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 		public void UpdateActorHealth (int actorId, int newHealth)
	///					int actorId: The actor id of the GameObject to change the health of
	///					int newHealth: The actors new health value
    /// 
    /// NOTES:		A function to change the health of an actor
    /// ----------------------------------------------
	public void UpdateActorHealth (int actorId, int newHealth){
		try{
			objectController.GameActors[actorId].GetComponent<Actor>().Status.HP = newHealth;
			Debug.Log("Update health of actor " + actorId + " to " +newHealth);

		} catch	(KeyNotFoundException e){

		}
	}

	/// ----------------------------------------------
    /// FUNCTION:	UseTargetAbility
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	
    /// 
    /// INTERFACE: 	UseTargetedAbility (int actorId, AbilityType abilityId, int targetId)
	///					int actorId: The actor id of the GameObject to instruct to use an ability
	///					AbiltyType abilityId: The id of the ability to use
	///					int targetId: The actor to use the ability on
    /// 
    /// NOTES:		A function to instruct an actor to use an ability on another actor.
    /// ----------------------------------------------
	public void UseTargetedAbility (int actorId, AbilityType abilityId, int targetId){
		//TODO Implement this function
	}

	/// ----------------------------------------------
    /// FUNCTION:	UseAreaAbility
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Simon Wu
    /// 
    /// INTERFACE: 	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z)
	///					int actorId: The actor id of the GameObject to instruct to use an ability
	///					AbiltyType abilityId: The id of the ability to use
	///					float x: The x position to use the ability on
	///					float z: The z position to use the ability on
    /// 
    /// NOTES:		A function to instruct an actor to use an ability on a location.
    /// ----------------------------------------------
	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z){
        objectController.GameActors[actorId].GetComponent<AbilityController>().UseAbility(abilityId, x, z);
    }

	/// ----------------------------------------------
    /// FUNCTION:	ProcessCollision
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	
    /// 
    /// INTERFACE: 	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId)
    /// 
    /// NOTES:		Function not intended to be used on the client.
    /// ----------------------------------------------
	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId){

	}

	/// ----------------------------------------------
    /// FUNCTION:	SpawnActor
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	public void SpawnActor(ActorType actorType, int ActorId, float x, float z)
	///					ActorType actorType: The type of actor to spawn
	///					int actorId: An id to assign the newly created actor
	///					float x: The x position to spawn the actor in
	///					float z: The z position to spawn the actor in
    /// 
    /// NOTES:		A function to spawn new actors
    /// ----------------------------------------------
	public void SpawnActor(ActorType actorType, int ActorId, float x, float z){
		Debug.Log("Spawn actor " + ActorId + " " + actorType);
		if(actorType == ActorType.AlliedPlayer && ActorId == ConnectionManager.Instance.ClientId){
			objectController.InstantiateObject(ActorType.Player, new Vector3(x,0,z), ActorId);
		} else {
			objectController.InstantiateObject(actorType, new Vector3(x,0,z), ActorId);
		}
	}

	/// ----------------------------------------------
    /// FUNCTION:	SetActorMovement
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	public void SpawnActor(ActorType actorType, int ActorId, float x, float z)
	///					int actorId: An id to assign the newly created actor
	///					float x: The x position the actor should currently be at
	///					float z: The z position the actor should currently be at
	///					float targetX: The x position the actor is moving to
	///					float targetZ: The z position the actor is moving to
    /// 
    /// NOTES:		A function to control the movement of actors based on their current
	///				and target position. Behaviour differs if the actor is the player character.
    /// ----------------------------------------------
	public void SetActorMovement(int actorId, float x, float z, float targetX, float targetZ){
		Debug.Log("Setting actor movement of " + actorId + " position + "+ x+","+z +" target"+targetX+","+targetZ);

		if(actorId == ConnectionManager.Instance.ClientId){
			try {	
				GameObject actor = objectController.GameActors[actorId];
				if(Math.Abs(actor.transform.position.x - x) > POSITION_TOLERANCE || Math.Abs(actor.transform.position.z - z) > POSITION_TOLERANCE){
					Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
					actor.transform.position = targetPosition;
				}
			} catch	(KeyNotFoundException e){
				//TODO Error handling
			}
		} else {
			try {	
				GameObject actor = objectController.GameActors[actorId];
				if(Math.Abs(actor.transform.position.x - x) > POSITION_TOLERANCE || Math.Abs(actor.transform.position.z - z) > POSITION_TOLERANCE){
					Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
					actor.transform.position = targetPosition;
				}
				actor.GetComponent<ActorMovement>().SetTargetPosition(new Vector3(x, 0, z));
			} catch	(KeyNotFoundException e){
				//TODO Error handling
			}
		}
		
	}

	/// ----------------------------------------------
    /// FUNCTION:	SetReady
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	
    /// 
    /// INTERFACE: 	public void SetReady(int clientId, bool ready)
    /// 
    /// NOTES:		Function not intended to be used on the client.
    /// ----------------------------------------------
	public void SetReady(int clientId, bool ready){

	}

	/// ----------------------------------------------
    /// FUNCTION:	SetActorMovement
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	public void StartGame(int playerNum)

    /// 
    /// NOTES:		Starts the game and sets the number of players in the game
	///				Full implementation waiting on completed lobby state
    /// ----------------------------------------------
	public void StartGame(int playerNum){
		Debug.Log("StartGame");
		ConnectionManager.Instance.PlayerNum = playerNum;
		ConnectionManager.Instance.gameStarted = true;
	}

}


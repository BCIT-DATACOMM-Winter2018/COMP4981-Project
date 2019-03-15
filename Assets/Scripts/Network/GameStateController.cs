﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;
public class GameStateController : MonoBehaviour
{
    private GameObjectController objectController;
    private ClientStateMessageBridge stateBridge;
    private ConcurrentQueue<UpdateElement> elementQueue;

    /// ----------------------------------------------
    /// FUNCTION:	Start
    /// 
    /// DATE:		March 14th, 2019
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
    void Start()
    {
        elementQueue = ConnectionManager.Instance.MessageQueue;
        objectController = GetComponent<GameObjectController>();
        stateBridge = new ClientStateMessageBridge(objectController);
    }

    /// ----------------------------------------------
    /// FUNCTION:	FixedUpdate
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void FixedUpdate()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function. Called at a fixed interval.
    ///             Dequeues UpdateElements and calls their UpdateState function.
    /// ----------------------------------------------
    void FixedUpdate()
    {
        UpdateElement updateElement;
        while(elementQueue.TryDequeue(out updateElement)){
            updateElement.UpdateState(stateBridge);
        }
    }
}
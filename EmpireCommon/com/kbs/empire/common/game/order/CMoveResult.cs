////////////////////////////////////////////////////////////////////////////////////////
//This Source Code File Is Part Of The 
//Empire Common DLL Assembly for 
//Empire Deluxe Combined Edition
//
//Copyright 2017 Mark Kinkead
//All rights reserved
//
//This is part of code included in the game
//you are not able to edit this code,
//but you should build the assembly and use it 
//as the API for World Building and AI Player
//Interfacing to the game with your custom code.
//
//Version Release Information Available
//In the file
//empire/version/Version.cs
//
//
////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using com.kbs.empire.ai.common.cevent;

namespace com.kbs.empire.common.game.order
{
    //returning value form a movement command
    //See COrder Constants
    public class CMoveResult
    {
        //The Result
        public readonly string result_;
        //Error Further Explained (or null)
        public readonly string error_;
        //Resulting Events
        public readonly Queue<CGameEvent> events_ = new Queue<CGameEvent>();

        public CMoveResult(string result, string error)
        {
            result_ = result;
            error_ = error;
        }
    }
}

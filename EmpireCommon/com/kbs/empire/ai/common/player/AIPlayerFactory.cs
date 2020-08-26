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
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.ai.common.player
{
    //Player Factory Class - All player factories must be dreived from this
    public abstract class AIPlayerFactory
    {
        //Send back the keys of the sets you like
        public abstract List<string> bestBuildSets();
        //Send back the hints your player class would like to see
        public abstract CDLLHints getHints();

        //call by the game to have you create a player
        public abstract AIPlayer createAIPlayer(int position, string logpath, string logname, CDLLHints hints, AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel);
        //call by the game to have you reload a player
        public abstract AIPlayer reloadAIPlayer(int position, CEncodedObjectInputBufferI bin, string logpath, string logname, AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel);

    }
}

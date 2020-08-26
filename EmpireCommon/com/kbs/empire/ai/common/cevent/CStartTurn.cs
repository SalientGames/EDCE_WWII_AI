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
using com.kbs.empire.common.game.treaty;
using com.kbs.empire.common.game.unit;

namespace com.kbs.empire.ai.common.cevent
{
    public class CStartTurn : CGameEvent
    {
        //Turn to start
        public int turn_;
        //new units
        public List<CUnit> unitAdds_ = null;
        //unit updates
        public List<CUpdate> updates_ = null;

        ////////////////////////////////////////////////////////
        //Production Info
        //production reports for the current turn (null if none)
        public List<CProductionReportData> prodData_ = null; 
        //These Are Treaty Propoals for this turn
        public List<CProposal> proposals_ = null; 
        ////////////////////////////////////////////////////////
        

        public CStartTurn(uint id, string type) : base(id, type){}


    }
}

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
using EmpireCommon.com.kbs.empire.common.game.rules;
using com.kbs.empire.ai.common.map;
using com.kbs.empire.common.game.player;
using com.kbs.empire.common.game.rules;
using com.kbs.empire.common.game.supply;
using com.kbs.empire.common.game.treaty;
using com.kbs.empire.common.game.unit;

namespace com.kbs.empire.ai.common.cevent
{
    //One time event at the start of a game
    public class CStateEvent : CGameEvent
    {
        public List<CPlayer> players_ = null;

        //just the basic map facts
        //width
        public int w_;
        //horz wrap
        public bool hw_;
        //height
        public int h_;
        //vert wrap
        public bool vw_;

        //Set of rules
        public CGameRules gameRules_;
        //Set of Victory Conditions
        public CVictoryConditions vc_;

        //flyover gid, x, y, 
        public uint flyoverGid_ = CUnitConstants.NOUNIT;

        //Visible Map info
        public List<CMapLocInfo> exploredLocs_ = null;

        //Player's Cities
        public List<CProducerUnit> cities_ = null;
        //PLayers Non-City Producers (like Oilfields)
        public List<CProducerUnit> prods_ = null;
        //City's Units (will include prods
        public List<CUnit> units_ = null;
        //Enemy Cities and Units
        public List<CUnit> spots_ = null;
        //Player's Supply routes
        public List<CSupplyRoute> routes_ = null;
        
        //Current Game Turn
        public int curturn_;

        //Units before drain issues
        public int unitsBeforeDrain_ = 0;
        //Drain Weight. Values over 100 cause drain
        public int rdWeight_ = 0;
        //These Are Gift Proposals for this turn (if current turn)
        public List<CProposal> proposals_ = null; 
        //Production Reports for the past turn (if any or null)
        public List<CProductionReportData> prodData_ = null; 

        public CStateEvent(uint id, string type) : base(id, type)
        {
            
        }
    }
}

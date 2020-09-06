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
    /// <summary>
    /// One time event at the start of a game
    /// </summary>
    public class CStateEvent : CGameEvent
    {
        public List<CPlayer> players_ = null;

        //just the basic map facts

        /// <summary>
        /// width
        /// </summary>
        public int w_;
        /// <summary>
        /// horz wrap
        /// </summary>
        public bool hw_;
        /// <summary>
        /// height
        /// </summary>
        public int h_;
        /// <summary>
        /// vert wrap
        /// </summary>
        public bool vw_;

        /// <summary>
        /// Set of rules
        /// </summary>
        public CGameRules gameRules_;
        /// <summary>
        /// Set of Victory Conditions
        /// </summary>
        public CVictoryConditions vc_;

        /// <summary>
        /// flyover gid, x, y, 
        /// </summary>
        public uint flyoverGid_ = CUnitConstants.NOUNIT;

        /// <summary>
        /// Visible Map info
        /// </summary>
        public List<CMapLocInfo> exploredLocs_ = null;

        /// <summary>
        /// Player's Cities
        /// </summary>
        public List<CProducerUnit> cities_ = null;
        /// <summary>
        /// PLayers Non-City Producers (like Oilfields)
        /// </summary>
        public List<CProducerUnit> prods_ = null;
        /// <summary>
        /// City's Units (will include prods
        /// </summary>
        public List<CUnit> units_ = null;
        /// <summary>
        /// Enemy Cities and Units
        /// </summary>
        public List<CUnit> spots_ = null;
        /// <summary>
        /// Player's Supply routes
        /// </summary>
        public List<CSupplyRoute> routes_ = null;

        /// <summary>
        /// Current Game Turn
        /// </summary>
        public int curturn_;

        /// <summary>
        /// Units before drain issues
        /// </summary>
        public int unitsBeforeDrain_ = 0;
        /// <summary>
        /// Drain Weight. Values over 100 cause drain
        /// </summary>
        public int rdWeight_ = 0;
        /// <summary>
        /// These Are Gift Proposals for this turn (if current turn)
        /// </summary>
        public List<CProposal> proposals_ = null;
        /// <summary>
        /// Production Reports for the past turn (if any or null)
        /// </summary>
        public List<CProductionReportData> prodData_ = null; 

        public CStateEvent(uint id, string type) : base(id, type)
        {
            
        }
    }
}

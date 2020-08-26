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
namespace com.kbs.empire.ai.common.cevent
{
    //
    //This file is home for event type constants
    //
    public class CEventConstants
    {
        //EVENT TYPES                                   //FIELDS USED in CGameEvent Object
        //state                                         
        public const string  GAMSESTATE = "GSE";        //CGameState Event

        //turn
        public const string  ENDTURN = "ETE";           //...
        public const string  TURNCHANGE = "TCE";        //value 1 = player position, value 2 = turn
        public const string  STARTTURNEVENT = "STE";    //CStartTurn Event
        public const string  CONTINUETURNEVENT = "CTE"; //..

        public const string  STOPTURNEVENT = "STOPE";   //..

        //data updates
        public const string  PLAYERNAMECHANGE = "PNC";  //value1 = player position, info1 = new name
        public const string  DRAIN_EVENT = "DRE";       //value1 units before drain, value2 drain weight
        public const string  UNITUPDATE = "UUE";        //CUpdate Event
        public const string  DEATHEVENT = "DOE";        //gid = dead unit, info1 reason
        public const string  FLYOVEREVENT = "FOE";      //flag = set flyover, gid = unit id

        //map discovery
        public const string  MAPLOCATION = "MLE";       //CMapLocInf

        //unit disovery
        public const string  NEWUNIT = "NUE";           //CUnit
        public const string  SPOTTER = "SPRE";          //gid unit that sees something, info1 what they saw

        //victory and defeat
        public const string  PLAYERDEAD = "PDE";        //value1 is position, value2 numeric reason, info1 = reason
        public const string  PLAYERWIN = "PWE";         //value 1 is the position that won, info1 is the win type


        //client side only event
        public const string  END_OF_EVENTS = "EOE";     //..  there won't be any more events, game has halted

        //supply
        public const string  REMOVE_SUPPLY_EVENT = "RSUE"; //gid = route id to be removed
        public const string  SUPPLY_EVENT = "SUPPE";       //CSupplyRoute  Adds or Updates

        //chat
        public const string CHAT_EVENT = "CHATE";           //Chat event, info1 = name of chatter, info2 = message
        //score
        public const string SCORE_EVENT = "CSCORE";         //a score: value1 = player, value2 = their current score
        //buypoints
        public const string PLY_BUYPOINT_EVENT = "BPE";     //change in the players buypoint totals. 
                                                            //value1 = buypoints available, value2 = buypoints spent
    }
}

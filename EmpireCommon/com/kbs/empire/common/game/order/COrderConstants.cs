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
namespace com.kbs.empire.common.game.order
{
    public class COrderConstants
    {
        //////////////////////////////////////////////
        //Orders with No Extra Data Required

        //No Order
        public const string ORD_NO_ORDER = "NO";
        //Sentry
        public const string ORD_SENTRY = "SN";
        //Sleep
        public const string ORD_SLEEP = "SL";
        //Full Rest
        public const string ORD_FULL_REST = "FR";
        //Dig In or Out
        public const string ORD_DIG_IN_OUT = "DIO";
        //Load All Attempts
        public const string ORD_LOAD_ALL = "LA";
        //Load Till Full
        public const string ORD_LOAD_TILL_FULL = "LF";
        //Explore
        public const string ORD_EXPLORE = "EX";
        //Find Nearest Home/Host
        public const string ORD_GO_HOME = "HM";
        //Drop Unit (can mean drop self or 1st child unit)
        public const string ORD_DROP_UNIT = "DU";
        //Land Unit
        public const string ORD_LAND_UNIT = "LU";
        //Take Off
        public const string ORD_TAKEOFF_UNIT = "TO";
        //Launch
        public const string ORD_LAUNCH = "LCH";
        //Perform Re-Entry
        public const string ORD_REENTRY = "REE";
        //Detonate
        public const string ORD_DETONATE = "DET";
        //Find A host to board (while inside a city)
        public const string ORD_SINGLE_LOAD = "SLD";
        //Exit host while remaining in a city
        public const string ORD_SINGLE_UNLOAD = "SULD";
        //Load Available units while in a city/base
        public const string ORD_HOST_LOAD = "HLD";
        //Load Available units while in a city/base
        public const string ORD_HOST_UNLOAD = "HULD";

        //////////////////////////////////////////////
        //Requries Int Value
        //Move in Specified Direction
        public const string ORD_MOVE_DIR = "MD";

        //////////////////////////////////////////////
        //Requries Location
        //Move To Location
        public const string ORD_MOVE_TO = "M2";
        public const string ORD_MOVE_INTO = "MI2";
        public const string ORD_MOVE_TO_SENTRY = "M2S";
        //Patrol Around Location
        public const string ORD_PATROL_LOC = "PL";
        //Range Fire In Loc
        public const string ORD_RANGE_FIRE = "RF";

        //////////////////////////////////////////////
        //requires A List of Locs

        //Patrol On Loc List
        public const string ORD_PATROL = "PA";
        //////////////////////////////////////////////
        //requires A List of Locs
        //uses utype as filter  ("" == all, or colon delimited with TYPE_DELIM-> "TYPEA:TYPEB")
        public const string ORD_CONVOY = "CVY";
        
        //////////////////////////////////////////////
        //Requires Unit Id
        //Escort Unit
        public const string ORD_ESCORT = "ES";
        //Load Switch - in the same square, leave current host 
        //(while inside a city)
        public const string ORD_LOAD_SWITCH = "LSO";
        //////////////////////////////////////////////
        //Requires String
        //Construct a Type
        public const string ORD_CONSTRUCT = "CS";
        //Morph Unit
        public const string ORD_MORPH = "MO";



        //////////////////////////////////////////////
        //Notifications
        public const string NOTE_UNIT_SWITCH = "DSD";
        public const string NOTE_PUSH_TO_FRONT = "PFH";
        public const string NOTE_PUSH_TO_BEHIND_UNIT = "PBU";
        public const string NOTE_PLAYER_NAME_CHANGE = "PNC";

        public const string NOTE_ORDER_CLEAR = "OC";
        public const string NOTE_SENTRY = "OSE";
        public const string NOTE_SLEEP = "OSL";
        public const string NOTE_NAMECHANGE = "NC";
        public const string NOTE_BUYPOINTS_DONE = "BPD";
        public const string NOTE_REMOVE_TREATY = "REMT";
        //////////////////////////////////////////////
        //Move Results        
        public const string MV_RAN_OUT_OF_POINTS = "ROP";
        public const string MV_REACHED_DESIRED_LOC = "RDL";
        public const string MV_NO_PATH = "NP";
        public const string MV_PRE_TERMINATED = "PRE";
        public const string MV_COMBAT_OCCURED = "CBT";
        public const string MV_ENEMY_DISCOVERY = "ED";
        public const string MV_UNIT_DIED = "UD";
        public const string MV_UNIT_DIED_ATTACKING = "UDA";
        public const string MV_UNIT_MORPHED = "UM";
        public const string MV_NOT_ENUFF_POINTS = "NEP";
        public const string MV_TARGET_LOC_IS_FULL = "LIF";
        public const string MV_MOVING_OUT_OF_RANGE = "MOR";
        public const string MV_ENEMY_NEXT = "EN";
        public const string MV_ENEMY_CITY_NEXT = "ECN";
        public const string MV_NEUTRAL_NEXT = "NN";
        public const string MV_NEUTRAL_CITY_NEXT = "NCN";
        public const string MV_RAN_OUT_OF_POINTS_ENEMY_DISCOVERY = "ROPED";
        public const string MV_REACHED_DESIRED_LOC_ENEMY_DISCOVERY = "RDLED";

        public const string MV_SUCCESS = "SS";
        public const string MV_FAILURE = "FA";

        public const char TYPE_DELIM = ':';

    }
}

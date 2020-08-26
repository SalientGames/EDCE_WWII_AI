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
namespace com.kbs.empire.common.game.unit
{

    public class CUnitConstants
    {
        //////////////////////////////
        //Unit Levels
        public const string LVL_SUB = "U";
        public const string LVL_GROUND = "L";
        public const string LVL_ORB = "O";

        //////////////////////////////
        //Land Sea Air Designations
        public const string LSA_SUB = "U";
        public const string LSA_SEA = "S";
        public const string LSA_LAND = "L";
        public const string LSA_AIR = "A";
        public const string LSA_ORB = "O";


        //////////////////////////////
        //Range Fire Type
        public const string RF_NONE = "N";
        public const string RF_LAND_SEA_BASED = "L";
        public const string RF_AIR_BASED = "A";
        public const string RF_ORB_BASED = "O";

        //////////////////////////////
        //Player Death Reasons
        public const string DR_NOT_DEAD_YET = "N"; //not dead
        public const string DR_RESIGNED = "R"; //resigned
        public const string DR_CANT_FIGHT = "F"; //no fighting power
        public const string DR_SMELLS_BAD = "S"; //dead for other reasons

        //////////////////////////////
        //Player Win Reasons
        public const string WR_SOLO = "SOLO"; //solitaire victory
        public const string WR_ANNIHILATION = "ANNI";//wiped out everyone else

        //////////////////////////////
        //Unit Death Reasons
        public const string DU_DEATH_DISBAND = "DDB";           //disbanded
        public const string DU_DEATH_KILLED_ATTACKING = "DKA";  //while attacking the enemy
        public const string DU_DEATH_KILLED_DEFENDING = "DKD";  //while defending against an attack
        public const string DU_DEATH_KILLED_BOMB = "DKB";       //destroyed due to bombing
        public const string DU_DEATH_RANGE = "DRG";             //ran out of fuel and crashed
        public const string DU_DEATH_COLLATERAL = "DCL";        //collateral damage (in a host that died/was captured)
        public const string DU_DEATH_CONSUMED = "DCS";          //used to build another unit
        public const string DU_DEATH_SIGHTING_REMOVED = "DSR";  //sighting removed
        public const string DU_DEATH_CAPTURED = "DCP";          //was captured
        public const string DU_DEATH_NEUTRALIZED = "DNZ";       //was neutralized
        public const string DU_DEATH_MORPHED = "DMD";           //has changed
        public const string DU_DEATH_DETONATED = "DDT";         //has detonated
        public const string DU_DEATH_KILLED_NUKE = "DNK";       //died in the apocalypse
        public const string DU_DEATH_BUYPOINTS = "DBP";         //unit removed during buypoints
        public const string DU_DEATH_RESOURCES = "DRS";         //unit ran out of resources
        //////////////////////////////
        //City Combat Type
        public const string CC_BOMB = "B";/* may bomb cities                              */
        public const string CC_CAPT = "C";/* may capture a city                           */
        public const string CC_CAPT_PP = "P";/* when city is captured, fast prod on infantry */
        public const string CC_CAPT_ND = "X";/* when city is captured, unit gets hosted */
        public const string CC_NONE = "N";
        //////////////////////////////
        //Readiness Codes
        public const string RD_EXHAUSTED = "EX";
        public const string RD_TIRED = "TR";
        public const string RD_USED = "US";
        public const string RD_READY = "RY";
        public const string RD_RESTED = "RS";
        public const string RD_FRESH = "FR";
        //////////////////////////////
        //Experience Levels
        public const string UE_GREEN = "GR";
        public const string UE_PROVEN = "PR";
        public const string UE_HARDENED = "HR";
        //////////////////////////////
        //No Unit Id
        public const uint NOUNIT = 0u;
        //Capacity
        public const int UNLIMITED_CAPACITY = int.MaxValue;
        public const int NO_CAPACITY = 0;


        //////////////////////////////
        //Updates
        public const string NAME = "NM";        //update unoit name
        public const string TURN = "TN";        //update unit's turn seen
        public const string HOST = "HT";        //change unit's host
        public const string RMVR = "RR";        //change units range remaining
        public const string RMVS = "RS";        //change unit's movement  points
        public const string FIRED = "FD";       //has unit fired a range fire
        public const string ARMED = "AD";       //is missile armed
        public const string DUGIN = "DI";       //is unit dug in
        public const string DMG = "DM";         //change unit's hit points
        public const string SHORT_FUEL = "SF";  //is unit short on fuel
        public const string READINESS = "RD";   //readiness code for the unit
        public const string EXPERIENCE = "EX";  //experience points for unit
        public const string EXP_LEVEL = "EL";   //experience code for unit
        public const string CHILD_ADD = "CDA";       //unit's children have changed (one is added)
        public const string CHILD_REM = "CDR";       //unit's children have changed (one is removed)

        public const string LX = "LX";          //location x value
        public const string LY = "LY";          //location y value
        public const string OWNER = "OW";       //owner positon
        public const string LANDED = "LN";      //unit is landed
        public const string REENTRY = "RE";     //unit in reentry
        public const string LEVEL = "LV";       //unit's level (Orb/Air/Ground/Sub)
        public const string SLAYER = "SL";      //unit's layer (0... or 1 for land/sea/air layer)

        public const string PRODUCING = "PRD";      //unit type in production
        public const string SPECIALTY = "SPC";      //specialty of city
        public const string EFFICIENCY = "EFF";     //efficiency
        public const string TTC = "TTC";            //turns to completion   
        public const string SUPPLY_STORE = "SST";   //current supply stored
        public const string MIN_STORE = "MST";      //min supplies stored
        public const string SCRAPVAL = "SCR";       //amount of scrap
        public const string AUTOSUPPLYDRAIN = "AUT"; //switch to allow the game to move between supply and drain - not for city units
        public const string CONS_FOR_DRAIN = "CFD"; //use supply for drain
        public const string CONS_PRIORITY = "CNP";  //priority ranking for supply use in drain

        public const int SUB_LEVEL_INDEX = 0;         
        public const int STACK_GROUND_INDEX = 1;
        public const int STACK_AIR_INDEX = 2;
        public const int ORB_LEVEL_INDEX = 3;
        public const int TOTAL_LEVEL_INDEXES = 4;
    }
}

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

namespace com.kbs.empire.common.game.data
{
    //////////////////////////////////////////
    //Empire Common Constants
	public class EmpireCC
	{
        //Hard Coded GameVersions of Unit Sets
        public const string US_BS = "EBUNITS"; //Basic
        public const string US_SS = "ESUNITS"; //Standard
        public const string US_AS = "EAUNITS"; //Advanced
        public const string US_ES = "EEUNITS"; //Enhanced


	    //Default ID - Mean There is no associated unit
	    public const uint NO_UNIT_ID = 0u;  
        //Neutral Owner Indication - position for neutral player 
        //be aware of the negative if you are using arrays
	    public const int NEUTRAL = -1;
        //No Unit Type Specified
	    public const string C_NO_UNIT_TYPE = "CNUT";
        //Forever, basically
        public const int LONGTIME = -1;

        //Sent By Map Maker DLL when something goes wrong
        public const string MAP_FAIL = "MAP_FAIL";

        public const uint UNEVER = uint.MaxValue;
	    public const int IMPOSSIBLE = int.MaxValue;

        //No Land Mass/Sea Mass Id
	    public const int NO_MASS_ID = -1;

	    ///////////////////////////////
        //Hard Coded Terrain Codes
        //Made 1 char
        public const string CT_BLANK = "B";
        public const string CT_EDGE = "E";
        public const string CT_CITY = "C";
        public const string CT_CLEAR = "L";
        public const string CT_SEA = "S";
        public const string CT_SHALLOWSEA = "A";
        public const string CT_FOREST = "F";
        public const string CT_ROUGH = "R";
        public const string CT_MOUNTN = "M";
        public const string CT_RIVER = "V";
        public const string CT_HILL = "H";
        public const string CT_SNOW = "W";
        public const string CT_SWAMP = "P";
        public const string CT_PEAK = "K";
	    public const string CT_SPARE_0 = "0";
        public const string CT_SPARE_1 = "1";
        public const string CT_SPARE_2 = "2";
        public const string CT_SPARE_3 = "3";
        public const string CT_SPARE_4 = "4";
        public const string CT_SPARE_5 = "5";
        public const string CT_SPARE_6 = "6";
        public const string CT_SPARE_7 = "7";
        public const string CT_SPARE_8 = "8";
        public const string CT_SPARE_9 = "9";


        //City Unit Type Hard Coded
        public const string U_CITY = "CI";
        public const string U_INFANTRY = "IN";
        public const string U_ARMOR = "AR";
        public const string U_TRUCK = "TK";

        public const string U_LIGHT_ARTY = "LA";
        public const string U_HEAVY_ARTY = "HA";
        public const string U_ANTI_AIR = "AA";

        public const string U_GENERAL = "GE";
        public const string U_ENGINEER = "EN";
        public const string U_SEABEE = "SB";

        public const string U_FIGHTER = "FI";
        public const string U_BOMBER = "BO";
        public const string U_AIR_TRAN = "AT";
        public const string U_HELICOPTER = "HE";

        public const string U_PATROL_BOAT = "PB";
        public const string U_TRANSPORT = "TR";
        public const string U_DESTROYER = "DE";
        public const string U_SUBMARINE = "SU";
        public const string U_SUBMERGED_SUB = "SS";
        public const string U_DEEP_SUB = "SD";
        public const string U_CRUISER = "CR";
        public const string U_BATTLESHIP = "BB";
        public const string U_AIRCRAFTCARRIER = "AC";

        public const string U_SCOUT_SAT = "SC";
        public const string U_ATT_SAT = "AS";
        public const string U_SHORT_MISSILE = "SM";
        public const string U_LONG_MISSILE = "LM";
        public const string U_SHORT_NUKE = "SN";
        public const string U_LONG_NUKE = "LN";

        public const string U_SUPPLY = "SP";

        public const string U_AIRBASE = "AB";
        public const string U_FORT = "FT";
        public const string U_PORT = "PT";
        public const string U_OILFIELD = "OF";

        public const string C_ROAD = "RD";
        public const string C_MINE = "MN";


        //Player Types
	    public const string EVIL_HUMAN = "EH";
	    public const string AWESOME_AI = "AA";

        //GIFT UNIT
	    public const string TREATY_GIFT = "TG";
        //Proposal
	    public const string PROP_UNCONFIRMED = "U";
	    public const string PROP_ACCEPTED = "A";
	    public const string PROP_REJECTED = "R";

	    public const string NULL_STRING = "NULL";
	}
}

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


using com.kbs.empire.common.game.data;

namespace com.kbs.empire.worldbuild.common.proc
{
    //////////////////////////////////////////////////////
    //Representation of units as final product
    //Handles Both City and Non City Units
	public class TempUnitStruct
	{
        /////////////////////////////////
        // ---Common To Unit And City----
        /////////////////////////////////

        /////////////////////////////////        
        //UNIT ID
        // id's do not need to be defined
        // unless the unit will have child units        
        public uint id_ = EmpireCC.NO_UNIT_ID;

        /////////////////////////////////        
        //Unit Owner 
        //(Position Of Owning Player) 0 to n-1
	    public int owner_ = EmpireCC.NEUTRAL;

        /////////////////////////////////        
        //Unit Type String
        public string utype_ = EmpireCC.C_NO_UNIT_TYPE;

        /////////////////////////////////        
        //Unit's name
        //unit will be named by the game 
        //if this field is unchanged
	    public string name_ = "";

        /////////////////////////////////        
        //Unit Location
	    public int lx_ = 0;
        public int ly_ = 0;

        /////////////////////////////////        
        //ID of host unit if it has one
	    public uint host_id_ = EmpireCC.NO_UNIT_ID;


        /////////////////////////////////
        // --------City Specific --------
        /////////////////////////////////        

        /////////////////////////////////        
        //Important - for games where 
        //important cities matter
        public bool is_important_ = false;

        /////////////////////////////////        
        //For Cap-Kill Games - one per position
	    public bool is_capital_ = false;

        /////////////////////////////////        
        //The starting production efficiency 
        //of the city
	    public int prod_eff_ = 0;

        /////////////////////////////////        
        //The starting amount of supply 
        //a city/unit has
	    public uint supply_ = 0u;

        /////////////////////////////////        
        //Unit Type Production Specialty if any
        public string spec_ = EmpireCC.C_NO_UNIT_TYPE;

	}
}

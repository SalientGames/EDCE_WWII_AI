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
    //representation of locations as final product
	public class TempMapStruct
	{
        //String Code representing the terrain
	    public string terrainType_ = EmpireCC.CT_BLANK;
        //resource count for the square
	    public uint resources_ = 0u;
        //mine strength in square
	    public uint mineCharges_ = 0u;
        //does it have a road
	    public bool road_ = false;
        //is it a wasteland
	    public bool wasteland_ = false;
	}
}

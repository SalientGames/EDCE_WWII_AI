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
using com.kbs.empire.ai.common.cevent;
using com.kbs.empire.ai.common.map;
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.game.unit;

namespace com.kbs.empire.ai.common.proc
{
    //The Cheat interface can only be accessed during the runTurn portion of the AI.
    public interface AICheatI
    {
        //Can This unit of lsa type get from loc A to loc B 
        bool is_path(int positon, CLoc A, CLoc B, string lsacode);
        //Give me a path from A to B
        List<CLoc> buildPath(int position, CLoc A, CLoc B, string utype);



        //////////////////////////////////////////////////////////////
        //LAND MASS AND SEA MASS
        //These Land Mass IDs may change over time if 
        //cities are added with buy points. 
        //If no buy points are used then the masses will be constant
        //on turn one - otherwise, wait till turn 2 for static accuracy
        //So for games with BP's (as of this writing 100 or over) - no guarantees as to 
        //the data not changing until turn 2
        //get a land mass' id
        int getLandMassId(int position, CLoc loc);
        //get a sea mass' id
        int getSeaMassId(int position, CLoc loc);

        //squares count on land masses 
        int getLandMassSize(int position, int lmid);
        //city count on land masses        
        int getCityLandMassCount(int position, int landMassId);
        List<uint> getCitiesByLandMass(int position, int landMassId);

        //squares count on sea masses 
        int getSeaMassSize(int position, int smid);
        //city count on sea masses
        int getCitySeaMassCount(int position, int seaMassId);
        List<uint> getCitiesBySeaMass(int position, int seaMassId);
        //////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////

        //Get City
        CProducerUnit getCity(int position, uint cid);

        //Get Unit
        CUnit getUnit( int position, uint uid);

        //How many cities does a player have
        int getCityCount(int position, int p);

        //What really is the terrain there?
        CMapLocInfo getTerrainType(int position, CLoc loc);

        //What is really there within range
        List<CUnit> getUnitsInRange(int position, string level, CLoc loc, int range);
        //Get A Specific City at a location
        CProducerUnit getCityByLocation(int position, CLoc loc);

        //This AI is immune to drain issues
        void immuneToDrain(int position);

        //This will give back events to reveal an area, will be terrain information and city spots - no other units
        //x1 <=  x2
        //y1 <=  y2
        //only within map bounds
        List<CGameEvent> getTerrainInfo(int position, int x1, int y1, int x2, int y2);
    }
}

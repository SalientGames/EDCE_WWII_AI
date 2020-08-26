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

namespace com.kbs.empire.common.game.proc
{
    //basically an interface to a unit data in a unit set
    public interface UnitQueryI
    {
        ////////////////////////////////////////////////////////
        //Unit Stats
        ////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////
        //General
        //Unit Type
        string utype();
        //LongName
        string longName();
        //Shortname
        string shortName();
        //Land/Sea/Air type
        string lsaCode();
        //Helpers
        bool isSea();
        bool isAir();
        bool isLand();
        bool isOrb();
        bool isSub();

        ////////////////////////////////////////////////////////
        //Stats
        int mvr(); //Movement range
        int mvs(); //movement points
        int portage();//portage value
        int capacity(); //portage capacity
        int maxHits();//hitpoints
        int damage();//damage per hit
        int bombEff();//bomb damage to efficiency
        bool canMove(string ttype);//is this terrain naturally allowed
        int getMove(string ttype); //return movement points needs to enter terrain

        ////////////////////////////////////////////////////////
        //Combat
        //ability to attack
        bool canMoveCombat(string utype);
        //ability to attack without entering
        bool canSeaBombardAttack();
        //get attack deep subs
        bool canEngageSubLevelUnits();
        //can attack cites type
        string cityCombat();
        //morph type post capture
        string cityPPUnit();
        //can Dig in
        bool canDigIn();
        //Can Capture City (convenience)
        bool canCityCapture();
        //always Dies in combat
        bool diesAfterAtt();

        ////////////////////////////////////////////////////////
        //RangeFire Combat
        bool canRangeFireAttack(string utype);
        //range fire type
        string rfAttackCode();
        //range
        int rfRange();
        //damage
        int rfDamage();
        //defensive fire allowed?
        bool canDefFire();
        //hosted units can range fire (like LA in City)
        bool allowChildrenRFire();
        //unit is restricted to not firing at orb on ground, or not firing on ground from orb.
        bool levelRestrictedRFire();
        ////////////////////////////////////////////////////////
        //Scanning
        //scan range to see terrain stuff
        int terrainScanRange();
        //max overall scanning range 
        int maxScanRange();
        //scan when looking for unit in reentry
        int reentryScaneRange();
        //Hosted units cannot spot
        bool blockHostedLandSpotting();
        //can see units on land/sea/air
        bool canSeeStackLevelUnits();
        //can see orbiting units
        bool canSeeOrbLevelUnits();
        //see units in deep
        bool canSeeSubLevelUnits();
        //sees mines
        bool canSeeMines();
        //returns scan range of a particular type
        int scanRange(string utype);

        ////////////////////////////////////////////////////////
        //Production Queries 
        //What can it make?
        List<string> produceables();
        //Can I produce a type
        bool canProduce(string producee);
        //Initial production value for a type
        int initProduce(string producee);
        //Continuing Production Value For a Type
        int contProduce(string producee);


        //Type Queries
        //Cities Are Unique Units in the game that 
        //--produce units
        //--help the drain
        //--host most units
        //--are marked by a special terrain type
        //--often count towards victory
        bool isCity();
        //producers aree special units that can help the drain and produce supply
        bool isProducer();
        bool helpsDrain();
        bool helpsSupply();
        //a base is a unit which holds other units and do not move
        bool isBase();
        //constructors are units that can build other units and move
        bool isConstructor();
        //can I make a road
        bool makeRoad();
        //do i act as a road
        bool asRoad();

        //Orbital Type
        //Launch Me
        bool canLaunch();
        //I can reenter
        bool canRenter();
        //I am the ultimate solution
        bool isNuke();
        //almost...
        int nukePerc();

        //Transporting
        //I can drop 
        bool canTransportDrop();
        //I can carry a type
        bool canTransport(string utype);

        //Landing
        //I can land
        bool canLand(string ttype);

        //Morphing
        //My A Changing Type
        string morphAType();
        //What level can I change at
        string morphALevel();
        //Can I change in this terrain
        bool canMorphAInTerrain(string ttype);
        //My B changing Type
        string morphBType();
        string morphBLevel();
        bool canMorphBInTerrain(string ttype);
        
        //Engineering Type
        //I can repair type while hosting/hosted
        bool canRepair(string utype);
        //I can destroy roads
        bool canDestroyRoads();
        //how long to build/destroy a road
        int roadTime(string ttype);
        

        //Experience
        //how much exp needed for proven
        uint provenLevelExp();
        //how much exp needed for hardened
        uint hardenedLevelExp();

        //Buypoints Cost
        //my price
        int buypoints();

    }
}

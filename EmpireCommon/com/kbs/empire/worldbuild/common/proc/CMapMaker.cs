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
using com.kbs.common.thread;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.util.log;
using com.kbs.empire.common.util.random;

namespace com.kbs.empire.worldbuild.common.proc
{
    /////////////////////////////////////////////////////
    //Shared Code For All Map Makers - Base class
    public abstract class CMapMaker : RunnableI, WorldBuilderI
	{
        /////////////////////////////////////////////////////
        //Logging
        public CLog reallogger_ = null;
        public CSubLog logger_ = null;

        //////////////////////////////////////////////////////
        //passed in
        protected StringPollerI distributor_ = null;
        protected MapCallbackI callback_ = null;
        //////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////
        //coming out - cities, units, map
        protected readonly List<TempUnitStruct> cities_ = new List<TempUnitStruct>();
        protected readonly List<TempUnitStruct> units_ = new List<TempUnitStruct>();
        protected readonly List<TempMapStruct> mapLocs_ = new List<TempMapStruct>();
        
        //For Map Calculations
        protected CMapUtil mapUtil_ = null;

        /////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////
        //game type
        /////////////////////////////////////////////////////
        //Random Generator -
        protected CMTRandom random_ = null;

        /////////////////////////////////////////////////////
        //unit list
        protected WBQueryI unitData_ = null;
        /////////////////////////////////////////////////////////
        //Game Map Parameters
        protected CGameMapParameters mparams_ = null;

        public virtual void prepare(
            int mapWidth,
            bool horzWrap,
            int mapHeight,
            bool vertWrap,
            CDLLHints hints,
            CGameMapParameters mparams,
            WBQueryI unitData,
            StringPollerI distributor,
            MapCallbackI callback,
            string logpath,
            string logname,
            int loglevel)
        {
            if (reallogger_ == null)
            {
                reallogger_ = new CLog(logpath, logname, loglevel);
                logger_ = new CSubLog("MapMaker", reallogger_);
            }
            distributor_ = distributor;
            callback_ = callback;

            unitData_ = unitData;

            mapUtil_ = new CMapUtil(mapWidth, horzWrap, mapHeight, vertWrap);

            mparams_ = mparams;

            //build the map output
            for (int i = 0; i < mapWidth * mapHeight; i++)
                mapLocs_.Add(new TempMapStruct());

            //the default seed - object may get replaced
            random_ = new CMTRandom();
        }

        protected abstract void buildWorld();
        public void run()
        {
            buildWorld();
            if(reallogger_ != null)
               reallogger_.endLog();
        }
     
        public void closeLogger(){reallogger_.endLog();}
	}
}

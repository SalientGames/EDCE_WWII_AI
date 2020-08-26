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
using com.kbs.common.thread;
using com.kbs.empire.common.game.hints;

namespace com.kbs.empire.worldbuild.common.proc
{
    //inteface for preparing for a world build
    public interface WorldBuilderI
    {
        void prepare(
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
            int loglevel
            );

    }
}

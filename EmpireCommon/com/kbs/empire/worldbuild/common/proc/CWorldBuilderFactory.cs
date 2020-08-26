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
using com.kbs.empire.common.game.hints;

namespace com.kbs.empire.worldbuild.common.proc
{
    //Base Class For serviing world buildingers
    public abstract class CWorldBuilderFactory
    {
        public abstract CDLLHints getMapHints();

        public abstract CMapMaker getMapMaker();

    }
}

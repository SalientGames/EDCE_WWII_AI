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

using System;

namespace com.kbs.empire.common.util.log
{
    //used for AI player output
    public class CSubLog
    {
        private readonly string name_;
        private readonly CLog trueLog_;

        public CSubLog(string name, CLog trueLog)
        {
            name_ = name;
            trueLog_ = trueLog;
        }

        public void info(string str)
        {
            if (trueLog_ != null)
                trueLog_.info(name_ + " -> " +  str);
        }
        public void infof(string str)
        {
            if (trueLog_ != null)
                trueLog_.infof(name_ + " -> " + str);
        }

        public void info(Exception T)
        {
            if (trueLog_ != null)
                trueLog_.info(name_ + " -> ", T);
        }
    }
}

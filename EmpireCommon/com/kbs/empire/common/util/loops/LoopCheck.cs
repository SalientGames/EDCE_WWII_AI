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
using com.kbs.empire.common.util.log;

namespace com.kbs.empire.common.util.loops
{
    public class LoopCheck
    {
        public int checkCount_ = 5;
        private int counter_ = 0;
        public string label_ = null;

        public CSubLog logger_ = null;

        public LoopCheck(int checkCount, CSubLog logger)
        {
            checkCount_ = checkCount;
            logger_ = logger;
        }
        public void setLog(CSubLog logger)
        {
            logger_ = logger;
        }

        //Reset The Counter (options to add label, change count max
        public void reset(string label)
        {
            label_ = label;
            reset();
        }
        public void reset(string label, int checkCount)
        {
            label_ = label;
            reset(checkCount);
        }
        public void reset(int checkCount)
        {
            checkCount_ = checkCount;
            reset();
        }
        public void reset()
        {
            counter_ = 0;
        }

        public bool loopCheck()
        {
            counter_++;
            if(wasExceeded())
            {
                if(logger_ != null)
                {
                    logger_.infof("Count Check Exceeded: " + ((label_ != null) ? label_:"") + " [" + Convert.ToString(counter_) + "/" + Convert.ToString(checkCount_) + "]");
                }
                return false;
            }
            return true;
        }

        public bool wasExceeded()
        {
            return (counter_ > checkCount_);
        }
    }
}

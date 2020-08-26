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
using System.Collections.Generic;
using com.kbs.empire.ai.common.cevent;
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.game.order;
using com.kbs.empire.common.util.log;
using com.kbs.empire.common.util.xml;


namespace com.kbs.empire.ai.common.player
{
    //Base AI Clas that ultimately all players must be derived from
    public abstract class AIPlayer
    {
        public readonly CLog realLog_;
        public void closeLog() { realLog_.endLog(); }
        private readonly CSubLog alogger_;

        protected bool aiLogging_ = false;

	    public int recurseCount = 0;

        //Interfacing
        public AICheatI cheat_ = null;
        public AIQueryI query_ = null;
        private readonly AICommandInterfaceI command_ = null;
        private readonly AIEventInterfaceI aiEvent_ = null;
        //////////////////////////////////////////////////////////////
        //Data
        //position
        public readonly int position_;

        //name
        public string pname_ = "Your Nemesis";

        //////////////////////////////////////////////////////////////////////////////
        //This is flipped if a signal is sent from the mothership to stop.
        private bool holdFlag_ = false;
	    public void ackHold(){holdFlag_ = false;}
        public void setHold(){holdFlag_ = true;}

        //////////////////////////////////////////////////////////////////////////////
        //Progress is your estimate, between 0-10000 of you percentage complete for
        //a turn  10000 = 100%, 0 = 0%
        public const uint MAX_PROGRESS = 10000u;
        public const uint NO_PROGRESS = 0u;
        //////////////////////////////////////////////////////////////////////////////
        
	    //////////////////////////////////////////////////////////////
        protected AIPlayer(int position, string path, string logname, AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel)
        {
            position_ = position;
            realLog_ = new CLog(path, logname, logLevel);
            alogger_ = new CSubLog("AIPlayer" + Convert.ToString(position_), realLog_);
            alogger_.info("A Logger Log Open: "  + path  + " " + logname);

            aiEvent_ = aiEvent;
            command_ = command;
            query_ = query;
            cheat_ = cheat;

	    }

        // ReSharper disable UnusedParameter.Local
        protected AIPlayer(
            int position, 
            string path, 
            string logname, 
            Dictionary<string, string> caMap,  
            CEncodedObjectInputBufferI bin, 
            AIEventInterfaceI aiEvent, 
            AICommandInterfaceI command, 
            AIQueryI query, 
            AICheatI cheat, 
            int logLevel)
        // ReSharper restore UnusedParameter.Local
        {
            position_ = position;
            realLog_ = new CLog(path, logname, logLevel);
            alogger_ = new CSubLog("AIPlayer:" + Convert.ToString(position_), realLog_);

            aiEvent_ = aiEvent;
            command_ = command;
            query_ = query;
            cheat_ = cheat;

        }

        //////////////////////////////////////////////////////////////
        //what makes the AI tick - your control thread starts here
        //should not return unless told or your turn is done        
        protected abstract void runTurn();

        public void runMyTurn()
        {
            try
            {
                setProgress(NO_PROGRESS);
                runTurn();
            }
            catch (Exception T)
            {
                alogger_.info("Error In Run Turn");
                alogger_.info(T);
                command_.aiThrow(position_, T);
            }
            finally
            {
                setProgress(MAX_PROGRESS);
                endTurn();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        //Events
        private Queue<CGameEvent> getEvents() { return aiEvent_.poll(position_); }

        public bool pollAllEvents()
        {
            Queue<CGameEvent> evec = getEvents();
            while (!holdFlag_ && evec != null && evec.Count > 0)
            {
                processEvents(evec, alogger_);
                evec = getEvents();
            }
            return holdFlag_;
        }
        public void processEvents(Queue<CGameEvent> evec, CSubLog logger)
        {
            while (!holdFlag_ && evec.Count > 0)
            {
                CGameEvent ge = evec.Dequeue();
                //update the sequence to mark event as received
                processEvent(ge, alogger_);
            }
        }

        public virtual void processEvent(CGameEvent ge, CSubLog logger)
        {
            //Here You Would Deal With The Event By Looking
            //At It's Type and taking appropriate action
            //if need be
        }
	
	    //////////////////////////////////////////////////////////////////////////////////////////
	    //////////////////////////////////////////////////////////////////////////////////////////
	    /////////////////////////////////////////////////////////////////////////////
	    //To Command Interface
        /////////////////////////////////////////////////////////////////////////////
        //Neutralize - Give Away Unit
        public bool neutralizeUnit(uint gid)
        {
            string res = command_.neutralize(position_, gid);
            if (res != null)
            {
                if (aiLogging_)
                    alogger_.info(" neutralizeUnit: " + res);
            }
            else
                pollAllEvents();

            return (res == null);
        }

        //Set Production for a city/producer
        public bool setProduction(uint gid, string utype, uint applySupply)
        {
            string res = command_.setProducerProduction(position_, gid, utype, applySupply);
            if (res != null)
            {
                if (aiLogging_)
                    alogger_.info(" setproduction: " + res);
            }
            else
                pollAllEvents();

            return (res == null);
        }
        //Set Supply consumption
        public bool setProductionConsumption(uint gid, bool consume, int priority)
        {
            string res = command_.setProducerConsumptionOfSupply(position_, gid, consume, priority);
            if (res != null)
            {
                if (aiLogging_)
                    alogger_.info(" setProductionConsumption: " + res);
            }
            else
                pollAllEvents();

            return (res == null);
        }
        //Set Minimum Supply Store
        public bool setProducerMinSupply(uint gid, uint minSupply)
        {
            string res = command_.setProducerMinSupplyStore(position_, gid, minSupply);
            if (res != null)
            {
                if (aiLogging_)
                    alogger_.info(" setProducerMinSupply: " + res);
            }
            else
                pollAllEvents();

            return (res == null);
        }

        //End Turn (automatically called at the end of runTurn() )
        private void endTurn()
        {
            if(aiLogging_)
                alogger_.info("Calling End Turn");
            try
            {

            string res = command_.endPlayerTurn(position_);
            if(res != null){alogger_.info(res);}
            }
            catch (Exception T)
            {
                alogger_.info(T);
            }

            if (aiLogging_)
                alogger_.info("Calling End Turn Done");
        }
        //disband unit
        public bool disbandUnit(uint gid)
        {
            string res = command_.disbandUnit(position_, gid);
            if (res != null)
            {
                alogger_.info(" disband: " + res);
            }
            else
                pollAllEvents();
            return (res == null);
        }
        //buypoints commands - results gain via event poll
        public void placeBPUnit(string placeType, CLoc coord)
        {
            command_.placeBPUnit(position_, placeType, coord);
            pollAllEvents();
        }
        public void removeBPUnit(uint gid)
        {
            command_.removeBPUnit(position_, gid);
            pollAllEvents();
        }
        public void removeMineOrRoad(CLoc coord)
        {
            command_.removeMineOrRoad(position_, coord);
            pollAllEvents();
        }


        //notification - no expectation of result
        public void doNotification(string ntype, uint a, uint b, uint c, string info)
        {
            var cn = new CNotification(ntype, a, b, c, info);
            command_.notify(position_, cn);
        }

        //messages
        public void sendMessage(int position, string message)
        {
            command_.sendChatMessage(position_, position, pname_, message);
        }

        ////////////////////////////////////////////////////////////////////
	    //MOVEMENT/ORDER EXECUTION
        //For these commands, resulting events will be executed before the result is returned	   
        ////////////////////////////////////////////////////////////////////
        //Sets Order - does not execute
	    public string setUnitOrder(uint gid, COrder ord) 
	    {
		    CMoveResult cmr = command_.setOrder(position_, gid, ord);
		    return executeMoveResult(cmr, gid);
	    }

        public string executeUnitOrder(uint gid)
        {
            CMoveResult cmr = command_.executeOrder(position_, gid);
            return executeMoveResult(cmr, gid);
	    }

	    public string setAndExecuteUnitOrder(uint gid, COrder ord) 
	    {
            CMoveResult cmr = command_.setAndExecuteOrder(position_, gid, ord);
            return executeMoveResult(cmr, gid);
	    }

        private string executeMoveResult(CMoveResult cmr, uint gid) 
	    {
		    try
		    {
                if (aiLogging_)
                    alogger_.info("GMR Result " + cmr.result_ + " for gid " + gid);
			
			    recurseCount++;
			    if(recurseCount > 3)
			    {
                    if (aiLogging_)
                        alogger_.info("Recurse Count Exceeded`: " + recurseCount);
				    return COrderConstants.MV_FAILURE;
			    }
				
			    if(cmr.error_ != null)
			    {
                    if (aiLogging_)
                        alogger_.info("On exe gmr Unit Order: Error->: " + cmr.error_);
				    return cmr.result_;
			    }
			
			    //process gmr events
			    processEvents(cmr.events_, alogger_);
			    //process other events
			    pollAllEvents();
	
		        return cmr.result_;
	                		
		    }
		    finally
		    {
			    recurseCount--;
		    }
	    }

        //Cleanup call - different from save
        public virtual void cleanup()
        {
            closeLog();
        }


        //To Produce Save Data
        private const string PNAME_TAG = "PNAME";
        public virtual void encodeInternal(CEncodedObjectOutputBufferI output)
        {
            output.addTextObject(PNAME_TAG, pname_);
        }

	    //This event is passed to give you the state of the AI - only done at the beginning,
        //for reload it will be null
        public virtual void aiRestored(CStateEvent cse)
        {
            if(cse != null)
                processEvent(cse, alogger_);
            alogger_.info("Player Restored");
        }
	    ////////////////////////////////////////////////////////////
        public string getName() { return pname_; }



        ////////////////////////////////////////////////////////////
        //Progress  AI Sets Progress
        protected void setProgress(uint pval)
        {
            command_.setProgress(pval);
        }
    }
}

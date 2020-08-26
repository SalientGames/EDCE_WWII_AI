////////////////////////////////////////////////////////////////////////////////////////
//This Source Code File Is Part Of An
//AI DLL Assembly for 
//Empire Deluxe Combined Edition
//
//Copyright 2017 Mark Kinkead
//May be freely hacked and altered to make your own 
//non-commercial AI DLLs for Empire Deluxe Combined Edition
//All other rights reserved
//
//This code can be hacked to to build AI players
//Please remeber to create a unique dll name for your player
//in order to be recognized by the game
//This code can only be worked in association with 
//the game Empire Deluxe Combined Edition
//
//Version Release Information Available
//In the Factory file of each AI player
//
//
////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using com.kbs.empire.ai.common.cevent;
using com.kbs.empire.ai.common.player;
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.log;
using com.kbs.empire.common.util.xml;

//See Factory For Version Info
namespace ai.player
{
    //Example Player - the actual player instantce for this DLL
    //This class takes advantage of AI player data, but that is not
    //necessary. All AI PLayer Instances must ultiimately be derived 
    //from the "AIPlayer" class found in the common library.
    //It is recommended you review that class.
    //And recommended you review AIPlayer Data if you care to use it (which is also recommended!)
    //
    public class CorporalAI : AIPlayerData
    {
        //The output logger for debug work
        public readonly CSubLog elogger_ = null;


        //hints passed at construction
        //These represent any hints the player has setup for this unique AI instance
        private readonly CDLLHints hints_;

        //construction
        //This is the constructor for an AI Player at a new game.
        public CorporalAI(int position, string pname, string logpath, string logname, CDLLHints hints,
            AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel) 
            : base(position, logpath, logname, aiEvent, command, query, cheat, logLevel)
        {
            //creation of the player log
            elogger_ = new CSubLog("Player " + Convert.ToString(position), realLog_);
            realLog_.logLevel_ = CLog.INFO;
            elogger_.info("Open " + logpath + " " + logname);
            elogger_.info(pname + " waking up");

            //copy hints
            hints_ = hints.copy();

            foreach (string s in hints.hintOrder_)
            {
                elogger_.info("hint " + s);
            }


            //copy assigned name
            pname_ = pname;
        }

        //Run Turn
        //The game gives the AI PLayer Thread control. 
        //When it returns, the turn will be over
        //the start turn event will be somewhere in the stack of events
        //that the AI received. 
        protected override void runTurn()
        {
            try
            {
                elogger_.info("runTurn - start");

                //pull all events since last we met
                //This should end with the start turn
                if (pollAllEvents()) { ackHold(); return; }

                elogger_.info("runTurn - end");
            }
            catch (Exception T)
            {
                elogger_.info("runTurn - exception " + T);
            }
        }

        //Process Events
        //override this method to do extra stuff with the events that come in
        public override void processEvent(CGameEvent ge, CSubLog logger)
        {
            base.processEvent(ge, logger);

            elogger_.info("processEvent - " + ge);
        }

        //Saving The AI State
        //Attribute data needs to be completely saved before the child elements are saved
        //Read Up on how to use the CEncodedObjectOutputBufferI interface
        private const string TEST_ATTR = "TA";

        private int testAttribute_ = 12;
        public override void encodeInternal(CEncodedObjectOutputBufferI output)
        {
            ////////////////////
            //Attributes First
            encodeAttr(output);

            //now write out your attributes
            output.addAttr(TEST_ATTR, Convert.ToString(testAttribute_));

            ////////////////////
            //Derived Children Second
            encodeChildren(output);

            //now write out your objects
            hints_.encode(output);

        }

        //Loading The AI State
        //The map holds the attributes previously saved
        //child elements must be loaded in the order they were saved
        //however, note that any STATE (cities, units, etc.) data used in the parent classes 
        //(AIPlayer and AIPlayerData) will not be set until aiResotred is called.
        //Read Up on how to use the CEncodedObjectInputBufferI interface
        //
        //When the Input Buffer enters this function, the "xml tape" is to be assumed to 
        //be at the first element written out i encode enternal.
        //caMap contains any attributes written out in EncodeInternal
        //
        public CorporalAI(
            int position, 
            Dictionary<string, string> caMap, 
            CEncodedObjectInputBufferI bin, 
            string logpath, 
            string logname,
            AIEventInterfaceI aiEvent, 
            AICommandInterfaceI command, 
            AIQueryI query, 
            AICheatI cheat,
            int logLevel) 
            : base(
                position, 
                logpath, 
                logname, 
                caMap,
                bin, 
                aiEvent, 
                command, 
                query, 
                cheat,
                logLevel)
        {
            elogger_ = new CSubLog(this.getName() + Convert.ToString(position), realLog_);
            elogger_.info("INI - LOGPATH - " + System.IO.Path.Combine(logpath, logname));
            elogger_.info("Position " + Convert.ToSingle(position) + " waking up.");

            //get attributes
            testAttribute_ = EncodeUtil.parseInt(caMap[TEST_ATTR]);

            //get objects - must retrieve in the order they were submitted
            hints_ = new CDLLHints(bin);
        }

        //Post reload from save activities, prior to game play
        //the base call reloads the state data into AIPlayerData (if in fact derived), 
        //then the AI player can reconnect any direct
        //object references used in the derived objects
        public override void aiRestored(CStateEvent cse)
        {
            elogger_.info("Restored started");
            base.aiRestored(cse);
            elogger_.info("Restored done");
            elogger_.info(pname_ + " is now self aware.");
        }

        //Creation of Hints
        //indicate the hints the player can touch and set their defaults
        private const string DEBUG_ATTR = "DB";
        private const string STANCE_ATTR = "ST";
        private const string STANCE_NORMAL = "NORMAL";
        private const string STANCE_DEFENSIVE = "DEFENSIVE";
        private const string STANCE_OFFENSIVE = "OFFENSIVE";

        public static CDLLHints getHints()
        {
            Dictionary<string, string> DictionaryStance_ = new System.Collections.Generic.Dictionary<string, string>();

            DictionaryStance_.Add(STANCE_NORMAL, "Normal");
            DictionaryStance_.Add(STANCE_DEFENSIVE, "Defensive");
            DictionaryStance_.Add(STANCE_OFFENSIVE, "Offensive");

            //Key Must Not Contain Spaces
            var ret = new CDLLHints(new CDLLInfo("WWIIAI", "WWII database AI", "WWII AI for use with WWII unit database.", "1.0"));

            ret.addInfo(new CDLLNameValueHintInfo(STANCE_ATTR, "Stance", "What is the stance of the AI", DictionaryStance_, STANCE_OFFENSIVE));
            ret.addInfo(new CDLLBoolHintInfo(DEBUG_ATTR, "Debug", "Create a debug file", true));

            return ret;
        }

    }
}

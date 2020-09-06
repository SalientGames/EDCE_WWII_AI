//////////////////////////////////////////////////////////////////////////////////////////
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


//This is the implementation of the AI Factory for the Example AI.
//It should not be within any namespace in order to be
//recognized by the game program.

using System.Collections.Generic;
using com.kbs.empire.ai.common.player;
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.random;
using com.kbs.empire.common.util.xml;

//VERSION 1 09-09-17
public class AI_WW2W_Basic : AIPlayerFactory
{
    ////////////////////////////////////////////
    //For Individual Name Selection
    private static readonly string[] leaders_ =
        {
            "Dwight D. Eisenhower",
            "Bernard Montgomery"
        };

    private readonly List<string> aiGroup_ = new List<string>();
    private readonly CMTRandom random_ = new CMTRandom();
    ////////////////////////////////////////////


    ////////////////////////////////////////////
    //The returned list gives the Unit Set DB keys
    //that the AI is most attuned to. This can 
    //include strings representing the keys for modified databases
    public override List<string> bestBuildSets()
    {
        var ret = new List<string>();
        ret.Add("WW2W_Basic_WW2_Enhanced_Set");
        return ret;
    }

    private const string DEBUG_ATTR = "DB";
    private const string STANCE_ATTR = "ST";
    private const string STANCE_NORMAL = "NORMAL";
    private const string STANCE_DEFENSIVE = "DEFENSIVE";
    private const string STANCE_OFFENSIVE = "OFFENSIVE";

    private const string SIDE_ATTR = "SD";
    private const string SIDE_ALLIED = "ALLIED";
    private const string SIDE_AXIS = "AXIS";

    ////////////////////////////////////////////
    //Hint Mechanism
    //This allows the factory to serve up a new 
    //hint set for the player to configure
    public override CDLLHints getHints()
    {
        Dictionary<string, string> DictionaryStance_ = new System.Collections.Generic.Dictionary<string, string>();
        Dictionary<string, string> DictionarySide_ = new System.Collections.Generic.Dictionary<string, string>();

        DictionaryStance_.Add(STANCE_NORMAL, "Normal");
        DictionaryStance_.Add(STANCE_DEFENSIVE, "Defensive");
        DictionaryStance_.Add(STANCE_OFFENSIVE, "Offensive");

        DictionarySide_.Add(SIDE_ALLIED, "Allied");
        DictionarySide_.Add(SIDE_AXIS, "Axis");

        //Key Must Not Contain Spaces
        var ret = new CDLLHints(new CDLLInfo("WW2W_Basic", "WW2 Winter basic AI", "WW2 winter AI for use with WW2W basic unit database.", "1.0"));

        ret.addInfo(new CDLLNameValueHintInfo(STANCE_ATTR, "Stance", "What is the stance of the AI", DictionaryStance_, STANCE_OFFENSIVE));
        ret.addInfo(new CDLLNameValueHintInfo(SIDE_ATTR, "Side", "What is the side of the AI", DictionarySide_, SIDE_ALLIED));
        ret.addInfo(new CDLLBoolHintInfo(DEBUG_ATTR, "Debug", "Create a debug file", true));

        return ret;
    }


    ////////////////////////////////////////////
    //Creation Call 
    //Returns a new instance of the player.
    //note the pplayer name is also being set here
    public override AIPlayer createAIPlayer(int position, string logpath, string logname, CDLLHints hints,
        AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel) 
    {
        AIPlayer AIPlayer_;

        //name selection
        if (aiGroup_.Count == 0)
        {
            for (int i = 0; i < leaders_.Length; i++)
                aiGroup_.Add(leaders_[i]);
        }

        int r = random_.nextInt(aiGroup_.Count);
        string pname = aiGroup_[r];
        aiGroup_.RemoveAt(r);

        // Create the player based on the side selection
        switch (hints.getValue(SIDE_ATTR))
        {
            case SIDE_ALLIED:
                AIPlayer_ = new ai.player.AlliedAI(position, pname, logpath, logname, hints, aiEvent, command, query, cheat, logLevel);
                break;
            case SIDE_AXIS:
                AIPlayer_ = new ai.player.AxisAI(position, pname, logpath, logname, hints, aiEvent, command, query, cheat, logLevel);
                break;
            default:
                AIPlayer_ = new ai.player.AlliedAI(position, pname, logpath, logname, hints, aiEvent, command, query, cheat, logLevel);
                break;
        }

        return AIPlayer_;
    }

    ////////////////////////////////////////////
    //Reload Call
    //This call gives the data needed to reload an AI player instance
    public override AIPlayer reloadAIPlayer(int position, CEncodedObjectInputBufferI bin, string logpath, string logname,
        AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel) 
    {
        Dictionary<string, string> caMap = bin.getAttributes();
        return new ai.player.AlliedAI(position, caMap, bin, logpath, logname, aiEvent, command, query, cheat, logLevel);
    }

}

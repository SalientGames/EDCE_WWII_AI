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
using com.kbs.empire.common.game.proc;

namespace com.kbs.empire.ai.common.proc
{
    //Access to the unit set information
    public interface AIQueryI
    {
        
        //UnitSetKey
        string unitSetKey();

        //All Unit Types
        List<string> getAllTypes();
        //Obtain A Unit query for a type
        UnitQueryI unitQuery(string utype);

        //Calculate the actual production time of unit gid making utype
        int prodTime(uint gid, string utype, bool initial);

        //Calculate The Attack Probability
        int attackProbability(
            string atype, int aowner, string ardy, int admg, 
            string dtype, int down, string drdy, 
            string ttype);
    }
}

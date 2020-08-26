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

namespace com.kbs.empire.common.game.order
{
    //A notification is like a command (player to game) you don't expect any feedback from
    //See CorderConstants
    public class CNotification
    {
        public readonly string ntype_;
        public readonly uint a_;
        public readonly uint b_;
        public readonly uint c_;
        public readonly string info_;
        
        public CNotification(string ntype, uint a, uint b, uint c, string info)
        {
            ntype_ = ntype;
            a_ = a;
            b_ = b;
            c_ = c;
            info_ = info;
        }
    }
}

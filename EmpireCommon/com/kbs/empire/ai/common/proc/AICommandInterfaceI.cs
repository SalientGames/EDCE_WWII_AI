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
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.game.order;
using com.kbs.empire.common.game.treaty;

namespace com.kbs.empire.ai.common.proc
{
    //Useful for sending command to the game
    public interface AICommandInterfaceI
    {
        //Neutralizing A Unit
        string neutralize(int position, uint gid);

        //Set Unit Production
        string setProducerProduction(int position, uint gid, string utype, uint applySupply);
        //Consume Supply If need be to off set drain
        string setProducerConsumptionOfSupply(int position, uint gid, bool consume, int priority);
        //Set Minimum Supply Store
        string setProducerMinSupplyStore(int position, uint gid, uint minSupply);
        //Set Auto use for drain/supply situations - does not work with cities
        string setProducerAutoDrainSuppy(int position, uint gid, bool autoOn);

        //End Player Turn
        string endPlayerTurn(int position);

        //Disband a Unit
        string disbandUnit(int position, uint gid);

        //Notify - some changes require no results
        void notify(int position, CNotification note);

        //Set Order - No Execute
        CMoveResult setOrder(int position, uint gid, COrder ord);
        //Execute a previously set order
        CMoveResult executeOrder(int position, uint gid);
        //Set AND Execute an Order
        CMoveResult setAndExecuteOrder(int position, uint gid, COrder ord);

        //Proposals - made or replied to
        void makeProposal(CProposal pr);
        //Chatting
        void sendChatMessage(int fposition, int position, string pname, string msg);

        //BuyPoints Commands
        void placeBPUnit(int fposition, string placeType, CLoc coord);
        void removeBPUnit(int fposition, uint gid);
        void removeMineOrRoad(int fposition, CLoc coord);

        //Tracking AI Player Turn Progress
        void setProgress(uint pval);
        //Tracking AI Errors
        void aiThrow(int position, Exception T);
    }
}

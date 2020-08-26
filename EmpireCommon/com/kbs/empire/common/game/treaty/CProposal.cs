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
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.treaty
{
    //only used for gifts
    public class CProposal
    {
        //will only be gift type
        public readonly string type_;
        //position from
        public readonly int fromP_;
        //position to 
        public readonly int toP_;
        //gift id
        public readonly uint gid_;
        //gift unit type
        public readonly string gutype_;
        //goft name
        public readonly string name_;
        //transaction id (handles by game)
        public uint id_ = 0;
        //status of gift
        public string status_ = EmpireCC.PROP_UNCONFIRMED;

        public const string TAGS = "CPROPS";
        private const string TAG = "CPROP";
        private const string TYPE = "TYP";
        private const string FROM = "FR";
        private const string TO = "TO";
        private const string GID = "GID";
        private const string NAME = "NM";
        private const string ID = "ID";
        private const string STATUS = "STA";

        public CProposal(string type, int fromP, int toP, string name, uint gid, string gutype)
        {
            type_ = type;
            fromP_ = fromP;
            toP_ = toP;
            name_ = name;
            gid_ = gid;
            gutype_ = gutype;
        }

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addAttr(TYPE, type_);
            output.addAttr(FROM, Convert.ToString(fromP_));
            output.addAttr(TO, Convert.ToString(toP_));
            output.addAttr(GID, Convert.ToString(gid_));
            output.addAttr(ID, Convert.ToString(id_));
            output.addAttr(STATUS, status_);

            output.addTextObject(NAME, name_);
            output.objectEnd();
        }

        public CProposal(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();
            type_ = A[TYPE];
            fromP_ = EncodeUtil.parseInt(A[FROM]);
            toP_ = EncodeUtil.parseInt(A[TO]);
            gid_ = EncodeUtil.parseUInt(A[GID]);
            id_ = EncodeUtil.parseUInt(A[ID]);
            status_ = A[STATUS];
            name_ = bin.getObjectText(NAME);
            bin.endTag(TAG);
        }
    }
}

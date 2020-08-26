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
using System.Text;
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.supply
{
    public class CSupplyRoute
    {
        //route id
        public uint rid_;

        //source id and loc
        public uint source_;
        public CLoc sourceLoc_;
        //dest id and loc
        public uint dest_;
        public CLoc destLoc_;
        //supplies in transit
        public uint inTransit_ = 0;
        //turns till next arrival
        public uint nextArrival_ = EmpireCC.UNEVER;

        public override string ToString()
        {
            var sb = new StringBuilder("{");
            sb.Append(rid_).Append(" src: ").Append(source_).Append(" dest: ").Append(dest_).Append(" it: ").Append(
                inTransit_).Append(" na: ").Append(nextArrival_).Append("}");
            return sb.ToString();
        }

        public CSupplyRoute(
            uint rid,
            uint source, 
            CLoc sourceLoc,
            uint dest,
            CLoc destLoc,
            uint inTransit,
            uint nextArrival
            )
        {
            rid_ = rid;
            source_ = source;
            sourceLoc_ = sourceLoc;
            dest_ = dest;
            destLoc_ = destLoc;
            inTransit_ = inTransit;
            nextArrival_ = nextArrival;
        }


        public const string TAGS = "CSPRTS";
        public const string TAG = "CSPRTS";
        public const string RID = "RID";
        public const string SOURCE = "SRC";
        public const string SOURCE_LOC = "SLOC";
        public const string DEST = "DES";
        public const string DEST_LOC = "DLOC";
        public const string IN_TRANSIT = "ITR";
        public const string NEXT_ARRIVAL = "NAR";

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addAttr(RID, Convert.ToString(rid_));
            output.addAttr(SOURCE, Convert.ToString(source_));
            output.addAttr(SOURCE_LOC, sourceLoc_.getKey());
            output.addAttr(DEST, Convert.ToString(dest_));
            output.addAttr(DEST_LOC, Convert.ToString(destLoc_.getKey()));
            output.addAttr(IN_TRANSIT, Convert.ToString(inTransit_));
            output.addAttr(NEXT_ARRIVAL, Convert.ToString(nextArrival_));
            output.objectEnd();
        }

        public CSupplyRoute(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();
            rid_ = EncodeUtil.parseUInt(A[RID]);
            source_ = EncodeUtil.parseUInt(A[SOURCE]);
            sourceLoc_ = CLoc.fromKey(A[SOURCE_LOC]);
            dest_ = EncodeUtil.parseUInt(A[DEST]);
            destLoc_ = CLoc.fromKey(A[DEST_LOC]);
            inTransit_ = EncodeUtil.parseUInt(A[IN_TRANSIT]);
            nextArrival_ = EncodeUtil.parseUInt(A[NEXT_ARRIVAL]);
            bin.endTag(TAG);
        }
    }
}

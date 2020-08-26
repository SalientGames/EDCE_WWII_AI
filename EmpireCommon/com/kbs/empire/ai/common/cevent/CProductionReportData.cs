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
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.ai.common.cevent
{
    public class CProductionReportData
    {
        public const string PRODUCTION_CONSTRUCTION = "PC";
        public const string EFFICIENCY_INCREASE = "EI";
        public const string MAKE_SUPPLY = "MS";
        public const string CONSUMED_SUPPLY = "CS";
        public const string SWITCHED_TO_DRAIN = "SWD";
        public const string SWITCHED_TO_SUPPLY = "SWS";


        //type of message
        public readonly string rtype_;
        //text of message
        public readonly string report_;
        //unit type of the "parent/actor"
        public readonly string ptype_;
        //either the efficiency or the supply amount in question
        public readonly int value_;
        //child type if produced/constructed
        public readonly string ctype_;
        //location of event
        public readonly CLoc loc_;
        //parent key
        public readonly uint pkey_;
        //child key (if any)
        public readonly uint ckey_;


        public CProductionReportData(
            string rtype, 
            string report, 
            string ptype, 
            int value,
            string ctype,
            CLoc loc,
            uint pkey,
            uint ckey
            )
        {
            rtype_ = rtype;
            report_ = report;
            ptype_ = ptype;
            value_ = value;
            ctype_ = ctype;
            loc_ = loc;
            pkey_ = pkey;
            ckey_ = ckey;
        }

        public const string TAGS = "CPDRS";
        private const string TAG = "CPDR";
        private const string RTYPE = "RT";
        private const string REPORT = "REP";
        private const string PTYPE = "PT";
        private const string VALUE = "V";
        private const string CTYPE = "CT";
        private const string LOC = "LOC";
        private const string PKEY = "PK";
        private const string CKEY = "CK";


        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addAttr(VALUE, Convert.ToString(value_));
            output.addAttr(PKEY, Convert.ToString(pkey_));
            output.addAttr(CKEY, Convert.ToString(ckey_));

            output.addTextObject(RTYPE, rtype_);
            if(report_ != null)
                output.addTextObject(REPORT, report_);
            if(ptype_ != null)
                output.addTextObject(PTYPE, ptype_);
            if(ctype_ != null)
                output.addTextObject(CTYPE, ctype_);
            if(loc_ != null)
                output.addTextObject(LOC, loc_.getKey());

            output.objectEnd();
        }

        public CProductionReportData(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();
            value_ = EncodeUtil.parseInt(A[VALUE]);
            pkey_ = EncodeUtil.parseUInt(A[PKEY]);
            ckey_ = EncodeUtil.parseUInt(A[CKEY]);

            rtype_ = bin.getObjectText(RTYPE);
            if (bin.thisTag() == REPORT)
                report_ = bin.getObjectText(REPORT);
            else
                report_ = null;

            if (bin.thisTag() == PTYPE)
                ptype_ = bin.getObjectText(PTYPE);
            else
                ptype_ = null;

            if (bin.thisTag() == CTYPE)
                ctype_ = bin.getObjectText(CTYPE);
            else
                ctype_ = null;

            if (bin.thisTag() == LOC)
                loc_ = CLoc.fromKey(bin.getObjectText(LOC));
            else
                loc_ = null;

            bin.endTag(TAG);
        }
    }
}

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
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.game.unit;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.order
{
    public class COrder
    {
        public readonly string ordType_;
        public readonly int value_ = 0;
        public readonly List<CLoc> locs_ = null;
        public readonly string utype_ = null;

        //Flag Is used for construction of mines and roads
        //also can be set in movement cases where you do not want out of range restrictions
        public readonly bool flag_ = false;
        
        public readonly uint ugid_ = CUnitConstants.NOUNIT;

        //optional for move type commands - if this is enabled, 
        //transports will load and unload in a city upon exit/entry
        public bool useEmbark_ = false;



        private const string TAG = "CORD";
        private const string ORDTYPE = "OT";
        private const string VALUE = "V";
        private const string LOCS = "LS";
        private const string LOC = "L";
        private const string UTYPE = "OUT";
        private const string FLAG = "FL";
        private const string UGID = "UGID";
        private const string USEEMBARK = "UEM";

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);    
            output.addAttr(ORDTYPE, ordType_);
            output.addAttr(VALUE, Convert.ToString(value_));
            if (utype_ != null)
                output.addAttr(UTYPE, utype_);
            output.addAttr(FLAG, EncodeUtil.makeBoolString(flag_));
            output.addAttr(UGID, Convert.ToString(ugid_));
            output.addAttr(USEEMBARK, EncodeUtil.makeBoolString(useEmbark_));

            output.openObject(LOCS);
            if(locs_ != null)
            {
                foreach (CLoc l in locs_)
                    output.addTextObject(LOC, l.getKey());
            }
            output.objectEnd();

            
            output.objectEnd();
        }

        public COrder(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);

            Dictionary<string, string> A = bin.getAttributes();
            ordType_ = A[ORDTYPE];
            value_ = EncodeUtil.parseInt(A[VALUE]);
            if (A.ContainsKey(UTYPE))
                utype_ = A[UTYPE];
            flag_ = EncodeUtil.fromBoolString(A[FLAG]);
            ugid_ = EncodeUtil.parseUInt(A[UGID]);
            useEmbark_ = EncodeUtil.fromBoolString(A[UGID]);

            bin.firstChild();
            bin.nextTag(LOCS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                locs_ = new List<CLoc>();
                while(!bin.reachedEndTag(LOCS))
                {
                    CLoc l = CLoc.fromKey(bin.getObjectText(LOC));
                    locs_.Add(l);
                }
            }
            bin.endTag(LOCS);


            bin.endTag(TAG);
        }


        public COrder(string ordType)
        {
            ordType_ = ordType;
        }

        public COrder(string ordType, uint ugid)
        {
            ordType_ = ordType;
            ugid_ = ugid;
        }

        public COrder(string ordType, int value)
        {
            ordType_ = ordType;
            value_ = value;
        }

        public COrder(string ordType, CLoc loc)
        {
            ordType_ = ordType;
            locs_ = new List<CLoc>();
            locs_.Add(loc.copy());
        }
        public COrder(string ordType, CLoc loc, bool flag)
        {
            ordType_ = ordType;
            locs_ = new List<CLoc>();
            locs_.Add(loc.copy());
            flag_ = flag;
        }
        public COrder(string ordType, List<CLoc> list)
        {
            ordType_ = ordType;
            locs_ = new List<CLoc>();
            foreach (CLoc loc in list)
            {
                locs_.Add(loc.copy());
            }
        }
        public COrder(string ordType, string utype, List<CLoc> list)
        {
            ordType_ = ordType;
            utype_ = utype;
            locs_ = new List<CLoc>();
            foreach (CLoc loc in list)
            {
                locs_.Add(loc.copy());
            }
        }

        public COrder(string ordType, List<CLoc> list, bool flag)
        {
            ordType_ = ordType;
            locs_ = new List<CLoc>();
            foreach (CLoc loc in list)
            {
                locs_.Add(loc.copy());
            }
            flag_ = flag;
        }

        public COrder(string ordType, string utype)
        {
            ordType_ = ordType;
            utype_ = utype;
        }

        public COrder(string ordType, string utype, bool flag)
        {
            ordType_ = ordType;
            utype_ = utype;
            flag_ = flag;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(ordType_ + " ");
            if(locs_ != null)
            {
                for(int i = 0; i < locs_.Count; i++)
                {
                    if (i > 0) sb.Append(", ");
                    sb.Append(locs_[i]);
                }
                sb.Append(" ");
            }
            if(utype_ != null)
                sb.Append(" utype: ").Append(utype_);

            sb.Append(" v: ").Append(value_);
            sb.Append(" flg: ").Append(flag_);
            return sb.ToString();
        }

    }
}

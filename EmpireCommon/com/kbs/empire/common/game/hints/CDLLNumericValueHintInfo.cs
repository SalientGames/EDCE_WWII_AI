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
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.hints
{
    //Numeric Value Hint
	public class CDLLNumericValueHintInfo : CDLLHintInfo
	{
        //Value
        public int value_;
        //Default
        public readonly int def_;
        //Can only be a postive number
	    public readonly bool positiveOnly_;

	    private const string POS_ATTR = "POS";

        public CDLLNumericValueHintInfo(string key, string name, string desc, int def, bool positiveOnly):base(NUMERIC_VALUE_TYPE, key, name, desc)
        {
            def_ = def;
            value_ = def;
            positiveOnly_ = positiveOnly;
        }


	    public override CDLLHintInfo copy()
	    {
            var ret = new CDLLNumericValueHintInfo(key_, name_, desc_, def_, positiveOnly_);
	        ret.value_ = value_;
	        return ret;
	    }

	    public override string getValue()
	    {
	        return Convert.ToString(value_);
	    }

        protected override void encodeAttr(CEncodedObjectOutputBufferI output)
        {
            base.encodeAttr(output);
            output.addAttr(DEF_ATTR, Convert.ToString(def_));
            output.addAttr(VALUE_ATTR, Convert.ToString(value_));
            output.addAttr(POS_ATTR, EncodeUtil.makeBoolString(positiveOnly_));
	    }

        public CDLLNumericValueHintInfo(Dictionary<string, string> attr, CEncodedObjectInputBufferI bin)
            : base(NUMERIC_VALUE_TYPE, attr, bin)
	    {
            def_ = EncodeUtil.parseInt(attr[DEF_ATTR]);
            value_ = EncodeUtil.parseInt(attr[VALUE_ATTR]);
            if (attr.ContainsKey(POS_ATTR))
                positiveOnly_ = EncodeUtil.fromBoolString(attr[POS_ATTR]);
            else
                positiveOnly_ = false;
        }
	}
}

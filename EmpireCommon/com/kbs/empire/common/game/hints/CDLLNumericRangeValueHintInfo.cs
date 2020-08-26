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
    //Numeric Range hint
	public class CDLLNumericRangeValueHintInfo : CDLLHintInfo
	{
        //current value
	    public int value_;
        //default value
	    public int def_;
        //min value
	    public int min_;
        //max value
	    public int max_;

        public CDLLNumericRangeValueHintInfo(string key, string name, string desc, int min, int max, int def):base(NUMERIC_RANGE_VALUE_TYPE, key, name, desc)
        {
            min_ = min;
            max_ = max;
            def_ = def;
            value_ = def;
        }


	    public override CDLLHintInfo copy()
	    {
	        var ret = new CDLLNumericRangeValueHintInfo(key_, name_, desc_, min_, max_, def_);
	        ret.value_ = value_;
	        return ret;
	    }

	    public override string getValue()
	    {
	        return Convert.ToString(value_);
	    }

	    private const string MIN_ATTR = "MIN";
        private const string MAX_ATTR = "MAX";

	    protected override void  encodeAttr(CEncodedObjectOutputBufferI output)
        {
 	        base.encodeAttr(output);

            output.addAttr(DEF_ATTR, Convert.ToString(def_));
            output.addAttr(VALUE_ATTR, Convert.ToString(value_));
            output.addAttr(MIN_ATTR, Convert.ToString(min_));
            output.addAttr(MAX_ATTR, Convert.ToString(max_));
	    }


	    public CDLLNumericRangeValueHintInfo(Dictionary<string, string> attr, CEncodedObjectInputBufferI bin) : base(NUMERIC_RANGE_VALUE_TYPE, attr, bin)
	    {
            def_ = EncodeUtil.parseInt(attr[DEF_ATTR]);
            value_ = EncodeUtil.parseInt(attr[VALUE_ATTR]);
            min_ = EncodeUtil.parseInt(attr[MIN_ATTR]);
            max_ = EncodeUtil.parseInt(attr[MAX_ATTR]);
        }
	}
}

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
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.hints
{
    //Boolean Map/AI Hint
	public class CDLLBoolHintInfo : CDLLHintInfo
	{
	    public readonly bool def_;
	    public bool value_;

        public CDLLBoolHintInfo(string key, string name, string desc, bool def):base(BOOL_TYPE, key, name,desc)
        {
            def_ = def;
            value_ = def;
        }


	    public override CDLLHintInfo copy()
	    {
	        var ret =  new CDLLBoolHintInfo(key_, name_, desc_, def_);
	        ret.value_ = value_;
	        return ret;
	    }

	    public override string getValue()
	    {
	        return EncodeUtil.makeBoolString(value_);
	    }


	    protected override void encodeAttr(CEncodedObjectOutputBufferI output)
	    {
	        base.encodeAttr(output);
            
            output.addAttr(DEF_ATTR, EncodeUtil.makeBoolString(def_));
            output.addAttr(VALUE_ATTR, EncodeUtil.makeBoolString(value_));
        }


        public CDLLBoolHintInfo(Dictionary<string, string> attr, CEncodedObjectInputBufferI bin)
            : base(BOOL_TYPE, attr, bin)
        {
            def_ = EncodeUtil.fromBoolString(attr[DEF_ATTR]);
            value_ = EncodeUtil.fromBoolString(attr[VALUE_ATTR]);
        }
	}
}

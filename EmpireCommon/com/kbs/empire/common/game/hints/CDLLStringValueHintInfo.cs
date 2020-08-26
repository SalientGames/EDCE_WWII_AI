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
    //String Hint
    public class CDLLStringValueHintInfo : CDLLHintInfo
    {
        //Value
        public string value_;
        //default
        public readonly string def_;

        public CDLLStringValueHintInfo(string key, string name, string desc, string def):base(STRING_VALUE_TYPE, key, name, desc)
        {
            def_ = def;
            value_ = def;
        }


	    public override CDLLHintInfo copy()
	    {
            var ret = new CDLLStringValueHintInfo(key_, name_, desc_, def_);
	        ret.value_ = value_;
	        return ret;
	    }

	    public override string getValue()
	    {
	        return value_;
	    }

        protected override void  encodeChildren(CEncodedObjectOutputBufferI output)
        {
            base.encodeChildren(output);
            output.addTextObject(DEF_ATTR, def_);
            output.addTextObject(VALUE_ATTR, value_);
        }

        public CDLLStringValueHintInfo(Dictionary<string, string> attr, CEncodedObjectInputBufferI bin)
            : base(STRING_VALUE_TYPE, attr, bin)
        {
            def_ = bin.getObjectText(DEF_ATTR);
            value_ = bin.getObjectText(VALUE_ATTR);
        }
    }
}

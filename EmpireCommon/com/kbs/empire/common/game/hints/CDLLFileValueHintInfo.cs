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
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.xml;

namespace EmpireCommon.com.kbs.empire.common.game.hints
{
    //essentially a string indicating a file path
    public class CDLLFileValueHintInfo : CDLLHintInfo
    {
        public string value_;
        public readonly string def_;


        public CDLLFileValueHintInfo(string key, string name, string desc, string def)
            : base(FILE_VALUE_TYPE, key, name, desc)
        {
            if (string.IsNullOrEmpty(def))
                def_ = EmpireCC.NULL_STRING; 
            else
                def_ = def;

            value_ = def_;
        }

	    public override CDLLHintInfo copy()
	    {
            var ret = new CDLLFileValueHintInfo(key_, name_, desc_, def_);
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

        public CDLLFileValueHintInfo(Dictionary<string, string> attr, CEncodedObjectInputBufferI bin)
            : base(FILE_VALUE_TYPE, attr, bin)
        {
            def_ = bin.getObjectText(DEF_ATTR);
            value_ = bin.getObjectText(VALUE_ATTR);
        }
    }
}

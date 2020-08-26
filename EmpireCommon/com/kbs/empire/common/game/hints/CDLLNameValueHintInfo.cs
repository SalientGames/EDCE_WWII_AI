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
    //Name Value Pair Hint
	public class CDLLNameValueHintInfo : CDLLHintInfo
	{
	    public readonly Dictionary<string, string> set_;
	    public readonly string defkey_;
	    public string value_;

        public CDLLNameValueHintInfo(string key, string name, string desc, Dictionary<string, string> set, string defkey):base(NAME_VALUE_SET, key, name, desc)
        {
            set_ = new Dictionary<string, string>(set);
            defkey_ = defkey;
            value_ = defkey;
        }


	    public override CDLLHintInfo copy()
	    {
	        var ret = new CDLLNameValueHintInfo(key_, name_, desc_, new Dictionary<string, string>(set_), defkey_);
	        ret.value_ = value_;
	        return ret;
	    }

	    public override string getValue()
	    {
	        return value_;
	    }

        private const string SETS_TAG = "SS";
        private const string SET_TAG = "S";
	    private const string KEY_TAG = "K";
	    private const string VALUE_TAG = "V";
	    protected override void  encodeAttr(CEncodedObjectOutputBufferI output)
	    {
	        base.encodeAttr(output);


	        output.addAttr(DEF_ATTR, defkey_);
	        output.addAttr(VALUE_ATTR, value_);
	    }
        protected override void  encodeChildren(CEncodedObjectOutputBufferI output)
        {
 	        base.encodeChildren(output);
    
            output.openObject(SETS_TAG);
   
	        foreach (KeyValuePair<string, string> kp in set_)
	        {
	            output.openObject(SET_TAG);
                output.addTextObject(KEY_TAG, kp.Key);
                output.addTextObject(VALUE_TAG, kp.Value);
                output.objectEnd();
	        }

            output.objectEnd();
	    }

	    public CDLLNameValueHintInfo(Dictionary<string, string> attr, CEncodedObjectInputBufferI bin) : base(NAME_VALUE_SET, attr, bin)
	    {
            defkey_ = attr[DEF_ATTR];
            value_ = attr[VALUE_ATTR];

            bin.nextTag(SETS_TAG);

            if (bin.hasChildren())
            {
                while (!bin.reachedEndTag(SETS_TAG))
                {
                    bin.nextTag(SET_TAG);
                    string k = bin.getObjectText(KEY_TAG);
                    string v = bin.getObjectText(VALUE_TAG);
                    set_.Add(k, v);
                    bin.endTag(SET_TAG);
                    
                }
            }
            bin.endTag(SETS_TAG);
	    }
	}
}

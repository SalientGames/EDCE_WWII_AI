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
using EmpireCommon.com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.hints
{
    //Base class for Hint info, Defines Types and gives a description
    public abstract class CDLLHintInfo
	{
	    public const string BOOL_TYPE = "BT";
	    public const string NAME_VALUE_SET = "NVS";
        public const string STRING_VALUE_TYPE = "SVT";
        public const string FILE_VALUE_TYPE = "FILT";
	    public const string NUMERIC_VALUE_TYPE = "NUVT";
	    public const string NUMERIC_RANGE_VALUE_TYPE = "NRVT";
        public const string CONTAINER_TYPE = "CONT";
        public const string RADIO_CONTAINER_TYPE = "RCONT";
        public const string LOCKED_CONTAINER_TYPE = "LCONT";

	    public readonly string type_;
	    public readonly string key_;
	    public readonly string name_;
	    public readonly string desc_;

        protected CDLLHintInfo(string type, string key, string name, string desc)
        {
            type_ = type;
            key_ = key;
            name_ = name;
            desc_ = desc;
        }

	    public abstract CDLLHintInfo copy();


	    public abstract string getValue();

        private const string KTAG = "IK";
        private const string NTAG = "IN";
        private const string DTAG = "ID";

        //used in derived classes
	    protected const string DEF_ATTR = "DEF";
        protected const string VALUE_ATTR = "VAL";

        public static CDLLHintInfo decode(CEncodedObjectInputBufferI bin)
        {
            string tag = bin.thisTag();
            Dictionary<string, string> attr = bin.getAttributes();

            CDLLHintInfo ret = null;
            switch(tag)
            {
                case BOOL_TYPE:
                    ret = new CDLLBoolHintInfo(attr, bin);
                    break;
                case NAME_VALUE_SET:
                    ret =  new CDLLNameValueHintInfo(attr, bin);
                    break;
                case NUMERIC_RANGE_VALUE_TYPE:
                    ret = new CDLLNumericRangeValueHintInfo(attr, bin);
                    break;
                case NUMERIC_VALUE_TYPE:
                    ret = new CDLLNumericValueHintInfo(attr, bin);
                    break;
                case STRING_VALUE_TYPE:
                    ret = new CDLLStringValueHintInfo(attr, bin);
                    break;
                case FILE_VALUE_TYPE:
                    ret = new CDLLFileValueHintInfo(attr, bin);
                    break;
                case CONTAINER_TYPE:
                    ret = new CDLLContainerHintInfo(attr, bin);
                    break;
                case LOCKED_CONTAINER_TYPE:
                    ret = new CDLLLockableHintInfo(attr, bin);
                    break;
                case RADIO_CONTAINER_TYPE:
                    ret = new CDLLRadioContainerHintInfo(attr, bin);
                    break;
                default:
                    throw new Exception("Unknown decode type " + tag);
            }
            bin.endTag(tag);
            return ret;
        }
        // ReSharper disable UnusedParameter.Local
        protected CDLLHintInfo(string type, Dictionary<string, string> attr, CEncodedObjectInputBufferI bin)
        // ReSharper restore UnusedParameter.Local
        {
            type_ = type;
            key_ = bin.getObjectText(KTAG);
            name_ = bin.getObjectText(NTAG);
            desc_ = bin.getObjectText(DTAG);
        }
        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(type_);
                encodeAttr(output);
                encodeChildren(output);
            output.objectEnd();
        }

        protected virtual void encodeAttr(CEncodedObjectOutputBufferI output)
	    {
            
	    }

        protected virtual void encodeChildren(CEncodedObjectOutputBufferI output)
        {
            output.addTextObject(KTAG, key_);
            output.addTextObject(NTAG, name_);
            output.addTextObject(DTAG, desc_);
        }

        public virtual string getValue(string key)
        {
            if (key == key_)
                return getValue();
            return null;
        }
	}
}

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

using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.hints
{

	public class CDLLInfo
	{
	    public readonly string key_;
	    public readonly string name_ ;
	    public readonly string desc_;
	    public readonly string version_;

        public CDLLInfo(string key, string name, string desc, string version)
        {
            key_ = key;
            name_ = name;
            desc_ = desc;
            version_ = version;
        }


	    public const string TAG = "MVERSCD";
	    public const string KTAG = "K";
	    public const string NTAG = "N";
	    public const string DTAG = "D";
        public const string VTAG = "V";

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addTextObject(KTAG, key_);
            output.addTextObject(NTAG, name_);
            output.addTextObject(DTAG, desc_);
            output.addTextObject(VTAG, version_);
            output.objectEnd();
        }

	    public CDLLInfo(CEncodedObjectInputBufferI bin)
	    {
	        bin.nextTag(TAG);
	        key_ = bin.getObjectText(KTAG);
	        name_ = bin.getObjectText(NTAG);
	        desc_ = bin.getObjectText(DTAG);
            version_ = bin.getObjectText(VTAG);
            bin.endTag(TAG);
	    }

	}
}

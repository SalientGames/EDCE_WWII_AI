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
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.player
{
    //representation of you and your enemies in a game
    public class CPlayer
	{
	    public const string TAGS = "CPS";
	
	    //type of player
	    public readonly string type_;
        //position in setup
        public readonly int originalPosition_;
        //actual position (this could be different from original in Scenarios 
        //where a player is marked as "none")
	    public readonly int position_;
        //buypoints
        public int buypoints_;
        //what buypoints have been spent
        public int buypointsSpent_;

        public int score_;
	
        //Handicap Levels - 100 is neutral/none
        //Production
	    public readonly int pcap_;
        //Combat
	    public readonly int ccap_;
        //Neutral
        public readonly int ncap_;
	
	    //data which may change
        //the player name
        public string pname_ = null;

        //still alive
	    public bool living_;
        //why did this player die
	    public readonly string deadReason_;

        private const string TAG = "CP";
        
        private const string ORIGINAL_POSITION = "OP";
        private const string POSITION = "POS";
        private const string BUYPOINTS = "BP";
        private const string BUYPOINTSSPENT = "BPS";
        private const string SCORE = "SCR";
        private const string PCAP = "PCP";
        private const string CCAP = "CCP";
        private const string NCAP = "NCP";
        private const string LIVING = "LV";


        private const string TYPE_TAG = "TYPE";
        private const string PNAME_TAG = "PNM";
        private const string DEAD_REASON_TAG = "DR";


        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addAttr(ORIGINAL_POSITION, Convert.ToString(originalPosition_));
            output.addAttr(POSITION, Convert.ToString(position_));
            output.addAttr(BUYPOINTS, Convert.ToString(buypoints_));
            output.addAttr(BUYPOINTSSPENT, Convert.ToString(buypointsSpent_));
            output.addAttr(SCORE, Convert.ToString(score_));
            output.addAttr(PCAP, Convert.ToString(pcap_));
            output.addAttr(CCAP, Convert.ToString(ccap_));
            output.addAttr(NCAP, Convert.ToString(ncap_));
            output.addAttr(LIVING, EncodeUtil.makeBoolString(living_));

            output.addTextObject(TYPE_TAG, type_);
            if(pname_ != null)
                output.addTextObject(PNAME_TAG, pname_);
            if(deadReason_ != null)
                output.addTextObject(DEAD_REASON_TAG, deadReason_);

            output.objectEnd();
        }
        public CPlayer(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();
            originalPosition_ = EncodeUtil.parseInt(A[ORIGINAL_POSITION]);
            position_ = EncodeUtil.parseInt(A[POSITION]);
            buypoints_ = EncodeUtil.parseInt(A[BUYPOINTS]);
            buypointsSpent_ = EncodeUtil.parseInt(A[BUYPOINTSSPENT]);
            score_ = EncodeUtil.parseInt(A[SCORE]);
            pcap_ = EncodeUtil.parseInt(A[PCAP]);
            ccap_ = EncodeUtil.parseInt(A[CCAP]);
            ncap_ = EncodeUtil.parseInt(A[NCAP]);
            living_ = EncodeUtil.fromBoolString(A[LIVING]);

            type_ = bin.getObjectText(TYPE_TAG);
            if (bin.thisTag() == PNAME_TAG)
                pname_ = bin.getObjectText(PNAME_TAG);
            else
                pname_ = null;
            if (bin.thisTag() == DEAD_REASON_TAG)
                deadReason_ = bin.getObjectText(DEAD_REASON_TAG);
            else
                deadReason_ = null;

            bin.endTag(TAG);
        }
	
	    public CPlayer(
			    string type,
                int orig, 
			    int pos, 
                int buypoints,
                int buypointsSpent,
                int score,
			    int pcap, 
			    int ccap,
                int ncap,
			    string pname,
			    bool living,
			    string deadReason
			    )
	    {
		    type_ = type;
            originalPosition_ = orig;//used in scenarios
		    position_ = pos;
	        buypoints_ = buypoints;
	        buypointsSpent_ = buypointsSpent;
	        score_ = score;
		    pcap_ = pcap;
		    ccap_ = ccap;
	        ncap_ = ncap;
		    pname_ = pname;
		    living_ = living;
		    deadReason_ = deadReason;
	    }

        public bool isEvilHuman()
        {
            return (type_ == EmpireCC.EVIL_HUMAN);
        }
	}
}

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

namespace com.kbs.empire.worldbuild.common.proc
{
    //Map Params for World Building
    public class CGameMapParameters
    {
        //number of players
        public int numPositions_ = 0;

        //want capitals in game
        public bool needCapitals_ = false;
        //want important cities in game
        public bool needImportant_ = false;
        //number of important cities if needed
        public int numImportant_ = 1;
        //per positin starting city counts
        public int[] numStartingCities_ = null;

        public CGameMapParameters() { }

        public CGameMapParameters copy()
        {
            var ret = new CGameMapParameters();
            ret.numPositions_ = numPositions_;
            if (numStartingCities_ != null)
            {
                ret.numStartingCities_ = new int[numStartingCities_.Length];
                for (int i = 0; i < numStartingCities_.Length; i++)
                    ret.numStartingCities_[i] = numStartingCities_[i];
            }
            ret.needCapitals_ = needCapitals_;
            ret.needImportant_ = needImportant_;
            ret.numImportant_ = numImportant_;

            return ret;
        }


        public const string TAG = "GMPRMS";

        public const string NUM_POSITION_ATTR = "NPOS";
        public const string NEEDCAP_ATTR = "NCAP";
        public const string NEEDIMPORT_ATTR = "NIMP";
        public const string NUMIMPORT_ATTR = "NUMIMP";

        public const string NUMSTARTCI_TAGS = "NUMSCIS";
        public const string NUMSTARTCI_TAG = "NUMSCI";
        public const string POS_ATTR = "P";
        public const string VALUE_ATTR = "V";

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addAttr(NUM_POSITION_ATTR, Convert.ToString(numPositions_));
            output.addAttr(NEEDCAP_ATTR, EncodeUtil.makeBoolString(needCapitals_));
            output.addAttr(NEEDIMPORT_ATTR, EncodeUtil.makeBoolString(needImportant_));
            output.addAttr(NUMIMPORT_ATTR, Convert.ToString(numImportant_));

            output.openObject(NUMSTARTCI_TAGS);
            if (numStartingCities_ != null)
            {
                for (int i = 0; i < numStartingCities_.Length; i++)
                {
                    output.openObject(NUMSTARTCI_TAG);
                    output.addAttr(POS_ATTR, Convert.ToString(i));
                    output.addAttr(VALUE_ATTR, Convert.ToString(numStartingCities_[i]));
                    output.objectEnd();
                }
            }
            output.objectEnd();

            output.objectEnd();
        }

        public CGameMapParameters(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);

            Dictionary<string, string> attr = bin.getAttributes();

            numPositions_ = EncodeUtil.parseInt(attr[NUM_POSITION_ATTR]);
            needCapitals_ = EncodeUtil.fromBoolString(attr[NEEDCAP_ATTR]);
            needImportant_ = EncodeUtil.fromBoolString(attr[NEEDIMPORT_ATTR]);
            numImportant_ = EncodeUtil.parseInt(attr[NUMIMPORT_ATTR]);

            numStartingCities_ = null;
            var plist = new List<KeyValuePair<int, int>>();
            bin.nextTag(NUMSTARTCI_TAGS);
            if (bin.hasChildren())
            {
                bin.firstChild();
                while (!bin.reachedEndTag(NUMSTARTCI_TAGS))
                {
                    Dictionary<string, string> attrc = bin.getAttributes();
                    int index = EncodeUtil.parseInt(attrc[POS_ATTR]);
                    int v = EncodeUtil.parseInt(attrc[VALUE_ATTR]);
                    plist.Add(new KeyValuePair<int, int>(index, v));
                    bin.endTag(NUMSTARTCI_TAG);
                }
            }
            if (plist.Count > 0)
            {
                numStartingCities_ = new int[plist.Count];
                foreach (KeyValuePair<int, int> kp in plist)
                    numStartingCities_[kp.Key] = kp.Value;
            }
            bin.endTag(NUMSTARTCI_TAGS);

            bin.endTag(TAG);
        }

    }
}

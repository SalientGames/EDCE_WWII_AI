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

namespace EmpireCommon.com.kbs.empire.common.game.rules
{
    public class CVictoryConditions
    {
        //Annihilation Victory
        //True means last ma standing
        public readonly bool annihilationVictory_;
        //PercentageOfCities
        //percentage between 0-100
        //value of 0 means it is not a condition
        //the duration is the number of consecutive turns
        //that the minimum level needs to be  reached
        public readonly int percentageCitiesV_;
        public readonly int percentageCitiesVDuration_;
        //Important Cities
        //the duration is the number of consecutive turns
        //that the minimum level needs to be  reached
        public readonly int numImportantV_;
        public readonly int numImportantVDuration_;
        //Capital Kill
        public readonly bool capitalKill_;
        //Regicide
        //The number of general units that will be in play
        public readonly int regicideV_;

        public CVictoryConditions(
            bool annihilationVictory, 
            int percentageCitiesV, 
            int percentageCitiesVDuration, 
            int numImportantV, 
            int numImportantVDuration, 
            bool capitalKill, 
            int regicideV)
        {
            annihilationVictory_ = annihilationVictory;
            percentageCitiesV_ = percentageCitiesV;
            percentageCitiesVDuration_ = percentageCitiesVDuration;
            numImportantV_ = numImportantV;
            numImportantVDuration_ = numImportantVDuration;
            capitalKill_ = capitalKill;
            regicideV_ = regicideV;
        }

        private const string TAG = "VIC";
        private const string ANNIHILATION = "ANN";
        private const string PERCITIESVALUE = "PCV";
        private const string PERCITIESDUR = "PCD";
        private const string NUMIMPVALUE = "NIV";
        private const string NUMIMPDUR = "NID";
        private const string CAPKILL = "CK";
        private const string REGICIDE = "RV";

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addAttr(ANNIHILATION, EncodeUtil.makeBoolString(annihilationVictory_));
            output.addAttr(PERCITIESVALUE, Convert.ToString(percentageCitiesV_));
            output.addAttr(PERCITIESDUR, Convert.ToString(percentageCitiesVDuration_));
            output.addAttr(NUMIMPVALUE, Convert.ToString(numImportantV_));
            output.addAttr(NUMIMPDUR, Convert.ToString(numImportantVDuration_));
            output.addAttr(CAPKILL, EncodeUtil.makeBoolString(capitalKill_));
            output.addAttr(REGICIDE, Convert.ToString(regicideV_));
            output.objectEnd();
        }

        public CVictoryConditions(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();
            annihilationVictory_ = EncodeUtil.fromBoolString(A[ANNIHILATION]);
            percentageCitiesV_ = EncodeUtil.parseInt(A[PERCITIESVALUE]);
            percentageCitiesVDuration_ = EncodeUtil.parseInt(A[PERCITIESDUR]);
            numImportantV_ = EncodeUtil.parseInt(A[NUMIMPVALUE]);
            numImportantVDuration_ = EncodeUtil.parseInt(A[NUMIMPDUR]);
            capitalKill_ = EncodeUtil.fromBoolString(A[CAPKILL]);
            regicideV_ = EncodeUtil.parseInt(A[REGICIDE]);
            bin.endTag(TAG);
        }
    }
}

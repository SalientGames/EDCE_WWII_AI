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
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.game.proc;
using com.kbs.empire.common.util.log;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.unit
{
    //This class reprensts both supply producers and cities
    public class CProducerUnit : CUnit
    {
        /////////////////////////////////////////////
        //Production Unit Type
        public string producing_ = EmpireCC.C_NO_UNIT_TYPE;
        //Specialty Unit Type
        public string specialty_ = EmpireCC.C_NO_UNIT_TYPE;
        //Turns till completion of current production
        public int turnsToCompletion_ = 0;
        //producer's efficiency
        public int efficiency_ = 100;

        //Current Supply Storage
        public uint supplyStore_ = 0u;
        //Minimum Storage Value
        public uint minStore_ = 0u;
        //Amount of scrap
        public uint scrapVal_ = 0u;

        //Allow consuming Supplies For Drain Balance
        public bool consumeSupplyForDrain_ = false;
        //Priority ranking for consumption
        public int consumePriority_ = 0;
        //Automatically switch between supply production and drain protection
        public bool autoDrainSupply_ = false;

        //if city, is it a capital?
        public bool isCapital_ = false;
        //if city, is it important?
        public bool isImportant_ = false;


        private const string TAG = "CPU";
        private const string PRODUCING = "PROD";
        private const string SPECIALTY = "SPEC";
        private const string TURNSTOCOMPLETE = "TTC";
        private const string EFFICIENCY = "EFF";
        private const string SUPPLYSTORE = "SS";
        private const string MINSTORE = "MS";
        private const string SCRAPVAL = "SV";
        private const string CONSUMESUPPFORDRAIN = "CSD";
        private const string CONSUMEPRIORITY = "CPR";
        private const string AUTODRAIN = "AD";
        private const string CAPITAL = "CAP";
        private const string IMPORTANT = "IMP";

        protected override void encodeAttributes(CEncodedObjectOutputBufferI output)
        {
            base.encodeAttributes(output);

            output.addAttr(PRODUCING, producing_);
            output.addAttr(SPECIALTY, specialty_);
            output.addAttr(TURNSTOCOMPLETE, Convert.ToString(turnsToCompletion_));
            output.addAttr(EFFICIENCY, Convert.ToString(efficiency_));
            output.addAttr(SUPPLYSTORE, Convert.ToString(supplyStore_));
            output.addAttr(MINSTORE, Convert.ToString(minStore_));
            output.addAttr(SCRAPVAL, Convert.ToString(scrapVal_));
            output.addAttr(CONSUMESUPPFORDRAIN, EncodeUtil.makeBoolString(consumeSupplyForDrain_));
            output.addAttr(CONSUMEPRIORITY, Convert.ToString(consumePriority_));
            output.addAttr(AUTODRAIN, EncodeUtil.makeBoolString(autoDrainSupply_));
            output.addAttr(CAPITAL, EncodeUtil.makeBoolString(isCapital_));
            output.addAttr(IMPORTANT, EncodeUtil.makeBoolString(isImportant_));
        }

        private CProducerUnit(Dictionary<string, string> A, CEncodedObjectInputBufferI bin, AIQueryI query)
            : base(A, bin, query)
        {
            producing_ = A[PRODUCING];
            specialty_ = A[SPECIALTY];
            turnsToCompletion_ = EncodeUtil.parseInt(A[TURNSTOCOMPLETE]);
            efficiency_ = EncodeUtil.parseInt(A[EFFICIENCY]);
            supplyStore_ = EncodeUtil.parseUInt(A[SUPPLYSTORE]);
            minStore_ = EncodeUtil.parseUInt(A[MINSTORE]);
            scrapVal_ = EncodeUtil.parseUInt(A[SCRAPVAL]);
            consumeSupplyForDrain_ = EncodeUtil.fromBoolString(A[CONSUMESUPPFORDRAIN]);
            consumePriority_ = EncodeUtil.parseInt(A[CONSUMEPRIORITY]);
            autoDrainSupply_ = EncodeUtil.fromBoolString(A[AUTODRAIN]);
            isCapital_ = EncodeUtil.fromBoolString(A[CAPITAL]);
            isImportant_ = EncodeUtil.fromBoolString(A[IMPORTANT]);
        }
        
        public static void encodeCProducerUnit(CProducerUnit p, CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);   
            p.encodeAttributes(output);
            p.encodeChildren(output);
            output.objectEnd();
        }
        public static CProducerUnit decodeCProducerUnit(CEncodedObjectInputBufferI bin, AIQueryI query)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();
            var ret = new CProducerUnit(A, bin, query);
            bin.endTag(TAG);
            return ret;
        }

        
        public CProducerUnit(string utype, uint gid, CLoc loc, UnitQueryI entry) : base(utype, gid, loc, entry)
        {
            
        }

        public override bool update(string attr, string value, CSubLog logger)
        {
            if (base.update(attr, value, logger))
                return true;

            switch (attr)
            {
                case CUnitConstants.PRODUCING:
                    producing_ = value;
                    return true;

                case CUnitConstants.SPECIALTY:
                    {
                        specialty_ = value;
                        return true;
                    }
                case CUnitConstants.EFFICIENCY:
                    {

                        efficiency_ = EncodeUtil.parseInt(value);
                        return true;
                    }
                case CUnitConstants.TTC:
                    {
                        turnsToCompletion_ = EncodeUtil.parseInt(value);
                        return true;
                    }
                case CUnitConstants.SUPPLY_STORE:
                    {
                        supplyStore_ = EncodeUtil.parseUInt(value);
                        return true;
                    }
                case CUnitConstants.MIN_STORE:
                    {
                        minStore_ = EncodeUtil.parseUInt(value);
                        return true;
                    }
                case CUnitConstants.SCRAPVAL:
                    {
                        scrapVal_ = EncodeUtil.parseUInt(value);
                        return true;
                    }
                case CUnitConstants.AUTOSUPPLYDRAIN:
                    {
                        autoDrainSupply_ = EncodeUtil.fromBoolString(value);
                        return true;
                    }
                case CUnitConstants.CONS_FOR_DRAIN:
                    {
                        consumeSupplyForDrain_ = EncodeUtil.fromBoolString(value);
                        return true;
                    }
                case CUnitConstants.CONS_PRIORITY:
                    {
                        consumePriority_ = EncodeUtil.parseInt(value);
                        return true;
                    }
            }



            return false;

        }

        public override string ToString()
        {
            return (base.ToString() + " prd: " + producing_ + " ttc: " + Convert.ToString(turnsToCompletion_));
        }

    }
}

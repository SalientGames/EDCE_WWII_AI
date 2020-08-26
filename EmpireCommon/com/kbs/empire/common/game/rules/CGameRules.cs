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
using System.Text;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.rules
{
    public class CGameRules
    {
        public readonly uint stackCount_;
        public readonly bool useExploration_;
        public readonly bool useCityEff_;
        public readonly bool useContinue_;
        public readonly bool useSpec_;
        public readonly bool useDrain_;
        public readonly bool useRoads_;
        public readonly bool useSupply_;
        public readonly bool useMines_;
        public readonly bool useResources_;
        public readonly bool useNukes_;
        public readonly bool useDigin_;
        public readonly bool useExperience_;
        public readonly bool useDefFire_;
        public readonly bool useRangeFire_;
        public readonly bool useCrippled_;
        public readonly bool useUnitScrap_;
        public readonly bool useUnitNames_;
        public readonly bool useDefTer_;
        public readonly bool useHarshSupply_;
        public readonly bool useLimitedResources_;

        private const string TAG = "CGRL";
        private const string STACK_COUNT = "SC";
        private const string EXPLORE = "EXPL";
        private const string CITYEFF = "CEFF";
        private const string CONTINUE = "CON";
        private const string SPEC = "SPEC";
        private const string DRAIN = "DRN";
        private const string ROADS = "RDS";
        private const string SUPPLY = "SUP";
        private const string MINES = "MNS";
        private const string RESOURCES = "RES";
        private const string NUKES = "NK";
        private const string DIGIN = "DI";
        private const string EXPERIENCE = "EXPER";
        private const string DEFFIRE = "DF";
        private const string RANGEFIRE = "RF";
        private const string CRIPPLED = "CRIP";
        private const string SCRAP = "SCR";
        private const string UNITNAMES = "UNM";
        private const string DEFTER = "DEFTER";
        private const string HARSHSUPPLY = "HSUP";
        private const string LIMITEDREOURCES = "LRES";

        public CGameRules(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();
            stackCount_ = EncodeUtil.parseUInt(A[STACK_COUNT]);
            useExploration_ = EncodeUtil.fromBoolString(A[EXPLORE]);
            useCityEff_ = EncodeUtil.fromBoolString(A[CITYEFF]);
            useContinue_ = EncodeUtil.fromBoolString(A[CONTINUE]);
            useSpec_ = EncodeUtil.fromBoolString(A[SPEC]);
            useDrain_ = EncodeUtil.fromBoolString(A[DRAIN]);
            useRoads_ = EncodeUtil.fromBoolString(A[ROADS]);
            useSupply_ = EncodeUtil.fromBoolString(A[SUPPLY]);
            useMines_ = EncodeUtil.fromBoolString(A[MINES]);
            useResources_ = EncodeUtil.fromBoolString(A[RESOURCES]);
            useNukes_ = EncodeUtil.fromBoolString(A[NUKES]);
            useDigin_ = EncodeUtil.fromBoolString(A[DIGIN]);
            useExperience_ = EncodeUtil.fromBoolString(A[EXPERIENCE]);
            useUnitNames_ = EncodeUtil.fromBoolString(A[UNITNAMES]);
            useDefFire_ = EncodeUtil.fromBoolString(A[DEFFIRE]);
            useRangeFire_ = EncodeUtil.fromBoolString(A[RANGEFIRE]);
            useCrippled_ = EncodeUtil.fromBoolString(A[CRIPPLED]);
            useUnitScrap_ = EncodeUtil.fromBoolString(A[SCRAP]);
            useDefTer_ = EncodeUtil.fromBoolString(A[DEFTER]);
            useHarshSupply_ = EncodeUtil.fromBoolString(A[HARSHSUPPLY]);
            useLimitedResources_ = EncodeUtil.fromBoolString(A[LIMITEDREOURCES]);
            bin.endTag(TAG);
        }

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addAttr(STACK_COUNT, Convert.ToString(stackCount_));
            output.addAttr(EXPLORE, EncodeUtil.makeBoolString(useExploration_));
            output.addAttr(CITYEFF, EncodeUtil.makeBoolString(useCityEff_));
            output.addAttr(CONTINUE, EncodeUtil.makeBoolString(useContinue_));
            output.addAttr(SPEC, EncodeUtil.makeBoolString(useSpec_));
            output.addAttr(DRAIN, EncodeUtil.makeBoolString(useDrain_));
            output.addAttr(ROADS, EncodeUtil.makeBoolString(useRoads_));
            output.addAttr(SUPPLY, EncodeUtil.makeBoolString(useSupply_));
            output.addAttr(MINES, EncodeUtil.makeBoolString(useMines_));
            output.addAttr(RESOURCES, EncodeUtil.makeBoolString(useResources_));
            output.addAttr(NUKES, EncodeUtil.makeBoolString(useNukes_));
            output.addAttr(DIGIN, EncodeUtil.makeBoolString(useDigin_));
            output.addAttr(EXPERIENCE, EncodeUtil.makeBoolString(useExperience_));
            output.addAttr(UNITNAMES, EncodeUtil.makeBoolString(useUnitNames_));
            output.addAttr(DEFFIRE, EncodeUtil.makeBoolString(useDefFire_));
            output.addAttr(RANGEFIRE, EncodeUtil.makeBoolString(useRangeFire_));
            output.addAttr(CRIPPLED, EncodeUtil.makeBoolString(useCrippled_));
            output.addAttr(SCRAP, EncodeUtil.makeBoolString(useUnitScrap_));
            output.addAttr(DEFTER, EncodeUtil.makeBoolString(useDefTer_));
            output.addAttr(HARSHSUPPLY, EncodeUtil.makeBoolString(useHarshSupply_));
            output.addAttr(LIMITEDREOURCES, EncodeUtil.makeBoolString(useLimitedResources_));
            output.objectEnd();
        }


        public CGameRules(
            uint stackCount, 
            bool useExploration, 
            bool useCityEff, 
            bool useContinue, 
            bool useSpec, 
            bool useDrain, 
            bool useRoads, 
            bool useSupply, 
            bool useMines, 
            bool useResources, 
            bool useNukes, 
            bool useDigin, 
            bool useExperience, 
            bool useDefFire, 
            bool useRangeFire, 
            bool useCrippled, 
            bool useUnitScrap, 
            bool useUnitNames, 
            bool useDefTer,
            bool useHarshSupply,
            bool useLimitedResources)
	    {
	        stackCount_ = stackCount;
	        useExploration_ = useExploration;
            useCityEff_ = useCityEff;
            useContinue_ = useContinue;
            useSpec_ = useSpec;
            useDrain_ = useDrain;
            useRoads_ = useRoads;
            useSupply_ = useSupply;
            useMines_ = useMines;
            useResources_ = useResources;
            useNukes_ = useNukes;
            useDigin_ = useDigin;
            useExperience_ = useExperience;
            useDefFire_ = useDefFire;
            useRangeFire_ = useRangeFire;
            useCrippled_ = useCrippled;
            useUnitScrap_ = useUnitScrap;
            useUnitNames_ = useUnitNames;
            useDefTer_ = useDefTer;
            useHarshSupply_ = useHarshSupply;
            useLimitedResources_ = useLimitedResources;
	    }

        public override string ToString()
        {
            var sb = new StringBuilder("Game Rules ==>");

            sb.Append(" Stack Count : ").Append(stackCount_);
            sb.Append(" Use Expl: ").Append(useExploration_);
            sb.Append(" useCityEff: ").Append(useCityEff_);
            sb.Append(" useContinue: ").Append(useContinue_);
            sb.Append(" useSpec: ").Append(useSpec_);
            sb.Append(" useDrain: ").Append(useDrain_);
            sb.Append(" useRoads: ").Append(useRoads_);
            sb.Append(" useSupply: ").Append(useSupply_);
            sb.Append(" useHarshSupply: ").Append(useHarshSupply_);
            sb.Append(" useLimitedRes: ").Append(useLimitedResources_);
            sb.Append(" useMines: ").Append(useMines_);
            sb.Append(" useResources: ").Append(useResources_);
            sb.Append(" useNukes: ").Append(useNukes_);
            sb.Append(" useDigin: ").Append(useDigin_);
            sb.Append(" useExperience: ").Append(useExperience_);
            sb.Append(" useDefFire: ").Append(useDefFire_);
            sb.Append(" useRangedFire: ").Append(useRangeFire_);
            sb.Append(" useCrippled: ").Append(useCrippled_);
            sb.Append(" useUnitScrap: ").Append(useUnitScrap_);
            sb.Append(" useUnitNames: ").Append(useUnitNames_);
            sb.Append(" useDefTer: ").Append(useDefTer_);

            return sb.ToString();
        }

    }
}

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
using com.kbs.empire.common.game.order;
using com.kbs.empire.common.game.proc;
using com.kbs.empire.common.util.log;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.unit
{
    //this class represents the base class of all units and cities
    public class CUnit
    {

        /////////////////////////////////////////////
        //what is the unit type
        public readonly string utype_;
        public readonly UnitQueryI entry_;

        /////////////////////////////////////////////
        //location
        protected readonly CLoc loc_;
        public CLoc getLoc() { return loc_; }
        public void setLoc(int x, int y) { loc_.x = (short)x; loc_.y = (short)y; }
        public void setLoc(CLoc l) { loc_.x = l.x; loc_.y = l.y; }

        /////////////////////////////////////////////
        //gid - unique game identifier
        public readonly uint gid_;

        /////////////////////////////////////////////
        //who owns the unit?
        public int owner_;

        /////////////////////////////////////////////
        //layer stuff
        public uint stackLayer_ = 0u; //SUB Level and ORB Level are always 0. Ground == 0, Air == 1 in "STACK"
        public string level_;
        public bool landed_ = true;
        public bool inReentry_ = false;

        /////////////////////////////////////////////
        //unit name
        public string name_ = "";
        //host id
        public uint host_ = EmpireCC.NO_UNIT_ID;

        ///////////////////////////////////////////////////////////
        //units in hold (if carried/transported)
        public List<uint> cunits_ = new List<uint>();
        /////////////////////////////////////////////
        //turn used in spot
        public int turn_ = 0;

        /////////////////////////////////////////////
        //current movement values
        public int rmvr_ = 0; //range
        public int rmvs_ = 0; //mv points

        /////////////////////////////////////////////
        //RangeFire
        public bool fired_ = false; //has the unit fired

        /////////////////////////////////////////////
        //Armed
        public bool armed_ = false; //is the unit armed

        /////////////////////////////////////////////
        //damage
        public int dmg_ = 0; //hits unit has taken

        ////////////////////////////////////////////
        //orders
        public COrder ord_ = new COrder(COrderConstants.ORD_NO_ORDER); //unit orders

        ////////////////////////////////////////////
        //short fuel
        public bool shortFuel_ = false; //is this unit short on fuel

        ////////////////////////////////////////////
        //saw enemy
        public bool sawEnemy_ = false; //did this unit see an enemy

        ////////////////////////////////////////////
        //Dug in
        public bool dugIn_ = false;//is this unit dug in

        //Experience
        public uint experience_ = 0u;
        public string readiness_ = CUnitConstants.RD_READY;
        public string expType_ = CUnitConstants.UE_GREEN;

        private const string TAG = "CUNIT";
        private const string UTYPE = "TYP";
        private const string LOC = "LOC";
        private const string GID = "GID";
        private const string OWNER = "O";
        private const string STACKLAYER = "SL";
        private const string LEVEL = "LVL";
        private const string LANDED = "LND";
        private const string INREENTRY = "IRE";
        private const string NAME = "NM";
        private const string HOST = "HO";
        private const string CUNITS = "CUS";
        private const string CUNIT = "CU";
        private const string CUNITID = "I";
        private const string TURN = "T";
        private const string RMVR = "RMVR";
        private const string RMVS = "RMVS";
        private const string FIRED = "FRD";
        private const string ARMED = "ARD";
        private const string DMG = "DMG";
        private const string SHORTFUEL = "SF";
        private const string SAWENEMY = "SE";
        private const string DUGIN = "DI";
        private const string EXPERIENCE = "EXP";
        private const string READINESS = "RDY";
        private const string EXPTYPE = "EXPT";

        public static void encodeCUnit(CUnit u, CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);

            u.encodeAttributes(output);
            u.encodeChildren(output);

            output.objectEnd();
        }

        protected virtual void encodeAttributes(CEncodedObjectOutputBufferI output)
        {
            output.addAttr(UTYPE, utype_);
            output.addAttr(LOC, loc_.getKey());
            output.addAttr(GID, Convert.ToString(gid_));
            output.addAttr(OWNER, Convert.ToString(owner_));
            output.addAttr(STACKLAYER, Convert.ToString(stackLayer_));
            output.addAttr(LEVEL, level_);
            output.addAttr(LANDED, EncodeUtil.makeBoolString(landed_));
            output.addAttr(INREENTRY, EncodeUtil.makeBoolString(inReentry_));
            output.addAttr(HOST, Convert.ToString(host_));
            output.addAttr(TURN, Convert.ToString(turn_));
            output.addAttr(RMVR, Convert.ToString(rmvr_));
            output.addAttr(RMVS, Convert.ToString(rmvs_));
            output.addAttr(FIRED, EncodeUtil.makeBoolString(fired_));
            output.addAttr(ARMED, EncodeUtil.makeBoolString(armed_));
            output.addAttr(DMG, Convert.ToString(dmg_));
            output.addAttr(SHORTFUEL, EncodeUtil.makeBoolString(shortFuel_));
            output.addAttr(SAWENEMY, EncodeUtil.makeBoolString(sawEnemy_));
            output.addAttr(DUGIN, EncodeUtil.makeBoolString(dugIn_));
            output.addAttr(EXPERIENCE, Convert.ToString(experience_));
            output.addAttr(READINESS, readiness_);
            output.addAttr(EXPTYPE, expType_);
        }
        protected virtual void encodeChildren(CEncodedObjectOutputBufferI output                        )
        {
            output.addTextObject(NAME, name_);

            output.openObject(CUNITS);
            foreach (uint u in cunits_)
            {
                output.openObject(CUNIT);
                output.addAttr(CUNITID, Convert.ToString(u));
                output.objectEnd();
            }
            output.objectEnd();

            ord_.encode(output);
        }

        protected CUnit(Dictionary<string, string> A, CEncodedObjectInputBufferI bin, AIQueryI query)
        {
            utype_ = A[UTYPE];
            loc_ = CLoc.fromKey(A[LOC]);
            gid_ = EncodeUtil.parseUInt(A[GID]);
            owner_ = EncodeUtil.parseInt(A[OWNER]);
            stackLayer_ = EncodeUtil.parseUInt(A[STACKLAYER]);
            level_ = A[LEVEL];
            landed_ = EncodeUtil.fromBoolString(A[LANDED]);
            inReentry_ = EncodeUtil.fromBoolString(A[INREENTRY]);
            host_ = EncodeUtil.parseUInt(A[HOST]);
            turn_ = EncodeUtil.parseInt(A[TURN]);
            rmvr_ = EncodeUtil.parseInt(A[RMVR]);
            rmvs_ = EncodeUtil.parseInt(A[RMVS]);
            fired_ = EncodeUtil.fromBoolString(A[FIRED]);
            armed_ = EncodeUtil.fromBoolString(A[ARMED]);
            dmg_ = EncodeUtil.parseInt(A[DMG]);
            shortFuel_ = EncodeUtil.fromBoolString(A[SHORTFUEL]);
            sawEnemy_ = EncodeUtil.fromBoolString(A[SAWENEMY]);
            dugIn_ = EncodeUtil.fromBoolString(A[DUGIN]);
            experience_ = EncodeUtil.parseUInt(A[EXPERIENCE]);
            readiness_ = A[READINESS];
            expType_ = A[EXPTYPE];

            name_ = bin.getObjectText(NAME);

            bin.nextTag(CUNITS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while (!bin.reachedEndTag(CUNITS))
                {
                    bin.nextTag(CUNIT);
                    Dictionary<string, string> D = bin.getAttributes();
                    uint u = EncodeUtil.parseUInt(D[CUNITID]);
                    cunits_.Add(u);
                    bin.endTag(CUNIT);
                }
            }
            bin.endTag(CUNITS);

            ord_ = new COrder(bin);

            entry_ = query.unitQuery(utype_);
        }
        

        public static CUnit decodeCUnit(CEncodedObjectInputBufferI bin, AIQueryI query)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();
            var ret = new CUnit(A, bin, query);
            bin.endTag(TAG);
            return ret;
        }


        public override string ToString()
        {
            return "[" + Convert.ToString(gid_) + "] " + name_ + " (" + utype_ + ")" + " " + loc_ + " O:" + Convert.ToString(owner_);
        }

        public CUnit(
            string utype, 
            uint gid, 
            CLoc loc, 
            UnitQueryI entry
            )
        {
            utype_ = utype;
            gid_ = gid;
            loc_ = loc;
            entry_ = entry;
        }



        public bool isCrippled()
        {
            return (dmg_ >= (entry_.maxHits() + 1) / 2);
        }

        public int realCapacity()
        {
            return ((entry_.capacity() * (entry_.maxHits() - dmg_)) / entry_.maxHits());
        }

        public bool canRangeFire()
        {
            return (entry_.rfAttackCode() != CUnitConstants.RF_NONE &&
                    rmvs_ == entry_.mvs() &&
                    !fired_);
        }

        public bool isArmed()
        {
            return (entry_.isOrb() && level_ == CUnitConstants.LSA_ORB && inReentry_ && armed_);
        }

        public bool hasLoad()
        {
            return (cunits_.Count > 0);
        }


        public virtual bool update(string attr, string value, CSubLog logger)
        {
            switch (attr)
            {
                case CUnitConstants.NAME:
                    {
                        name_ = value;
                        return true;
                    }
                case CUnitConstants.TURN:
                    {
                        turn_ = EncodeUtil.parseInt(value);
                        return true;
                    }
                case CUnitConstants.HOST:
                    {
                        host_ = EncodeUtil.parseUInt(value);
                        logger.info(this + " now has host value [" + Convert.ToString(host_) + "]");

                        return true;
                    }
                case CUnitConstants.RMVR:
                    {
                        rmvr_ = EncodeUtil.parseInt(value);
                        return true;
                    }
                case CUnitConstants.RMVS:
                    {
                        rmvs_ = EncodeUtil.parseInt(value);
                        return true;
                    }
                case CUnitConstants.FIRED:
                    {
                        fired_ = EncodeUtil.fromBoolString(value);
                        return true;
                    }
                case CUnitConstants.ARMED:
                    {
                        armed_ = EncodeUtil.fromBoolString(value);
                        return true;
                    }
                case CUnitConstants.DUGIN:
                    {
                        dugIn_ = EncodeUtil.fromBoolString(value);
                        return true;
                    }
                case CUnitConstants.DMG:
                    {
                        dmg_ = EncodeUtil.parseInt(value);
                        return true;
                    }
                case CUnitConstants.SHORT_FUEL:
                    {
                        shortFuel_ = EncodeUtil.fromBoolString(value);
                        return true;
                    }
                case CUnitConstants.READINESS:
                    {
                        readiness_ = value;
                        return true;
                    }
                case CUnitConstants.EXPERIENCE:
                    {
                        experience_ = EncodeUtil.parseUInt(value);
                        return true;
                    }
                case CUnitConstants.EXP_LEVEL:
                    {
                        expType_ = value;
                        return true;
                    }

                case CUnitConstants.CHILD_ADD:
                    {
                        uint cgid = EncodeUtil.parseUInt(value);
                        cunits_.Add(cgid);
                        logger.info("Unit " + Convert.ToString(gid_) + " adds child " + Convert.ToString(cgid));
                        return true;
                    }
                case CUnitConstants.CHILD_REM:
                    {
                        uint cgid = EncodeUtil.parseUInt(value);
                        for (int j = 0; j < cunits_.Count; j++)
                        {
                            if (cunits_[j] == cgid)
                            {
                                cunits_.RemoveAt(j);                                
                                break;
                            }
                        }
                        logger.info("Unit " + Convert.ToString(gid_) + " removes child " + Convert.ToString(cgid));
                        return true;
                    }
                case CUnitConstants.LX:
                    setLoc(new CLoc(EncodeUtil.parseInt(value), loc_.y));
                    return true;
                case CUnitConstants.LY:
                    setLoc(new CLoc(loc_.x, EncodeUtil.parseInt(value)));
                    return true;

                case CUnitConstants.OWNER: //will need to do more here if interested in gifts or giveaways
                    {
                        owner_ = EncodeUtil.parseInt(value);
                        return true;
                    }
                case CUnitConstants.LANDED:
                    {
                        landed_ = EncodeUtil.fromBoolString(value);
                        return true;
                    }
                case CUnitConstants.REENTRY:
                    {
                        inReentry_ = EncodeUtil.fromBoolString(value);
                        return true;
                    }

                case CUnitConstants.LEVEL:
                    {
                        level_ = value;
                        return true;
                    }
                case CUnitConstants.SLAYER:
                    {
                        stackLayer_ = EncodeUtil.parseUInt(value);
                        return true;
                    }

            }
            return false;
        }

    }
}

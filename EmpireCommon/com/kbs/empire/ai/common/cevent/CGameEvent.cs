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

using System.Text;
using com.kbs.empire.ai.common.map;
using com.kbs.empire.common.game.supply;
using com.kbs.empire.common.game.unit;

//The CGameEvent comes inbound to the AI player describing some event has occurred
//see CEventConstants for types and values

namespace com.kbs.empire.ai.common.cevent
{
    public class CGameEvent
    {
        public readonly uint id_;
        public readonly string type_;

        //used in various cases
        public string info1_ = null;
        public string info2_ = null;
        public int value1_ = 0;
        public int value2_ = 0;
        public bool flag_ = false;
        public uint gid_ = CUnitConstants.NOUNIT;
        public CUpdate cUpdate_ = null;
        public CMapLocInfo locInfo_ = null;
        public CMapLocInfo loc_ = null;
        public CUnit unit_ = null;
        public CSupplyRoute supplyRoute_ = null;

        public CGameEvent(uint id, string type)
        {
            id_ = id;
            type_ = type;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(type_);

            if (info1_ != null)
                sb.Append(" info1: ").Append(info1_);
            if(info2_ != null)
                sb.Append(" info2: ").Append(info2_);
            sb.Append(" v1: ").Append(value1_).Append(" v2: ").Append(value2_).Append(" f: ").Append(flag_).Append(
                " gid: ").Append(gid_);

            if (cUpdate_ != null)
                sb.Append(" up: ").Append(cUpdate_.ToString());
            if (locInfo_ != null)
                sb.Append(" locinfo: ").Append(locInfo_.ToString());
            if (loc_ != null)
                sb.Append(" loc: ").Append(loc_.ToString());
            if (unit_ != null)
                sb.Append(unit_.ToString());
            if (supplyRoute_ != null)
                sb.Append(" srt: ").Append(supplyRoute_.ToString());

            return sb.ToString();
        }
    }
}

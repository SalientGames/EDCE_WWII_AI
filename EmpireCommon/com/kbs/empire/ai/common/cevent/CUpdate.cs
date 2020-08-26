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
using System.Text;
using com.kbs.empire.common.game.order;

namespace com.kbs.empire.ai.common.cevent
{
    public class CUpdate
    {
        //Name, Value pair of Update/Value
        public readonly Dictionary<string, string> updates_ = new Dictionary<string, string>();
        //Unit Id
        public readonly uint gid_;
        //Unit Order
        public COrder order_ = null;

        public CUpdate(uint gid)
        {
            gid_ = gid;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("gid: ").Append(gid_);
            if (order_ != null)
                sb.Append(" ord: " + order_);
            else
            {
                foreach (KeyValuePair<string, string> kp in updates_)
                {
                    sb.Append(" [").Append(kp.Key).Append(" / ").Append(kp.Value).Append("]");
                }
            }
            return sb.ToString();
        }
    }
}

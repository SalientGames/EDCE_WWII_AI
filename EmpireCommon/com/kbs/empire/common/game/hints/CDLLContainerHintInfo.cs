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
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.xml;

namespace EmpireCommon.com.kbs.empire.common.game.hints
{
    public class CDLLContainerHintInfo : CDLLHintInfo
    {
        //Set of hints I contain
        public readonly List<CDLLHintInfo> infos_;

        //Note SIG MUST BE UNIQUE TO EACH CONTAINER IN THE ENTIRE SET of Hints
        //so that containers can contain containers and serialize properly
        public readonly uint sig_;

        //This attribute is only used by the game program
        public bool goStraightToChildren_ = false;

        public CDLLContainerHintInfo(string key, string name, string desc, uint sig, List<CDLLHintInfo> infos) : base(CONTAINER_TYPE, key, name, desc)
        {
            infos_ = infos;
            sig_ = sig;
        }


        public override CDLLHintInfo copy()
        {
            List<CDLLHintInfo> infos = makeInfoCopy();


            var ret = new CDLLContainerHintInfo(key_, name_, desc_, sig_, infos);

            return ret;

        }

        private List<CDLLHintInfo> makeInfoCopy()
        {
            var infos = new List<CDLLHintInfo>();
            foreach(CDLLHintInfo hi in infos_)
            {
                infos.Add(hi.copy());
            }
            return infos;
        }

        public override string getValue()
        {
            return "NoValue";
        }

        public override string getValue(string key)
        {
            string n =  base.getValue(key);
            if (n != null)
                return n;

            foreach (CDLLHintInfo hi in infos_)
            {
                n = hi.getValue(key);
                if (n != null) return n;
            }
            return null;
        }

        private const string SIG_ATTR = "S";
        private const string GO_ATTR = "G";
        private const string NUM_CHILDREN = "NC";
        private const string CHILDREN_TAGS = "CS_";
        protected override void encodeAttr(CEncodedObjectOutputBufferI output)
        {
            base.encodeAttr(output);
            output.addAttr(NUM_CHILDREN, Convert.ToString(infos_.Count));
            output.addAttr(SIG_ATTR, Convert.ToString(sig_));
            output.addAttr(GO_ATTR, EncodeUtil.makeBoolString(goStraightToChildren_));

        }
        protected override void encodeChildren(CEncodedObjectOutputBufferI output)
        {
            base.encodeChildren(output);

            output.openObject(CHILDREN_TAGS + Convert.ToString(sig_));

            foreach (CDLLHintInfo hi in infos_)
            {
                hi.encode(output);
            }

            output.objectEnd();
        }

        public CDLLContainerHintInfo(Dictionary<string, string> attr, CEncodedObjectInputBufferI bin)
            : base(CONTAINER_TYPE, attr, bin)
        {
            //int nc = EncodeUtil.parseInt(attr[NUM_CHILDREN]);
            sig_ = EncodeUtil.parseUInt(attr[SIG_ATTR]);
            goStraightToChildren_ = EncodeUtil.fromBoolString(attr[GO_ATTR]);

            infos_ = new List<CDLLHintInfo>();

            bin.nextTag(CHILDREN_TAGS + Convert.ToString(sig_));
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(CHILDREN_TAGS  +  Convert.ToString(sig_)))
                {
                    CDLLHintInfo hi = decode(bin);
                    infos_.Add(hi);
                }
            }

            bin.endTag(CHILDREN_TAGS + Convert.ToString(sig_));
        }

    }
}

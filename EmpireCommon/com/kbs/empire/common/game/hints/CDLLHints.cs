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
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.common.game.hints
{
    //An Actual hint collection
    public class CDLLHints
    {
        //hints - key value
        public readonly Dictionary<string, CDLLHintInfo> hints_ = new Dictionary<string, CDLLHintInfo>();
        //order for hints to be presented
        public readonly List<string> hintOrder_ = new List<string>();
        //My dll
        public readonly CDLLInfo dllInfo_;

        public CDLLHints(CDLLInfo dllInfo)
        {
            dllInfo_ = dllInfo;
        }
        public CDLLHints copy()
        {
   
            var ret = new CDLLHints(dllInfo_);
            foreach (string s in hintOrder_)
            {
                CDLLHintInfo mhi = hints_[s];
                ret.addInfo(mhi.copy());
            }

            return ret;
        }

        public void addInfo(CDLLHintInfo info)
        {
            hints_.Add(info.key_, info);
            hintOrder_.Add(info.key_);
        }

        public const string TAG = "MH";
        public const string INFOS = "IS";

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
                dllInfo_.encode(output);

                output.openObject(INFOS);
                foreach (string s in hintOrder_)
                {
                    CDLLHintInfo mhi = hints_[s];
                    mhi.encode(output);
                }
                output.objectEnd();

            output.objectEnd();
        }


        public CDLLHints(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);
            dllInfo_ = new CDLLInfo(bin);
            {
                bin.nextTag(INFOS);

                if (bin.hasChildren())
                {
                    bin.firstChild();
                    while (!bin.reachedEndTag(INFOS))
                    {
                        CDLLHintInfo mhi = CDLLHintInfo.decode(bin);
                        addInfo(mhi);
                    }
                }
                bin.endTag(INFOS);
            }
            bin.endTag(TAG);
        }

        public bool overWriteWith(CDLLHints hints)
        {
            if (hints.dllInfo_.key_ != dllInfo_.key_)
                return false;

            foreach (string key in hints.hints_.Keys)
            {
                if (hints_.ContainsKey(key))
                    hints_[key] = hints.hints_[key];
            }
            return true;
        }

        public string getValue(string key)
        {
            if (hints_.ContainsKey(key))
                return hints_[key].getValue();

            foreach (CDLLHintInfo hi in hints_.Values)
            {
                string v = hi.getValue(key);
                if (v != null) return v;
            }

            return null;
        }


        public string present()
        {
            return (dllInfo_.name_ + " [" + dllInfo_.version_ + "]  (" + dllInfo_.version_ + ")");
        }

    }
}

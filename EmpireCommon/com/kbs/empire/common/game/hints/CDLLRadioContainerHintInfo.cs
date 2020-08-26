
using System;
using System.Collections.Generic;
using com.kbs.empire.common.game.hints;
using com.kbs.empire.common.util.xml;

namespace EmpireCommon.com.kbs.empire.common.game.hints
{
    public class CDLLRadioContainerHintInfo : CDLLHintInfo
    {

        public int currentOption_ = 0;

        //Set of hints I contain
        public readonly List<List<CDLLHintInfo>> minfos_;
        public readonly List<string> catkeys_;
        public readonly List<string> catnames_;
        public readonly List<string> rbnames_;
        

        //Note SIG MUST BE UNIQUE TO EACH CONTAINER IN THE ENTIRE SET of Hints
        //so that containers can contain containers and serialize properly
        public readonly uint sig_;

        public CDLLRadioContainerHintInfo(string key, 
            string name, 
            string desc, 
            uint sig, 
            List<List<CDLLHintInfo>> minfos, 
            List<string> catkeys,
            List<string>catnames,
            List<string> rbnames,
            int currentOption) 
            : base(RADIO_CONTAINER_TYPE, key, name, desc)
        {
            sig_ = sig;
            minfos_ = minfos;
            catkeys_ = catkeys;
            catnames_ = catnames;
            rbnames_ = rbnames;
            currentOption_ = currentOption;
        }

        public override CDLLHintInfo copy()
        {

            var minfos = new List<List<CDLLHintInfo>>();

            for (int i = 0; i < minfos_.Count; i++)
            {
                List<CDLLHintInfo> infos = minfos_[i];
                List<CDLLHintInfo> nimfos = makeInfoCopy(infos);
                minfos.Add(nimfos);
            }

            var ret = new CDLLRadioContainerHintInfo(key_, name_, desc_, sig_, minfos,  catkeys_, catnames_, rbnames_, currentOption_);

            return ret;
        }

        private List<CDLLHintInfo> makeInfoCopy(List<CDLLHintInfo> infos)
        {
            var ninfos = new List<CDLLHintInfo>();
            foreach (CDLLHintInfo hi in infos)
            {
                ninfos.Add(hi.copy());
            }
            return ninfos;
        }

        public override string getValue()
        {
            return "";
        }

        public override string getValue(string key)
        {
            string n = base.getValue(key);
            if (n != null)
                return n;

            for(int i = 0; i < catkeys_.Count; i++)
            {
                if(catkeys_[i] == key)
                {
                    return EncodeUtil.makeBoolString(i == currentOption_);
                }
            }


            foreach (List<CDLLHintInfo> hil in minfos_)
            {
                foreach (CDLLHintInfo hi in hil)
                {
                    n = hi.getValue(key);
                    if (n != null) return n;
                }
            }
            return null;
        }


        private const string SIG_ATTR = "S";
        private const string NUM_CHILDREN = "NC";
        private const string LIST_TAGS = "LCS_";
        private const string LIST_TAG = "LC_";
        private const string CURRENT_ATTR = "LN";
        //private const string CHILDREN_TAGS = "LCS_";
        private const string KEY_TAGS = "KS";
        private const string KEY_TAG = "K";
        private const string CATNAME_TAGS = "CNS";
        private const string CATNAME_TAG = "CN";
        private const string RBNAME_TAGS = "RBS";
        private const string RBNAME_TAG = "RB";

        protected override void encodeAttr(CEncodedObjectOutputBufferI output)
        {
            base.encodeAttr(output);
            output.addAttr(NUM_CHILDREN, Convert.ToString(minfos_.Count));
            output.addAttr(SIG_ATTR, Convert.ToString(sig_));
            output.addAttr(CURRENT_ATTR, Convert.ToString(currentOption_));


        }
        protected override void encodeChildren(CEncodedObjectOutputBufferI output)
        {
            base.encodeChildren(output);

            output.openObject(LIST_TAGS + Convert.ToString(sig_));

            foreach (List<CDLLHintInfo> infos in minfos_)
            {
                output.openObject(LIST_TAG + Convert.ToString(sig_));

                foreach (CDLLHintInfo hi in infos)
                {
                    hi.encode(output);
                }

                output.objectEnd();
            }

            output.objectEnd();


            output.openObject(KEY_TAGS);
            foreach (string catkey in catkeys_)
            {
                output.addTextObject(KEY_TAG, catkey);
            }
            output.objectEnd();

            output.openObject(CATNAME_TAGS);
            foreach (string catname in catnames_)
            {
                output.addTextObject(CATNAME_TAG, catname);
            }
            output.objectEnd();

            output.openObject(RBNAME_TAGS);
            foreach (string rbname in rbnames_)
            {
                output.addTextObject(RBNAME_TAG, rbname);
            }
            output.objectEnd();


        }

        public CDLLRadioContainerHintInfo(Dictionary<string, string> attr, CEncodedObjectInputBufferI bin)
            : base(RADIO_CONTAINER_TYPE, attr, bin)
        {
            sig_ = EncodeUtil.parseUInt(attr[SIG_ATTR]);
            currentOption_ = EncodeUtil.parseInt(attr[CURRENT_ATTR]);

            minfos_ = new List<List<CDLLHintInfo>>();

            bin.nextTag(LIST_TAGS + Convert.ToString(sig_));
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(LIST_TAGS + Convert.ToString(sig_)))
                {
                    var infos = new List<CDLLHintInfo>();
                    minfos_.Add(infos);
                    bin.nextTag(LIST_TAG + Convert.ToString(sig_));
                    if(bin.hasChildren())
                    {
                        bin.firstChild();
                        while(!bin.reachedEndTag(LIST_TAG + Convert.ToString(sig_)))
                        {
                            CDLLHintInfo hi = decode(bin);
                            infos.Add(hi);
                        }
                    }
                    bin.endTag(LIST_TAG + Convert.ToString(sig_));
                }
            }
            bin.endTag(LIST_TAGS + Convert.ToString(sig_));
            catkeys_ = new List<string>();
            bin.nextTag(KEY_TAGS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(KEY_TAGS))
                {
                    catkeys_.Add(bin.getObjectText(KEY_TAG));
                }
            }

            bin.endTag(KEY_TAGS);

            catnames_ = new List<string>();
            bin.nextTag(CATNAME_TAGS);
            if (bin.hasChildren())
            {
                bin.firstChild();
                while (!bin.reachedEndTag(CATNAME_TAGS))
                {
                    catnames_.Add(bin.getObjectText(CATNAME_TAG));
                }
            }
            bin.endTag(CATNAME_TAGS);

            rbnames_ = new List<string>();
            bin.nextTag(RBNAME_TAGS);
            if (bin.hasChildren())
            {
                bin.firstChild();
                while (!bin.reachedEndTag(RBNAME_TAGS))
                {
                    rbnames_.Add(bin.getObjectText(RBNAME_TAG));
                }
            }
            bin.endTag(RBNAME_TAGS);

        }
    }
}

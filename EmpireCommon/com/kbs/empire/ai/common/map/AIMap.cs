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
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.ai.common.map
{
	public class AIMap
	{
        //Map Utility 
	    public readonly CMapUtil  mapUtil_;

        //Stack Count refers to allowable stacking rules  (1 == one unit w flyover OR  2 == air/ground share square)
	    public readonly uint stackCount_;
	
        //The actual map - use "getIndex(x,y)" to reference
	    private readonly char[] mapBytes_;
        
        //Locations (by string key) of mines, resources, roads and wasteland
	    private readonly Dictionary<string, bool> wasteland_ = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> mines_ = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> roads_ = new Dictionary<string, bool>();
        private readonly Dictionary<string, int> resources_ = new Dictionary<string, int>();

        public bool isWasteland(CLoc loc){return wasteland_.ContainsKey(loc.getKey());}
        public void setWasteland(CLoc loc, bool add)
        {
            if (!add && wasteland_.ContainsKey(loc.getKey()))
                wasteland_.Remove(loc.getKey());
            if(add && !wasteland_.ContainsKey(loc.getKey()))
                wasteland_.Add(loc.getKey(), true);
        }
        public void setMine(CLoc loc, bool add)
        {
            if (!add && mines_.ContainsKey(loc.getKey()))
                mines_.Remove(loc.getKey());
            if(add && !mines_.ContainsKey(loc.getKey()))
                mines_.Add(loc.getKey(), true);
        }
        public bool isMine(CLoc loc) { return mines_.ContainsKey(loc.getKey()); }
        public int resourceValue(CLoc loc)
        {
            if (resources_.ContainsKey(loc.getKey()))
                return resources_[loc.getKey()];
            return 0;
        }
        public void setResource(CLoc loc, int rv)
        {
            if (rv == 0 && resources_.ContainsKey(loc.getKey()))
                resources_.Remove(loc.getKey());
            else if(rv > 0)
            {
                if (resources_.ContainsKey(loc.getKey()))
                    resources_[loc.getKey()] = rv;
                else
                    resources_.Add(loc.getKey(), rv);
            }
        }
        public List<string> getAllResourceLocs()
        {
            return new List<string>(resources_.Keys);
        }
        public void addRoad(CLoc loc, bool add)
        {
            if(!add && roads_.ContainsKey(loc.getKey()))
                roads_.Remove(loc.getKey());
            else if(add && !roads_.ContainsKey(loc.getKey()))
                roads_.Add(loc.getKey(), true);
        }
        public bool hasRoad(CLoc loc)
        {
            return roads_.ContainsKey(loc.getKey());
        }
	
	    public AIMap(int w, bool hw, int h, bool vw, uint sc)
	    {
	        mapUtil_ = new CMapUtil(w, hw, h, vw);

	        stackCount_ = sc;
		    
		    var sbw = new StringBuilder("");
            for (int i = 0; i < mapUtil_.width_; i++)
			    sbw.Append(EmpireCC.CT_BLANK);
		
		    var mstr = new StringBuilder("");
            for (int i = 0; i < mapUtil_.height_; i++)
			    mstr.Append(sbw.ToString());
		
		
		    mapBytes_ = mstr.ToString().ToCharArray();
	    }

        /*
	    public AIMap(int w, bool hw, int h, bool vw, uint sc, char[] data) 
	    {
            mapUtil_ = new CMapUtil(w, hw, h, vw);
	        stackCount_ = sc;
		    mapBytes_ = data;
	    }
        */


	    public string getMapLocation(int x, int y)
	    {
            int ux = mapUtil_.hwash(x);
            int uy = mapUtil_.vwash(y);
            if (mapUtil_.validLoc(ux, uy))
		    {
			    int X = getIndex(ux, uy);
			    var str = new String(new []{mapBytes_[X]});
			    return str;
		    }
		    return null;
	    }
	
	    private int getIndex(int x, int y)
	    {
            return (y + (mapUtil_.height_ * x));
	    }
	
        public string getLocationFromDir(int x, int y, int dir, int dist)
        {
            CLoc loc = mapUtil_.getLocationFromDir(x, y, dir, dist);
            if (loc == null) return null;

            return getMapLocation(loc);
        }

	    public CLoc getNextLocationFromDir(int x, int y, int dir)
	    {
	        return mapUtil_.getLocationFromDir(x, y, dir, 1);
	    }
	
	    public bool next2Sea(CLoc loc) 
	    {
            for (int dir = CMapUtil.NORTH; dir <= CMapUtil.NORTHWEST; dir++)
		    {
			    string ml = getLocationFromDir(loc.x, loc.y, dir, 1);
			    if(ml == null)
				    continue;
			    if( ml == EmpireCC.CT_SEA || ml == EmpireCC.CT_SHALLOWSEA)
				    return( true ) ;
		    }
		    return( false ) ;
	    }

	    public List<CLoc> getSurrounding(CLoc loc) {
		    var ret = new List<CLoc>();
            for (int dir = CMapUtil.NORTH; dir <= CMapUtil.NORTHWEST; dir++)
		    {
			    CLoc L = getNextLocationFromDir(loc.x, loc.y, dir);
			    if(L != null)
				    ret.Add(L);
		    }
		    return ret;
	    }

	    public List<CLoc> getEdgeLocsToCheck(CLoc loc, int range) {
		
		    var locs = new List<CLoc>();
		
		    if(range == 0)
			    locs.Add(loc);
		    else
		    {
			    int orgx = loc.x - range;
			    int orgy = loc.y - range;
			    int endx = loc.x + range;
			    int endy = loc.y + range;
			
			
			    //top left to top right
			    //bottom left to bottom, right
			    int x = orgx;
			    while(x <= endx)
			    {
                    var t = new CLoc(mapUtil_.hwash(x), mapUtil_.vwash(orgy));
                    var b = new CLoc(mapUtil_.hwash(x), mapUtil_.vwash(endy));

                    if (mapUtil_.validLoc(t.x, t.y))
					    locs.Add(t);
                    if (mapUtil_.validLoc(b.x, b.y))
					    locs.Add(b);
				    x++;
			    }
			
			    int y = orgy + 1;
			    while(y < endy)
			    {
                    var l = new CLoc(mapUtil_.hwash(orgx), mapUtil_.vwash(y));
                    var r = new CLoc(mapUtil_.hwash(endx), mapUtil_.vwash(y));

                    if (mapUtil_.validLoc(l.x, l.y))
					    locs.Add(l);
                    if (mapUtil_.validLoc(r.x, r.y))
					    locs.Add(r);				
				    y++;
			    }			
		    }		
		    return locs;
	    }
	
	    public List<CLoc> getLocsToCheck(CLoc loc, int range, bool includeOrigin)
	    {


	        List<CLoc> vec = mapUtil_.getSurroundingRng(loc, range);
            if (includeOrigin)
                vec.Add(loc);
	        return vec;		
	    }

	    public List<CLoc> getAdjacentSeaLocs(CLoc loc) 
	    {
		    var locs = new  List<CLoc>();
            for (int dir = CMapUtil.NORTH; dir <= CMapUtil.NORTHWEST; dir++)
		    {
			    string ml = getLocationFromDir(loc.x, loc.y, dir, 1);
			    if(ml == null)
				    continue;
			    if( ml == EmpireCC.CT_SEA || ml == EmpireCC.CT_SHALLOWSEA)
			    {
				    CLoc L = getNextLocationFromDir(loc.x, loc.y, dir);
				    if(L != null)
					    locs.Add(L);
			    }
		    }
		    return( locs ) ;
	    }
	
	    public int move_dir_l(CLoc beg_loc, CLoc end_loc)
	    {

		    int dr = end_loc.y - beg_loc.y ;
		    int dc = end_loc.x - beg_loc.x ;

		    if(dr == 0 && dc == 0)
                return CMapUtil.NODIR;
		
		    if( mapUtil_.hwrap_)
		    {
                if (dc * 2 > mapUtil_.width_)
                    dc = dc - mapUtil_.width_;
                else if (dc * 2 < -mapUtil_.width_)
                    dc += mapUtil_.width_;
		    }
            if (mapUtil_.vwrap_)
		    {
                if (dr * 2 > mapUtil_.height_)
                    dr = dr - mapUtil_.height_;
                else if (dr * 2 < -mapUtil_.height_)
                    dr += mapUtil_.height_;
		    }

		    if( Math.Abs( dc ) > Math.Abs( dr ) )
		    {
			    if( dc > 0 )
                    return (CMapUtil.EAST);
			    else
                    return (CMapUtil.WEST);
		    }
		    else if( dc == dr )
		    {
			    if( dc > 0 )
                    return (CMapUtil.SOUTHEAST);
			    else
                    return (CMapUtil.NORTHWEST);
		    }
		    else if( dc == -dr )
		    {
			    if( dc > 0 )
                    return (CMapUtil.NORTHEAST);
			    else
                    return (CMapUtil.SOUTHWEST);
		    }
		    else if( dr > 0 )
                return (CMapUtil.SOUTH);
		    else
                return (CMapUtil.NORTH);
	    }



	    public void setMapLocation(int x, int y, string type) 
	    {
		    char b = type[0];
		    mapBytes_[getIndex(x, y)] = b;		
	    }

        public string getMapLocation(CLoc loc)
        {
            if (!mapUtil_.validLoc(loc.x, loc.y)) return null;
            return Convert.ToString(mapBytes_[getIndex(loc.x, loc.y)]);
        }

        private const string TAG = "AIMAP";
        private const string TMAP_TAG = "TMAP";
        private const string GAME_MAP_WIDTH_ATTR = "W";
        private const string GAME_MAP_HWRAP_ATTR = "HW";
        private const string GAME_MAP_HEIGHT_ATTR = "H";
        private const string GAME_MAP_VWRAP_ATTR = "VW";
        private const string STACKCOUNT = "SC";
        private const string WASTELAND = "WL";
        private const string MINES = "MN";
        private const string ROAD = "RD";
        private const string RESOURCES = "RES";

        public void encode(CEncodedObjectOutputBufferI output)
        {
            output.openObject(TAG);
            output.addAttr(GAME_MAP_WIDTH_ATTR, Convert.ToString(mapUtil_.width_));
            output.addAttr(GAME_MAP_HWRAP_ATTR, EncodeUtil.makeBoolString(mapUtil_.hwrap_));
            output.addAttr(GAME_MAP_HEIGHT_ATTR, Convert.ToString(mapUtil_.height_));
            output.addAttr(GAME_MAP_VWRAP_ATTR, EncodeUtil.makeBoolString(mapUtil_.vwrap_));
            output.addAttr(STACKCOUNT, Convert.ToString(stackCount_));

            output.addTextObject(TMAP_TAG, new string(mapBytes_));

            EncodeUtil.encodeStringList(WASTELAND, new List<string>(wasteland_.Keys), output);
            EncodeUtil.encodeStringList(MINES, new List<string>(mines_.Keys), output);
            EncodeUtil.encodeStringList(ROAD, new List<string>(roads_.Keys), output);
            EncodeUtil.encodeDSI(RESOURCES, resources_, output);

            output.objectEnd();
        }	

        public AIMap(CEncodedObjectInputBufferI bin)
        {
            bin.nextTag(TAG);
            Dictionary<string, string> A = bin.getAttributes();

            int width = EncodeUtil.parseInt(A[GAME_MAP_WIDTH_ATTR]);
            int height = EncodeUtil.parseInt(A[GAME_MAP_HEIGHT_ATTR]);
            bool vwrap = EncodeUtil.fromBoolString(A[GAME_MAP_VWRAP_ATTR]);
            bool hwrap = EncodeUtil.fromBoolString(A[GAME_MAP_HWRAP_ATTR]);

            mapUtil_ = new CMapUtil(width, hwrap, height, vwrap);

            stackCount_ = EncodeUtil.parseUInt(A[STACKCOUNT]);

            string mb = bin.getObjectText(TMAP_TAG);
            mapBytes_ = mb.ToCharArray();

            var tmp = new List<string>();
            EncodeUtil.decodeStringList(WASTELAND, tmp, bin);
            foreach (string s in tmp)
                wasteland_.Add(s, true);
            tmp.Clear();

            EncodeUtil.decodeStringList(MINES, tmp, bin);
            foreach (string s in tmp)
                mines_.Add(s, true);
            tmp.Clear();

            EncodeUtil.decodeStringList(ROAD, tmp, bin);
            foreach (string s in tmp)
                roads_.Add(s, true);
            tmp.Clear();

            EncodeUtil.decodeDSI(RESOURCES, resources_, bin);


            bin.endTag(TAG);
        }
	}
}

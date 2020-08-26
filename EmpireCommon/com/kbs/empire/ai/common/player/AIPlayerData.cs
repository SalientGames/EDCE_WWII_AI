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
using EmpireCommon.com.kbs.empire.common.game.rules;
using com.kbs.empire.ai.common.cevent;
using com.kbs.empire.ai.common.map;
using com.kbs.empire.ai.common.proc;
using com.kbs.empire.common.game.data;
using com.kbs.empire.common.game.map;
using com.kbs.empire.common.game.order;
using com.kbs.empire.common.game.player;
using com.kbs.empire.common.game.proc;
using com.kbs.empire.common.game.rules;
using com.kbs.empire.common.game.supply;
using com.kbs.empire.common.game.treaty;
using com.kbs.empire.common.game.unit;
using com.kbs.empire.common.util.log;
using com.kbs.empire.common.util.random;
using com.kbs.empire.common.util.xml;

namespace com.kbs.empire.ai.common.player
{
    //deriving from AIPLayer, this class can hold a good amount of the regular game data. Very useful.
    public abstract class AIPlayerData : AIPlayer
    {
        //game turn
        public int curturn_ = 0;


        //num players
        public int numPlayers_ = 0;
        public List<CPlayer> players_ = new List<CPlayer>();

        //game object collections
        //All things I know about
        public Dictionary<uint, CUnit> masterObjects_ = new Dictionary<uint, CUnit>();
        //All My Cities
        public List<CProducerUnit> cities_ = new List<CProducerUnit>();        
        public Dictionary<uint, CProducerUnit> cityMap_ = new Dictionary<uint, CProducerUnit>();
        //Non-City Producer Units (like oilfields)
        public List<CProducerUnit> producers_ = new List<CProducerUnit>();
        public Dictionary<uint, CProducerUnit> producerMap_ = new Dictionary<uint, CProducerUnit>();
        //MY Units
        public List<CUnit> units_ = new List<CUnit>();
        public Dictionary<uint, CUnit> unitMap_ = new Dictionary<uint, CUnit>();
        //Other People's Stuff
        public List<CUnit> spots_ = new List<CUnit>();
        public Dictionary<uint, CUnit> spotMap_ = new Dictionary<uint, CUnit>();
        //All cities on the map that I see
        public List<CProducerUnit> knownCitiesVec_ = new List<CProducerUnit>();
        //Supply Routes
        public readonly Dictionary<uint, CSupplyRoute> supplySources_ = new Dictionary<uint, CSupplyRoute>();

        //map of terrain
        public AIMap map_ = null;
        public CMapUtil mapUtil_ = null;

        //flyover unit
        public CUnit foUnit_ = null;

        //GameRules
        public CGameRules gameRules_ = null;
        public CVictoryConditions vc_;

        //random for use of AI - can be replaced
        public CMTRandom random_;

        //log
        private readonly CSubLog dlogger_;

        //resource drain info
        public int rdWeight_ = 0;
        public int unitsBeforeDrain_ = 0;

        //treaties
        public List<CProposal> proposals_ = new List<CProposal>();
        public List<CProductionReportData>  prodReport_ = new List<CProductionReportData>();

        protected AIPlayerData(int position, string path, string logname, AIEventInterfaceI aiEvent, AICommandInterfaceI command, AIQueryI query, AICheatI cheat, int logLevel)
            : base(position, path, logname, aiEvent, command, query, cheat, logLevel)
        {
            dlogger_ = new CSubLog("PlayerData:" + Convert.ToString(position), realLog_);
            dlogger_.info("D Logger Log Open: " + path + " " + logname);
            random_ = new CMTRandom();
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        public CUnit getSpot(uint gid)
        {
            if (spotMap_.ContainsKey(gid))
                return spotMap_[gid];
            return null;
        }

        public virtual void addSpots(List<CUnit> spots)
        {
            if (spots != null)
            {
                for (int i = 0; i < spots.Count; i++)
                {
                    CUnit su = spots[i];
                    if (aiLogging_)
                        dlogger_.info("S - Adding Spot " + su);
                    addSpot(su);
                }
            }
        }
        public virtual void addSpot(CUnit su)
        {
            if (aiLogging_)
                dlogger_.info("Spot Added " + su);
            spots_.Add(su);
            spotMap_.Add(su.gid_, su);
            masterObjects_.Add(su.gid_, su);
            if (su.entry_.isCity())
                knownCitiesVec_.Add((CProducerUnit)su);
        }
        public virtual void removeSpot(CUnit du)
        {
            for (int i = 0; i < spots_.Count; i++)
            {
                if (spots_[i].gid_ == du.gid_)
                {
                    if (aiLogging_)
                        dlogger_.info("Spot Removed " + spots_[i]);
                    spots_.RemoveAt(i);
                    break;
                }
            }

            spotMap_.Remove(du.gid_);

            masterObjects_.Remove(du.gid_);

            if (du.entry_.isCity())
            {
                for (int i = 0; i < knownCitiesVec_.Count; i++)
                {
                    if (knownCitiesVec_[i].gid_ == du.gid_)
                    {
                        knownCitiesVec_.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        public virtual void addProds(List<CProducerUnit> units)
        {
            //These are non-city producers (like oilfields that can produce supply)
            if (units != null)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    CUnit cu = units[i];
                    if (aiLogging_)
                        dlogger_.info("S - Adding Prod " + cu);
                    addUnit(cu);
                }
            }
        }
        public virtual void addUnits(List<CUnit> units)
        {
            if (units != null)
            {
                for (int i = 0; i < units.Count; i++)
                {                    
                    CUnit cu = units[i];
                    if (aiLogging_)
                        dlogger_.info("S - Adding Unit " + cu);
                    addUnit(cu);
                }
            }
        }

        public virtual void addUnit(CUnit cu)
        {
            if (aiLogging_)
                dlogger_.info("Adding Unit " + cu);
            if (cu.entry_.isProducer())
                addProducer((CProducerUnit)cu);
            units_.Add(cu);
            unitMap_.Add(cu.gid_, cu);
            masterObjects_.Add(cu.gid_, cu);
        }

        public virtual void removeUnit(CUnit du)
        {
            if (aiLogging_)
                dlogger_.info("Removing Unit : " + du);
            for (int i = 0; i < units_.Count; i++)
            {
                if (units_[i].gid_ == du.gid_)
                {
                    units_.RemoveAt(i);
                    break;
                }
            }
            unitMap_.Remove(du.gid_);
            masterObjects_.Remove(du.gid_);
        }

        public CUnit getUnit(uint gid)
        {
            if (unitMap_.ContainsKey(gid))
                return unitMap_[gid];
            return null;
        }

        public int getUnitLoad(CUnit unit)
        {
            if (!unit.hasLoad()) return 0;
            int L = 0;
            for(int i = 0; i < unit.cunits_.Count; i ++)
            {
                CUnit c = getUnit(unit.cunits_[i]);
                UnitQueryI q = query_.unitQuery(c.utype_);
                L += q.portage();
            }
            return L;
        }

        public int countUnits(string unitType)
        {
            int count = 0;
            for (int i = 0; i < units_.Count; i++)
                if (units_[i].utype_ == unitType)
                    count++;
            return count;
        }

        public CUnit getUnitById(uint uid, bool usecheat)
        {
            if (usecheat)
                return cheat_.getUnit(position_, uid);
            return getUnit(uid);
        }        
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        public virtual void addCities(List<CProducerUnit> cities)
        {
            if (cities != null)
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    CProducerUnit cc = cities[i];
                    if (aiLogging_)
                        dlogger_.info("S - Adding City " + cc);
                    addCity(cc);
                }
            }
        }

        public virtual void addCity(CProducerUnit cc)
        {
            if (aiLogging_)
                    dlogger_.info("Adding City: " + cc);
            cities_.Add(cc);
            cityMap_.Add(cc.gid_, cc);
            knownCitiesVec_.Add(cc);
            masterObjects_.Add(cc.gid_, cc);
        }
        public virtual void removeCity(CProducerUnit du)
        {
            for (int i = 0; i < cities_.Count; i++)
            {
                if (cities_[i].gid_ == du.gid_)
                {
                    if (aiLogging_)
                        dlogger_.info("Removing City : " + cities_[i]);
                    cities_.RemoveAt(i);
                    break;
                }
            }

            cityMap_.Remove(du.gid_);
            for (int i = 0; i < knownCitiesVec_.Count; i++)
            {
                if (knownCitiesVec_[i].gid_ == du.gid_)
                {
                    knownCitiesVec_.RemoveAt(i);
                    break;
                }
            }

            masterObjects_.Remove(du.gid_);
        }

        public CProducerUnit getCityById(uint cid, bool usecheat)
        {
            if (usecheat)
                return cheat_.getCity(position_, cid);

            return getCity(cid);
        }

        public CProducerUnit getCity(uint cid)
        {
            if (cityMap_.ContainsKey(cid))
                return cityMap_[cid];
            return null;
        }
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        private void addObject(CUnit cu)
        {
            if (aiLogging_)
                dlogger_.info("Add GID : " + cu.gid_ + " " + cu.utype_);
            if (cu.owner_ != position_)
                addSpot(cu);
            else
            {
                if (cu.entry_.isCity())
                    addCity((CProducerUnit)cu);
                else
                {
                    addUnit(cu);
                }
            }
        }


        public CUnit getObjectById(uint gid)
        {
            if (masterObjects_.ContainsKey(gid))
                return masterObjects_[gid];
            return null;

        }

        private void removeObject(uint gid)
        {
            CUnit du = getObjectById(gid);
            if (du == null)
            {
                throw new Exception("*****Remove GID not found: " + gid);
            }
            if (aiLogging_)
                dlogger_.info("Remove GID : " + gid + " " + du.utype_ + " " + du.name_);
            if (du.owner_ != position_)
                removeSpot(du);
            else
            {
                if (du.entry_.isCity())
                    removeCity((CProducerUnit)du);
                else
                {
                    if (du.entry_.isProducer())
                        removeProducer((CProducerUnit)du);
                    removeUnit(du);
                }
            }
            if (foUnit_ != null && foUnit_.gid_ == gid)
                foUnit_ = null;
        }

        public virtual void addProducer(CProducerUnit cu)
        {
            dlogger_.info("Adding Producer " + cu);
            producers_.Add(cu);
            producerMap_.Add(cu.gid_, cu);
        }

        public virtual void removeProducer(CProducerUnit cu)
        {
            for(int i = 0; i < producers_.Count; i++)
            {
                if(cu.gid_ == producers_[i].gid_)
                {
                    producers_.RemoveAt(i);
                    break;
                }
            }
            producerMap_.Remove(cu.gid_);
        }

        public CProducerUnit getAllKnownCityById(uint cnum)
        {
            for (int i = 0; i < knownCitiesVec_.Count; i++)
            {
                if (knownCitiesVec_[i].gid_ == cnum)
                {
                    return knownCitiesVec_[i];
                }
            }
            return null;
        }



        //////////////////////////////////////////////////////////
        //Get All Top Units in the location
        //will return null if there is nothing
        public CUnit[] getHostsAtLoc(CLoc loc)
        {
            var ret = new CUnit[CUnitConstants.TOTAL_LEVEL_INDEXES];

            bool added = false;
            //cities first
            for (int i = 0; i < cities_.Count; i++)
            {
                if (cities_[i].getLoc().areEqual(loc))
                {
                    
                    ret[CUnitConstants.STACK_GROUND_INDEX] = cities_[i];
                    added = true;
                    break;
                }
            }

            for (int i = 0; i < units_.Count; i++)
            {
                CUnit cu = units_[i];
                if (cu.host_ == CUnitConstants.NOUNIT && cu.getLoc().areEqual(loc))
                {
                    if (cu.level_ == CUnitConstants.LVL_ORB)
                        ret[CUnitConstants.ORB_LEVEL_INDEX] = cu;
                    else if (cu.level_ == CUnitConstants.LVL_SUB)
                        ret[CUnitConstants.SUB_LEVEL_INDEX] = cu;
                    else
                        ret[CUnitConstants.STACK_GROUND_INDEX + cu.stackLayer_] = cu;
                    added = true;
                }
            }

            for (int i = 0; i < spots_.Count; i++)
            {
                CUnit cu = spots_[i];
                if (cu.host_ == CUnitConstants.NOUNIT && cu.getLoc().areEqual(loc))
                {
                    if (cu.level_ == CUnitConstants.LSA_ORB)
                        ret[CUnitConstants.ORB_LEVEL_INDEX] = cu;
                    else if (cu.level_ == CUnitConstants.LSA_SUB)
                        ret[CUnitConstants.SUB_LEVEL_INDEX] = cu;
                    else
                        ret[CUnitConstants.STACK_GROUND_INDEX + cu.stackLayer_] = cu;
                    added = true;
                }
            }


            if(!added)
                return null;
            return ret;
        }
        //Get A Specific Unit in a specific loc at a specific layer

        public CUnit getHostAtLoc(CLoc l, string level, uint layer)
        {
            CUnit[] hosts = getHostsAtLoc(l);
            if (hosts == null)
            {
                return null;
            }
            switch (level)
            {
                case CUnitConstants.LVL_ORB:
                    return hosts[CUnitConstants.ORB_LEVEL_INDEX];
                case CUnitConstants.LVL_SUB:
                    return hosts[CUnitConstants.SUB_LEVEL_INDEX];
                default:
                    return hosts[CUnitConstants.STACK_GROUND_INDEX + layer];
            }
        }
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        //Event Handler
        public override void processEvent(CGameEvent ge, CSubLog logger)
        {
            if (aiLogging_)
                dlogger_.info("Event: " + ge.type_);

            if (ge.type_ == CEventConstants.CONTINUETURNEVENT ||
                       ge.type_ == CEventConstants.ENDTURN ||
                       ge.type_ == CEventConstants.TURNCHANGE ||
                ge.type_ == CEventConstants.CHAT_EVENT)
                return;




            if (ge.type_ == CEventConstants.GAMSESTATE)
            {
                processGameState((CStateEvent) ge);
                return;
            }
            if (ge.type_ == CEventConstants.STARTTURNEVENT)
            {
                processStartTurn((CStartTurn)ge, logger);
                return;
            }

            //data updates
            if (ge.type_ == CEventConstants.PLAYERNAMECHANGE)
            {
                CPlayer cp = players_[ge.value1_];
                cp.pname_ = ge.info1_;
                return;

            }            
            if (ge.type_ == CEventConstants.DRAIN_EVENT)
            {
                unitsBeforeDrain_ = ge.value1_;
                rdWeight_ = ge.value2_;                
                return;
            }
            if (ge.type_ == CEventConstants.UNITUPDATE)
            {
                updateUnit(ge.cUpdate_, logger);
                return;
            }
            if (ge.type_ == CEventConstants.DEATHEVENT)
            {
                dlogger_.info("DEATH " + ge.gid_ + " " + ge.info1_);
                removeObject(ge.gid_);
                return;
            }
            if (ge.type_ == CEventConstants.FLYOVEREVENT)
            {
                if (ge.flag_)
                    foUnit_ = getUnit(ge.gid_);
                else
                    foUnit_ = null;
                return;
            }
            if (ge.type_ == CEventConstants.MAPLOCATION)
            {
                addMapLocation(ge.locInfo_);
                return;
            }
            if (ge.type_ == CEventConstants.NEWUNIT)
            {
                addObject(ge.unit_);
                return;
            }
            if (ge.type_ == CEventConstants.STOPTURNEVENT)
            {
                setHold();
                return;
            } 
            if(ge.type_ == CEventConstants.SPOTTER)
            {
                return;
            }
            if(ge.type_ == CEventConstants.PLAYERDEAD)
            {
                players_[ge.value1_].living_ = false;
                if(ge.value1_ == position_)
                    setHold();
                return;
            }
            if (ge.type_ == CEventConstants.PLAYERWIN)
            {
                setHold();
                return;
            }
            if(ge.type_ == CEventConstants.END_OF_EVENTS)
            {
                setHold();
                return;
            }
            if(ge.type_ == CEventConstants.REMOVE_SUPPLY_EVENT)
            {
                supplySources_.Remove(ge.gid_);
                return;
            }
            if(ge.type_ == CEventConstants.SUPPLY_EVENT)
            {
                CSupplyRoute csr = ge.supplyRoute_;
                if (supplySources_.ContainsKey(csr.rid_))
                    supplySources_[csr.rid_] = csr;
                else
                    supplySources_.Add(csr.rid_, csr);
                return;
            }
            if(ge.type_ == CEventConstants.PLY_BUYPOINT_EVENT)
            {
                players_[position_].buypoints_ = ge.value1_;
                players_[position_].buypointsSpent_ = ge.value2_;
                return;
            }
            if(ge.type_ == CEventConstants.SCORE_EVENT)
            {
                players_[ge.value1_].score_ = ge.value2_;
            }
        }

        ////////////////////////////////////////////////////////////
        //Start Turn -  This event is sent at the beginning of
        //your turn. Includes data like production results
        private void processStartTurn(CStartTurn ste, CSubLog logger)
        {
            curturn_ = ste.turn_;
            addUnits(ste.unitAdds_);
            if (ste.updates_ != null)
            {
                for (int i = 0; i < ste.updates_.Count; i++)
                    updateUnit(ste.updates_[i], logger);
            }
            //Note, props are really invalid after your turn ends
            proposals_.Clear();
            if (ste.proposals_ != null)
            {
                foreach (CProposal p in ste.proposals_)
                    proposals_.Add(p);
            }

            prodReport_.Clear();
            if(ste.prodData_ != null)
            {
                foreach (CProductionReportData prd in ste.prodData_)
                    prodReport_.Add(prd);
            }


        }

        ////////////////////////////////////////////////////////////
        //Game State -  This event is sent initially and on reloads
        //Gives the current state of your forces and what you can see
        private void processGameState(CStateEvent gse)
        {
            //Player Information
            numPlayers_ = gse.players_.Count;
            for (int i = 0; i < numPlayers_; i++)
            {
                players_.Add(gse.players_[i]);
                if (i == position_)
                    pname_ = gse.players_[i].pname_;
            }

            curturn_ = gse.curturn_;

            rdWeight_ = gse.rdWeight_;
            unitsBeforeDrain_ = gse.unitsBeforeDrain_;

            //Map Information
            mapUtil_ = new CMapUtil(gse.w_, gse.hw_, gse.h_, gse.vw_);

            map_ = new AIMap(gse.w_, gse.hw_, gse.h_, gse.vw_, gse.gameRules_.stackCount_);


            uint fogid = gse.flyoverGid_;

            addMapLocations(gse.exploredLocs_);

            addCities(gse.cities_);
            addProds(gse.prods_);
            addUnits(gse.units_);
            addSpots(gse.spots_);

            addSupplyRoutes(gse.routes_);

            gameRules_ = gse.gameRules_;
            vc_ = gse.vc_;

            if(gse.proposals_ != null)
            {
                foreach (CProposal p in gse.proposals_)
                    proposals_.Add(p);
            }

            //Grab Flyover if need be (Basic and Standard Games)
            if (fogid != CUnitConstants.NOUNIT)
            {
                foUnit_ = getUnit(fogid);
            }

            prodReport_.Clear();
            if (gse.prodData_ != null)
            {
                foreach (CProductionReportData prd in gse.prodData_)
                    prodReport_.Add(prd);
            }

            gameStateReceived();

        }

        protected virtual void gameStateReceived()
        {
            //a notification that the game state has been populated.
        }

        private void updateUnit(CUpdate cup, CSubLog logger)
        {
            CUnit cu = getObjectById(cup.gid_);

            if(cu == null)
            {
                throw new Exception("Missing Unit in update: " + cup.gid_);
            }
            //firstly, is this an order update
            if (cup.order_ != null)
            {
                cu.ord_ = cup.order_;
                return;
            }
            
            foreach (string uptype in cup.updates_.Keys)
            {
                string value = cup.updates_[uptype];
                
                if (!cu.update(uptype, value, logger))
                {
                    if (aiLogging_)
                        dlogger_.info("Missing Update for " + uptype);
                    continue;
                }
            }
        }

        private void addSupplyRoutes(List<CSupplyRoute> routes)
        {
            if (routes != null)
            {
                foreach (CSupplyRoute csr in routes)
                {
                    supplySources_.Add(csr.source_, csr);
                }
            }
            
        }

        private void addMapLocations(List<CMapLocInfo> exploredLocs)
        {
            if (exploredLocs != null)
            {
                for (int i = 0; i < exploredLocs.Count; i++)
                    addMapLocation(exploredLocs[i]);
            }
        }

        private void addMapLocation(CMapLocInfo cml)
        {
            map_.setMapLocation(cml.loc_.x, cml.loc_.y, cml.terrain_);
            if(cml.road_ != map_.hasRoad(cml.loc_))
                map_.addRoad(cml.loc_, cml.road_);
            if(cml.resources_ != map_.resourceValue(cml.loc_))
                map_.setResource(cml.loc_, (int)cml.resources_);
            if(cml.mine_ != map_.isMine(cml.loc_))
                map_.setMine(cml.loc_, cml.mine_);
            if (cml.wasteland_ != map_.isWasteland(cml.loc_))
                map_.setWasteland(cml.loc_, cml.wasteland_);
        }


        public bool isSea(string ttype)
        {
            return (ttype == EmpireCC.CT_SEA || ttype == EmpireCC.CT_SHALLOWSEA || ttype == EmpireCC.CT_CITY);
        }

        public bool isLand(string ttype)
        {
            return(
                ttype == EmpireCC.CT_CITY ||
                ttype == EmpireCC.CT_CLEAR || 
                ttype == EmpireCC.CT_FOREST ||
                ttype == EmpireCC.CT_ROUGH ||
                ttype == EmpireCC.CT_SNOW ||
                ttype == EmpireCC.CT_RIVER ||
                ttype == EmpireCC.CT_SWAMP ||
                ttype == EmpireCC.CT_HILL ||
                ttype == EmpireCC.CT_MOUNTN ||
                ttype == EmpireCC.CT_PEAK);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        public const string AI_RANDOM_TAG = "AI_RAND";
        public const string CUR_TURN = "CT";
        public const string NUM_PLAYERS = "NP";
        public const string RD_WEIGHT = "RDW";
        public const string UNITS_BEFORE_DRAIN = "UBD";
        public const string FOUNIT_ID = "FOU";

        public const string SPOTU = "SPOTU";
        public const string SPOTC = "SPOTC";
        public const string SPOTP = "SPOTP";
        public const string CITIES = "CITIES";
        public const string UNITS = "UNITS";
        public const string PRODUCERS = "PRODS";
        

        //////////////////////////////////////////////////////////////////////////////////////////
        //Saving
        public void encodeAttr(CEncodedObjectOutputBufferI output)
        {
            output.addAttr(CUR_TURN, Convert.ToString(curturn_));
            output.addAttr(NUM_PLAYERS, Convert.ToString(numPlayers_));
            output.addAttr(RD_WEIGHT, Convert.ToString(rdWeight_));
            output.addAttr(UNITS_BEFORE_DRAIN, Convert.ToString(unitsBeforeDrain_));
            if(foUnit_ != null)
                output.addAttr(FOUNIT_ID, Convert.ToString(foUnit_.gid_));
        }

        public void encodeChildren(CEncodedObjectOutputBufferI output)
        {
            output.openObject(CPlayer.TAGS);
            foreach (CPlayer cp in players_)
                cp.encode(output);
            output.objectEnd();

            var spotc = new List<CProducerUnit>();
            var spotu = new List<CUnit>();
            var spotp = new List<CProducerUnit>();

            foreach (CUnit cu in spots_)
            {
                if(cu.entry_.isCity())
                    spotc.Add((CProducerUnit)cu);
                else if(cu.entry_.isProducer())
                    spotp.Add((CProducerUnit)cu);
                else
                    spotu.Add(cu);
            }

            output.openObject(SPOTU);
            foreach (CUnit u in spotu)
                CUnit.encodeCUnit(u, output);
            output.objectEnd();
            
            output.openObject(SPOTC);
            foreach (CProducerUnit c in spotc)
                CProducerUnit.encodeCProducerUnit(c, output);
            output.objectEnd();

            output.openObject(SPOTP);
            foreach (CProducerUnit p in spotp)
                CProducerUnit.encodeCProducerUnit(p, output);
            output.objectEnd();

            output.openObject(CITIES);
            foreach (CProducerUnit c in cities_)
                CProducerUnit.encodeCProducerUnit(c, output);
            output.objectEnd();

            output.openObject(UNITS);
            foreach (CUnit u in units_)
            {
                if (!u.entry_.isProducer())
                    CUnit.encodeCUnit(u, output);
            }
            output.objectEnd();

            output.openObject(PRODUCERS);
            dlogger_.info("Prods:" + Convert.ToString(producers_.Count));
            foreach (CProducerUnit p in producers_)
                CProducerUnit.encodeCProducerUnit(p, output);
            output.objectEnd();

            output.openObject(CSupplyRoute.TAGS);
            foreach (CSupplyRoute csr in supplySources_.Values)
                csr.encode(output);    
            output.objectEnd();

            map_.encode(output);

            gameRules_.encode(output);

            vc_.encode(output);

            output.openObject(CProposal.TAGS);
            foreach (CProposal p in proposals_)
                p.encode(output);
            output.objectEnd();

            output.openObject(CProductionReportData.TAGS);
            foreach (CProductionReportData prd in prodReport_)
                prd.encode(output);
            output.objectEnd();

            //random for use of AI
            random_.encode(AI_RANDOM_TAG, output);
            return;
        }



        ////////////////////////////////////////////////////////////////////
        //Reloading
        protected AIPlayerData(
            int position, 
            string path, 
            string logname,
            Dictionary<string, string> caMap,
            CEncodedObjectInputBufferI bin, 
            AIEventInterfaceI aiEvent, 
            AICommandInterfaceI command, 
            AIQueryI query, 
            AICheatI cheat,
            int logLevel)
            :base(position, path, logname, caMap, bin, aiEvent, command, query, cheat, logLevel)
        {
            dlogger_ = new CSubLog("PlayerData:" + Convert.ToString(position), realLog_);


            curturn_ = EncodeUtil.parseInt(caMap[CUR_TURN]);
            numPlayers_ = EncodeUtil.parseInt(caMap[NUM_PLAYERS]);
            rdWeight_ = EncodeUtil.parseInt(caMap[RD_WEIGHT]);
            unitsBeforeDrain_ = EncodeUtil.parseInt(caMap[UNITS_BEFORE_DRAIN]);

            bin.nextTag(CPlayer.TAGS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(CPlayer.TAGS))
                    players_.Add(new CPlayer(bin));
            }
            bin.endTag(CPlayer.TAGS);

            bin.nextTag(SPOTU);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(SPOTU))
                {
                    CUnit u = CUnit.decodeCUnit(bin, query_);
                    masterObjects_.Add(u.gid_, u);
                    spotMap_.Add(u.gid_, u);
                    spots_.Add(u);
                }
            }
            bin.endTag(SPOTU);


            bin.nextTag(SPOTC);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(SPOTC))
                {
                    CProducerUnit c = CProducerUnit.decodeCProducerUnit(bin, query_);
                    masterObjects_.Add(c.gid_, c);
                    knownCitiesVec_.Add(c);
                    spotMap_.Add(c.gid_, c);
                    spots_.Add(c);
                }
            }
            bin.endTag(SPOTC);

            bin.nextTag(SPOTP);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(SPOTP))
                {
                    CProducerUnit p = CProducerUnit.decodeCProducerUnit(bin, query);
                    masterObjects_.Add(p.gid_, p);
                    spotMap_.Add(p.gid_, p);
                    spots_.Add(p);
                }
            }
            bin.endTag(SPOTP);

            bin.nextTag(CITIES);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(CITIES))
                {
                    CProducerUnit c = CProducerUnit.decodeCProducerUnit(bin, query_);
                    masterObjects_.Add(c.gid_, c);
                    knownCitiesVec_.Add(c);
                    cities_.Add(c);
                    cityMap_.Add(c.gid_, c);
                }
            }
            bin.endTag(CITIES);


            bin.nextTag(UNITS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(UNITS))
                {
                    CUnit u = CUnit.decodeCUnit(bin, query_);
                    masterObjects_.Add(u.gid_, u);
                    unitMap_.Add(u.gid_, u);
                    units_.Add(u);
                }
            }
            bin.endTag(UNITS);

            bin.nextTag(PRODUCERS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(PRODUCERS))
                {
                    CProducerUnit p = CProducerUnit.decodeCProducerUnit(bin, query_);
                    masterObjects_.Add(p.gid_, p);
                    producers_.Add(p);
                    producerMap_.Add(p.gid_, p);
                    unitMap_.Add(p.gid_, p);
                    units_.Add(p);
                }
            }
            bin.endTag(PRODUCERS);

            bin.nextTag(CSupplyRoute.TAGS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(CSupplyRoute.TAGS))
                {
                    var csr = new CSupplyRoute(bin);
                    supplySources_.Add(csr.rid_, csr);
                }
            }
            bin.endTag(CSupplyRoute.TAGS);

            map_ = new AIMap(bin);
            mapUtil_ = map_.mapUtil_;

            gameRules_ = new CGameRules(bin);
            
            vc_ = new CVictoryConditions(bin);

            bin.nextTag(CProposal.TAGS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(CProposal.TAGS))
                {
                    var p = new CProposal(bin);
                    proposals_.Add(p);
                }
            }
            bin.endTag(CProposal.TAGS);

            bin.nextTag(CProductionReportData.TAGS);
            if(bin.hasChildren())
            {
                bin.firstChild();
                while(!bin.reachedEndTag(CProductionReportData.TAGS))
                {
                    var prd = new CProductionReportData(bin);
                    prodReport_.Add(prd);
                }
            }
            bin.endTag(CProductionReportData.TAGS);


            random_ = new CMTRandom(AI_RANDOM_TAG, bin);
            
            //retrieve flyover
            foUnit_ = null;
            if(caMap.ContainsKey(FOUNIT_ID))
            {
                uint fid = EncodeUtil.parseUInt(caMap[FOUNIT_ID]);
                foUnit_ = masterObjects_[fid];
            }
        }

        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////

        //Just get it off me - assumed stack level == 1
        public void ejectFlyover()
        {
            pollAllEvents();
            if (foUnit_ == null)
                return;

            //second send home
            var ord = new COrder(COrderConstants.ORD_GO_HOME);
            setAndExecuteUnitOrder(foUnit_.gid_, ord);
            pollAllEvents();
            if (foUnit_ == null)
                return;

            //third. move in open space
            for (int i = CMapUtil.NORTH; i < CMapUtil.NODIR; i++)
            {
                CLoc uloc = foUnit_.getLoc();
                CLoc loc = map_.getNextLocationFromDir(uloc.x, uloc.y, i);
                if (loc == null)
                    continue;
                CUnit cu = getHostAtLoc(loc, CUnitConstants.LVL_GROUND, 0u);
                if (cu == null || (cu.owner_ != position_ && !foUnit_.entry_.isCity()))
                {
                    ord = new COrder(COrderConstants.ORD_MOVE_DIR, i);
                    setAndExecuteUnitOrder(foUnit_.gid_, ord);
                    pollAllEvents();
                }
                if (foUnit_ == null)
                    return;

            }
            //finally disband
            disbandUnit(foUnit_.gid_);
            pollAllEvents();
        }
        //count 'em
        public int numHumans()
        {
            int c = 0;
            for (int i = 0; i < players_.Count; i++)
            {
                CPlayer cp = players_[i];
                if (cp.living_ && cp.isEvilHuman())
                    c++;
            }
            return c;
        }

        public int mv_cost(CLoc t_loc, string level, CUnit aiunit)
        {
            
            if (t_loc == null)
                return int.MaxValue;
            string t = map_.getMapLocation(t_loc.x, t_loc.y);
            
            CUnit[] hosts = getHostsAtLoc(t_loc);
            //No One in location
            bool nohost = true;
            if(hosts != null)
            {
                switch (level)
                {
                    case CUnitConstants.LVL_ORB:
                        nohost = (hosts[CUnitConstants.ORB_LEVEL_INDEX] == null);
                        break;
                    case CUnitConstants.LVL_SUB:
                        nohost = (hosts[CUnitConstants.SUB_LEVEL_INDEX] == null);
                        break;
                    default://stack
                        nohost = ((hosts[CUnitConstants.STACK_GROUND_INDEX] == null) &&
                                  (hosts[CUnitConstants.STACK_AIR_INDEX] == null));
                        break;
                }
            }
            if(nohost)
            {
                if (aiunit.entry_.canMove(t))
                    return aiunit.entry_.getMove(t);
                else
                    return EmpireCC.IMPOSSIBLE;
            }

            //we have a host
            //some of the cases here are simplified and could fail on execution
            CUnit hu = null;
            switch (level)
            {
                case CUnitConstants.LVL_ORB:
                    {
                        hu = hosts[CUnitConstants.ORB_LEVEL_INDEX];
                        break;
                    }
                case CUnitConstants.LVL_SUB:
                    {
                        hu = hosts[CUnitConstants.SUB_LEVEL_INDEX];
                        break;
                    }
                default://stack
                    {
                        hu = (hosts[CUnitConstants.STACK_AIR_INDEX] != null)
                                 ? hosts[CUnitConstants.STACK_AIR_INDEX]
                                 : hosts[CUnitConstants.STACK_GROUND_INDEX];
                        break;
                    }
            }

            //friendly
            if(hu.owner_ == position_)
            {
                if(hu.entry_.canTransport(aiunit.utype_))
                    return 1;
                return EmpireCC.IMPOSSIBLE;
            }

            if (aiunit.entry_.canMove(t))
            {
                if(hu.entry_.isCity())
                    return ((aiunit.entry_.cityCombat() != CUnitConstants.CC_NONE) ? 1:EmpireCC.IMPOSSIBLE);
                return aiunit.entry_.getMove(t);
            }
            else if((t == EmpireCC.CT_SEA || t == EmpireCC.CT_SHALLOWSEA) && aiunit.entry_.canSeaBombardAttack())
                return 1;
            return EmpireCC.IMPOSSIBLE;
        }

    }
}

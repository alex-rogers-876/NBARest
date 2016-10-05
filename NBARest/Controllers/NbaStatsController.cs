using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NBARest.Models;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace NBARest.Controllers
{
    public class NbaStatsController : Controller
    {
        List<NbaStats> nbaStatsListy = new List<NbaStats>();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NbaStats
        /*
        public ActionResult Index()
        {
            return View(db.NbaStats.ToList());
        }*/

        //Get: NbaStats/514241
        public List<NbaStats> getStats(JObject nbaData)
        {
            List<NbaStats> nbaStatsList = new List<NbaStats>();
            int seasons = nbaData["resultSets"][0]["rowSet"].Count(); // queries number of seasons for the player
            NbaStats nbaStatsTemp = new NbaStats();
            for (int i = 0; i < seasons; i++)
            {
                NbaStats nbaStats = new NbaStats();
                nbaStats.playerId = (int)nbaData["resultSets"][0]["rowSet"][i][0];
                nbaStats.seasonId = (string)nbaData["resultSets"][0]["rowSet"][i][1];
                nbaStats.leagueId = (string)nbaData["resultSets"][0]["rowSet"][i][2];
                nbaStats.teamId = (int)nbaData["resultSets"][0]["rowSet"][i][3];
                nbaStats.teamAbrv = (string)nbaData["resultSets"][0]["rowSet"][i][4];
                nbaStats.playerAge = (int)nbaData["resultSets"][0]["rowSet"][i][5];
                nbaStats.gamesPlayed = (double)nbaData["resultSets"][0]["rowSet"][i][6];
                nbaStats.gamesStarted = (double)nbaData["resultSets"][0]["rowSet"][i][7];
                nbaStats.minutes = (double)nbaData["resultSets"][0]["rowSet"][i][8];
                nbaStats.fgm = (double)nbaData["resultSets"][0]["rowSet"][i][9];
                nbaStats.fga = (double)nbaData["resultSets"][0]["rowSet"][i][10];
                nbaStats.fgPercent = (double)nbaData["resultSets"][0]["rowSet"][i][11];
                nbaStats.fg3m = (double)nbaData["resultSets"][0]["rowSet"][i][12];
                nbaStats.fg3a = (double)nbaData["resultSets"][0]["rowSet"][i][13];
                nbaStats.fg3Percent = (double)nbaData["resultSets"][0]["rowSet"][i][14];
                nbaStats.ftm = (double)nbaData["resultSets"][0]["rowSet"][i][15];
                nbaStats.fta = (double)nbaData["resultSets"][0]["rowSet"][i][16];
                nbaStats.ftPercent = (double)nbaData["resultSets"][0]["rowSet"][i][17];
                nbaStats.oReb = (double)nbaData["resultSets"][0]["rowSet"][i][18];
                nbaStats.dReb = (double)nbaData["resultSets"][0]["rowSet"][i][19];
                nbaStats.totReb = (double)nbaData["resultSets"][0]["rowSet"][i][20];
                nbaStats.ast = (double)nbaData["resultSets"][0]["rowSet"][i][21];
                nbaStats.stl = (double)nbaData["resultSets"][0]["rowSet"][i][22];
                nbaStats.blk = (double)nbaData["resultSets"][0]["rowSet"][i][23];
                nbaStats.tov = (double)nbaData["resultSets"][0]["rowSet"][i][24];
                nbaStats.pf = (double)nbaData["resultSets"][0]["rowSet"][i][25];
                nbaStats.pts = (double)nbaData["resultSets"][0]["rowSet"][i][26];
                Console.WriteLine(nbaStats.pts);
                nbaStatsList.Add(nbaStats);
                nbaStatsTemp = nbaStats;
            }
            return nbaStatsList;
        }
     
        public ActionResult Index(int id)
        {

            var client = new RestClient("http://stats.nba.com/stats/");

            var request = new RestRequest("playercareerstats?LeagueID=00&PerMode=PerGame&playerId={token}", Method.GET);

            request.AddParameter("token", id, ParameterType.UrlSegment);
            //201939
            // request.AddUrlSegment("token", "saga001"); 

            // request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var queryResult = client.Execute(request);
            JObject nbaData = JObject.Parse(queryResult.Content); // <- where json is the string above

            /* uncomment to get the above list instead of passing json data
            nbaStatsListy = getStats(nbaData);
            //return View(db.NbaStats.ToList());
            */
            return View(nbaData);
            
        }

        // GET: NbaStats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new RestClient("http://stats.nba.com/stats/");

            var request = new RestRequest("playercareerstats?LeagueID=00&PerMode=PerGame&playerId={token}", Method.GET);

            request.AddParameter("token", id, ParameterType.UrlSegment);
            //201939
            // request.AddUrlSegment("token", "saga001"); 

            // request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var queryResult = client.Execute(request);
            JObject nbaData = JObject.Parse(queryResult.Content); // <- where json is the string above
            int seasons = nbaData["resultSets"][0]["rowSet"].Count(); // queries number of seasons for the player
            //Cycles through the number of seasons  and puts the players season into a list
            NbaStats nbaStatsTemp = new NbaStats();
            for (int i = 0; i < seasons; i++)
            {
                NbaStats nbaStats = new NbaStats();
                nbaStats.playerId = (int)nbaData["resultSets"][0]["rowSet"][i][0];
                nbaStats.seasonId = (string)nbaData["resultSets"][0]["rowSet"][i][1];
                nbaStats.leagueId = (string)nbaData["resultSets"][0]["rowSet"][i][2];
                nbaStats.teamId = (int)nbaData["resultSets"][0]["rowSet"][i][3];
                nbaStats.teamAbrv = (string)nbaData["resultSets"][0]["rowSet"][i][4];
                nbaStats.playerAge = (int)nbaData["resultSets"][0]["rowSet"][i][5];
                nbaStats.gamesPlayed = (double)nbaData["resultSets"][0]["rowSet"][i][6];
                nbaStats.gamesStarted = (double)nbaData["resultSets"][0]["rowSet"][i][7];
                nbaStats.minutes = (double)nbaData["resultSets"][0]["rowSet"][i][8];
                nbaStats.fgm = (double)nbaData["resultSets"][0]["rowSet"][i][9];
                nbaStats.fga = (double)nbaData["resultSets"][0]["rowSet"][i][10];
                nbaStats.fgPercent = (double)nbaData["resultSets"][0]["rowSet"][i][11];
                nbaStats.fg3m = (double)nbaData["resultSets"][0]["rowSet"][i][12];
                nbaStats.fg3a = (double)nbaData["resultSets"][0]["rowSet"][i][13];
                nbaStats.fg3Percent = (double)nbaData["resultSets"][0]["rowSet"][i][14];
                nbaStats.ftm = (double)nbaData["resultSets"][0]["rowSet"][i][15];
                nbaStats.fta = (double)nbaData["resultSets"][0]["rowSet"][i][16];
                nbaStats.ftPercent = (double)nbaData["resultSets"][0]["rowSet"][i][17];
                nbaStats.oReb = (double)nbaData["resultSets"][0]["rowSet"][i][18];
                nbaStats.dReb = (double)nbaData["resultSets"][0]["rowSet"][i][19];
                nbaStats.totReb = (double)nbaData["resultSets"][0]["rowSet"][i][20];
                nbaStats.ast = (double)nbaData["resultSets"][0]["rowSet"][i][21];
                nbaStats.stl = (double)nbaData["resultSets"][0]["rowSet"][i][22];
                nbaStats.blk = (double)nbaData["resultSets"][0]["rowSet"][i][23];
                nbaStats.tov = (double)nbaData["resultSets"][0]["rowSet"][i][24];
                nbaStats.pf = (double)nbaData["resultSets"][0]["rowSet"][i][25];
                nbaStats.pts = (double)nbaData["resultSets"][0]["rowSet"][i][26];
                Console.WriteLine(nbaStats.pts);
                nbaStatsListy.Add(nbaStats);
                nbaStatsTemp = nbaStats;
            }
                /* Note: Replace with reflection*/
               // return View(nbaStatsList[0]);
              //  NbaStats nbaStats = db.NbaStats.Find(id);
              /*
            if (nbaStats == null)
            {
                return HttpNotFound();
            }*/
                return View(nbaStatsListy.ToList());
     }

        // GET: NbaStats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NbaStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "playerId,seasonId,leagueId,teamId,teamAbrv,playerAge,gamesPlayed,gamesStarted,minutes,fga,fgm,fgPercent,fg3m,fg3a,fg3Percent,ftm,fta,ftPercent,oReb,dReb,totReb,ast,stl,blk,tov,pf,pts")] NbaStats nbaStats)
        {
            if (ModelState.IsValid)
            {
                db.NbaStats.Add(nbaStats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nbaStats);
        }

        // GET: NbaStats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NbaStats nbaStats = db.NbaStats.Find(id);
            if (nbaStats == null)
            {
                return HttpNotFound();
            }
            return View(nbaStats);
        }

        // POST: NbaStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "playerId,seasonId,leagueId,teamId,teamAbrv,playerAge,gamesPlayed,gamesStarted,minutes,fga,fgm,fgPercent,fg3m,fg3a,fg3Percent,ftm,fta,ftPercent,oReb,dReb,totReb,ast,stl,blk,tov,pf,pts")] NbaStats nbaStats)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nbaStats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nbaStats);
        }

        // GET: NbaStats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NbaStats nbaStats = db.NbaStats.Find(id);
            if (nbaStats == null)
            {
                return HttpNotFound();
            }
            return View(nbaStats);
        }

        // POST: NbaStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NbaStats nbaStats = db.NbaStats.Find(id);
            db.NbaStats.Remove(nbaStats);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

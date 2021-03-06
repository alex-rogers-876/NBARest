﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NBARest.Models;

namespace NBARest.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // NbaStats nbaStats = new NbaStats();
        List<NbaStats> nbaStatsList = new List<NbaStats>();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5

        public string Get(int id)
        {
            var client = new RestClient("http://stats.nba.com/stats/");

            var request = new RestRequest("playercareerstats?LeagueID=00&PerMode=PerGame&playerId={token}", Method.GET);

            request.AddParameter("token", id, ParameterType.UrlSegment);
            //201939
            // request.AddUrlSegment("token", "saga001"); 

           // request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var queryResult = client.Execute(request);
            JObject nbaData = JObject.Parse(queryResult.Content); // <- where json is the string above
            int seasons = nbaData["resultSets"][0]["rowSet"].Count(); // queries number of seasons for the player
            
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

                /* Note: Replace with reflection*/

            }

            return nbaStatsList[0].printStats();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

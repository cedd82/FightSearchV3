using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightSearch.Repository.Sql;
using FightSearch.Repository.Sql.Entities;
using FightSearch.Repository.SqlLight;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FightSearch.Common.ExtensionMethods;

namespace PortSqlServerToSqlLite
{
    public class SqLiteMigration : ISqLiteMigration
    {
        private readonly ILogger<SqLiteMigration> _logger;
        private readonly UfcContextLite _ufcContextLite;
        private readonly UfcContext _ufcContext;

        //public SqLiteMigration(UfcContext ufcContext, UfcContextLite ufcContextLite)
        public SqLiteMigration(UfcContext ufcContext, ILogger<SqLiteMigration> logger, UfcContextLite ufcContextLite)
        {
            _ufcContext = ufcContext;
            _logger = logger;
            _ufcContextLite = ufcContextLite;
        }

        public void DoMigration()
        {
            List<WikiFightWeb> wikiFightsFromSql = _ufcContext.WikiFightWeb.ToList();


            //string json = JsonConvert.SerializeObject(xxx, Formatting.Indented);

            //using (StreamWriter writer = new StreamWriter($@"C:\temp\data\WikiFightWeb-{DateTime.Now.Ticks}.json"))
            //{
            //    writer.Write(json);
            //    writer.Flush();
            //}

            //byte[] t = Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes("asdf"));


            List<WikiFightWebSqlLite> wikiFightsSqlLite = wikiFightsFromSql.Select(x => new WikiFightWebSqlLite
            {

                Id = x.Id,
                DateHeld = x.DateHeld.ToString("yyyy-MM-dd"),
                EventId = x.EventId,
                EventName = x.EventName.UnicodeToUtf8(),
                FightId = x.FightId,
                FightResult = x.FightResult.UnicodeToUtf8(),
                FightResultHow = x.FightResultHow.UnicodeToUtf8(),
                FightResultSubType = x.FightResultSubType.UnicodeToUtf8(),
                FightResultType = x.FightResultType.UnicodeToUtf8(),
                Fighter1Name = x.Fighter1Name.UnicodeToUtf8(),
                Fighter2Name = x.Fighter2Name.UnicodeToUtf8(),
                Fotn = Convert.ToInt32(x.Fotn),
                ImageForWeb = x.ImageForWeb,
                ImagePath = x.ImagePath,
                Potn = Convert.ToInt32(x.Potn),
                Promotion = x.Promotion.UnicodeToUtf8(),
                RedditTopFights = Convert.ToInt32(x.RedditTopFights),
                Round = Convert.ToInt32(x.Round),
                Time = x.Time,
                TitleFight = Convert.ToInt32(x.TitleFight),
                TotalTime = x.TotalTime.ToString("g"),
                TotalTimeEnum = x.TotalTimeEnum,
                VideoLink = x.VideoLink,
                WeightClass = x.WeightClass.UnicodeToUtf8(),
                WikiFightId = x.WikiFightId,

            }).ToList();

            _ufcContextLite.WikiFightWebSqlLite.AddRange(wikiFightsSqlLite);
            _ufcContextLite.SaveChanges();

            return;

            foreach (WikiFightWeb x in wikiFightsFromSql)
            {
                WikiFightWebSqlLite f = new WikiFightWebSqlLite
                {
                    Id = x.Id,
                    DateHeld = x.DateHeld.ToString("yyyy-MM-dd"),
                    EventId = x.EventId,
                    EventName = x.EventName.UnicodeToUtf8(),
                    FightId = x.FightId,
                    FightResult = x.FightResult.UnicodeToUtf8(),
                    FightResultHow = x.FightResultHow.UnicodeToUtf8(),
                    FightResultSubType = x.FightResultSubType.UnicodeToUtf8(),
                    FightResultType = x.FightResultType.UnicodeToUtf8(),
                    Fighter1Name = x.Fighter1Name.UnicodeToUtf8(),
                    Fighter2Name = x.Fighter2Name.UnicodeToUtf8(),
                    Fotn = Convert.ToInt32(x.Fotn),
                    ImageForWeb = x.ImageForWeb,
                    ImagePath = x.ImagePath,
                    Potn = Convert.ToInt32(x.Potn),
                    Promotion = x.Promotion.UnicodeToUtf8(),
                    RedditTopFights = Convert.ToInt32(x.RedditTopFights),
                    Round = Convert.ToInt32(x.Round),
                    Time = x.Time,
                    TitleFight = Convert.ToInt32(x.TitleFight),
                    TotalTime = x.TotalTime.ToString("g"),
                    TotalTimeEnum = x.TotalTimeEnum,
                    VideoLink = x.VideoLink,
                    WeightClass = x.WeightClass.UnicodeToUtf8(),
                    WikiFightId = x.WikiFightId,

                };
                

                _ufcContextLite.WikiFightWebSqlLite.Add(f);
                _ufcContextLite.SaveChanges();
            }

            var test = wikiFightsFromSql;
        }

        public void Misc()
        {
            var xxx = _ufcContextLite.WikiFightWebSqlLite.Take(10);
            var test = xxx;
        }
    }
}
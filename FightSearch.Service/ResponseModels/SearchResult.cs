using System;

namespace FightSearch.Service.ResponseModels
{
    public class SearchResult
    {
        public string AwardType { get; set; }
        public DateTime DateHeld { get; set; }
        public string Fighter1Name { get; set; }
        public string Fighter1NameOriginal { get; set; }
        public string Fighter2Name { get; set; }
        public string Fighter2NameOriginal { get; set; }
        public string FightResultHow { get; set; }
        public bool Fotn { get; set; }
        public string ImageForWeb { get; set; }
        public string ImageForWebNameMixed { get; set; }
        public string ImageForWebOriginal { get; set; }
        public string ImgUrl { get; set; }
        public bool Potn { get; set; }
        public int Rank { get; set; }
        public string Round { get; set; }
        public string Time { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string VideoLink { get; set; }
        public string WeightClass { get; set; }
        public string WEventName { get; set; }
        public int WId { get; set; }
    }
}
namespace FightSearch.Service.DomainModels
{
	public class PagingInfo
	{
		public int ItemsPerPage { get; set; }
		public int Page { get; set; }
		public bool Reverse { get; set; }
		public string Search { get; set; }
		public string SortBy { get; set; }
		public int TotalItems { get; set; }
	}
}

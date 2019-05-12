using System.ComponentModel.DataAnnotations;

namespace FightSearch.Service.DomainModels
{
	public class SearchQuery
	{
		[Required]
		public PagingInfo PagingInfo { get; set; }

		[Required]
		public SearchParams SearchParams { get; set; }
	}
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FightSearch.Service.Interfaces
{
    /// Retrieves unique fighter names dependent on the page if fighters exist for the fights being queried.
	/// There may be fight entries with no video associated. So the query firstly checks that fights have a video and are
	/// further filtered depending on the page being visited. This way only relevant names are returned for fights that will appear.
	public interface IFighterNameService
	{
		Task<IEnumerable<string>> GetAllUniqueNames();
        Task<IEnumerable<string>> GetAwardFighterNames();
        Task<IEnumerable<string>> GetTitleFighterNames();
	}
}
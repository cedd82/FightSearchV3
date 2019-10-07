using System.Threading.Tasks;

namespace FightSearch.Service.Interfaces
{
    /// <summary>
	/// custom logic to track visitor behavior
	/// </summary>
	public interface IVisitorTrackingService
	{
		// keeps a count of how many times a video was clicked
		Task LinkClicked(int wikiFightId);
	}
}
namespace FightSearch.Service
{
	using System.Threading.Tasks;

	/// <summary>
	/// custom logic to track visitor behavior
	/// </summary>
	public interface IVisitorTrackingService
	{
		// keeps a count of how many times a video was clicked
		Task LinkClicked(int wikiFightId);
	}
}
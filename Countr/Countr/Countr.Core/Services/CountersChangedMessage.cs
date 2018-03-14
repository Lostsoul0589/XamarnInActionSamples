using MvvmCross.Plugins.Messenger;
namespace Countr.Core
{
	public class CountersChangedMessage : MvxMessage
	{
		public CountersChangedMessage(object sender) : base(sender)
		{
		}
	}
}

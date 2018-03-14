using System;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using MvvmCross.Plugins.Messenger;
using System.Threading.Tasks;
namespace Countr.Core
{
	public class CountersViewModel : MvxViewModel
	{
		ICountersService service;
		readonly MvxSubscriptionToken token;

		public CountersViewModel(ICountersService service,
								 IMvxMessenger messenger)
		{
			this.service = service;
			Counters = new ObservableCollection<CounterViewModel>();
			token = messenger.SubscribeOnMainThread<CountersChangedMessage>
							 (async m => await LoadCounters());
		}

		public ObservableCollection<CounterViewModel> Counters { get; }

		public override async Task Initialize()
		{
			await LoadCounters();
		}

		public async Task LoadCounters()
		{
			Counters.Clear();
			var counters = await service.GetAllCounters();
			foreach (Counter c in counters)
			{
				var viewModel = new CounterViewModel(service);
				viewModel.Prepare(c);
				Counters.Add(viewModel);
			}
		}
	}
}

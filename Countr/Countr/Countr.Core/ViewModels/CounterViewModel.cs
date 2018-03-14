using System;
using MvvmCross.Core.ViewModels;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace Countr.Core
{
	public class CounterViewModel : MvxViewModel<Counter>
	{
		Counter counter;
		ICountersService service;

		public CounterViewModel(ICountersService service)
		{
			this.service = service;
			IncrementCommand = new MvxAsyncCommand(IncrementCounter);
			DeleteCommand = new MvxAsyncCommand(DeleteCounter);
		}

		public override void Prepare(Counter counter)
		{
			this.counter = counter;

		}

		public string Name
		{
			get => counter.Name;
			set
			{
				if (Name == value) return;
				counter.Name = value;
				RaisePropertyChanged();
			}
		}

		public IMvxAsyncCommand IncrementCommand { get; }

		async Task IncrementCounter()
		{
			await service.IncrementCounter(counter);
			RaisePropertyChanged(() => Count);
		}

		public IMvxAsyncCommand DeleteCommand { get; }

		async Task DeleteCounter()
		{
			await service.DeleteCounter(counter);
		}

		public int Count => counter.Count;
	}
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;

namespace Countr.Core
{
	public class CountersService : ICountersService
	{
		readonly ICountersRepository repository;
		readonly IMvxMessenger messenger;

		public CountersService(ICountersRepository repository,
							   IMvxMessenger messenger)
		{
			this.repository = repository;
			this.messenger = messenger;
		}

		public async Task<Counter> AddNewCounter(string name)
		{
			var counter = new Counter { Name = name };
			await repository.Save(counter).ConfigureAwait(false);
			return counter;
		}

		public async Task DeleteCounter(Counter counter)
		{
			await repository.Delete(counter).ConfigureAwait(false);
			messenger.Publish(new CountersChangedMessage(this));
		}

		public Task<List<Counter>> GetAllCounters()
		{
			return repository.GetAll();
		}

		public Task IncrementCounter(Counter counter)
		{
			counter.Count += 1;
			return repository.Save(counter);
		}
	}
}

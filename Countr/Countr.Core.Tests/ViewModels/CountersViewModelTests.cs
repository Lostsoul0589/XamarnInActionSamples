using NUnit.Framework;
using System;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MvvmCross.Plugin.Messenger;

namespace Countr.Core.Tests
{
	[TestFixture()]
	public class CountersViewModelTests
	{
		Mock<ICountersService> mockCountersService;
		Mock<IMvxMessenger> messenger;
		CountersViewModel countersViewModel;

		[SetUp]
		public void SetUp()
		{
			mockCountersService = new Mock<ICountersService>();
			messenger = new Mock<IMvxMessenger>();
			countersViewModel = new CountersViewModel(mockCountersService.Object, messenger.Object);
		}

		[Test]
		public async Task LoadCounters_CreatesCounters()
		{
			// Arrange
			var counters = new List<Counter>
			{
				new Counter { Name = "Counter1", Count = 0 },
				new Counter { Name = "Counter2", Count = 4 }
			};

			mockCountersService.Setup(c => c.GetAllCounters()).ReturnsAsync(counters);

			// Act
			await countersViewModel.LoadCounters();

			// Assert
			Assert.AreEqual(2, countersViewModel.Counters.Count);
			Assert.AreEqual("Counter1", countersViewModel.Counters[0].Name);
			Assert.AreEqual(0, countersViewModel.Counters[0].Count);
			Assert.AreEqual("Counter2", countersViewModel.Counters[1].Name);
			Assert.AreEqual(4, countersViewModel.Counters[1].Count);
		}
	}
}

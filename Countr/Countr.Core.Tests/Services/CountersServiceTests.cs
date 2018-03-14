using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using System.Diagnostics.PerformanceData;
using System.Collections.Generic;
using MvvmCross.Plugin.Messenger;

namespace Countr.Core.Tests
{
	[TestFixture()]
	public class CountersServiceTests
	{
		ICountersService service;
		Mock<ICountersRepository> mockRepo;
		Mock<IMvxMessenger> mockMessenger;

		[SetUp]
		public void SetUp()
		{
			mockRepo = new Mock<ICountersRepository>();
			mockMessenger = new Mock<IMvxMessenger>();
			service = new CountersService(mockRepo.Object, mockMessenger.Object);
		}

		[Test]
		public async Task IncrementingCount_IncrementsCounter()
		{
			// Arrange
			var counter = new Counter { Count = 0 };

			// Act
			await service.IncrementCounter(counter);

			// Assert
			Assert.AreEqual(1, counter.Count);
		}

		[Test]
		public async Task IncrementingCount_SavesTheIncrementedCounter()
		{
			// Arrange
			var counter = new Counter { Count = 0 };

			// Act
			await service.IncrementCounter(counter);

			// Assert
			mockRepo.Verify(r => r.Save(It.Is<Counter>(c => c.Count == 1)),
							Times.Once);
		}

		[Test]
		public async Task GetAllCounters_ReturnsAllCountersFromTheRepository()
		{
			var counters = new List<Counter>
		   {
			  new Counter {Name = "Counter1" },
			  new Counter {Name = "Counter2" }
		   };
			mockRepo.Setup(r => r.GetAll()).ReturnsAsync(counters);
			var results = await service.GetAllCounters();
			CollectionAssert.AreEqual(results, counters);
		}

		[Test]
		public async Task DeleteCounter_PublishesAMessage()
		{
			// Act
			await service.DeleteCounter(new Counter());

			// Assert
			mockMessenger.Verify(m => m.Publish(It.IsAny<CountersChangedMessage>()));
		}
	}
}

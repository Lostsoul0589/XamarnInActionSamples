using NUnit.Framework;
using System;
using Moq;
using System.Threading.Tasks;
namespace Countr.Core.Tests
{
	[TestFixture()]
	public class CounterViewModelTests
	{
		Mock<ICountersService> mockCountersService;
		CounterViewModel viewModel;

		[SetUp]
		public void SetUp()
		{
			mockCountersService = new Mock<ICountersService>();
			viewModel = new CounterViewModel(mockCountersService.Object);
			viewModel.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
		}

		[Test]
		public void Name_ComesFromCounter()
		{
			// Arrange
			var counter = new Counter { Name = "A Counter" };

			// Act
			viewModel.Prepare(counter);

			// Assert
			Assert.AreEqual(counter.Name, viewModel.Name);
		}

		[Test]
		public void Count_ComesFromCounter()
		{
			// Arrange
			var counter = new Counter { Count = 1 };

			// Act
			viewModel.Prepare(counter);

			// Assert
			Assert.AreEqual(counter.Count, viewModel.Count);
		}

		[Test]
		public async Task IncrementCounter_IncrementsCounter()
		{
			// Act
			await viewModel.IncrementCommand.ExecuteAsync();

			// Assert
			mockCountersService.Verify(s => s.IncrementCounter(It.IsAny<Counter>()));
		}

		[Test]
		public async Task IncrementCounter_RaisesPropertyChanged()
		{
			// Arrange
			var propertyChangedRaised = false;
			viewModel.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == "Count");

			// Act
			await viewModel.IncrementCommand.ExecuteAsync();

			// Assert
			Assert.IsTrue(propertyChangedRaised);
		}

		[Test]
		public async Task DeleteCommand_DeletesTheCounter()
		{
			// Arrange
			var counter = new Counter { Name = "A Counter" };
			viewModel.Prepare(counter);

			// Act
			await viewModel.DeleteCommand.ExecuteAsync();

			// Assert
			mockCountersService.Verify(c => c.DeleteCounter(counter));
		}

	}
}

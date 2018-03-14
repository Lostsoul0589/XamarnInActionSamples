using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Countr.Core
{
	public interface ICountersService
	{
		Task<Counter> AddNewCounter(string name);
		Task<List<Counter>> GetAllCounters();
		Task DeleteCounter(Counter counter);
		Task IncrementCounter(Counter counter);
	}
}

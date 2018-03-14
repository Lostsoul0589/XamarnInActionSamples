using System.Threading.Tasks;
using System.Collections.Generic;
namespace Countr.Core
{
	public interface ICountersRepository
	{
		Task Save(Counter counter);
		Task<List<Counter>> GetAll();
		Task Delete(Counter counter);
	}
}

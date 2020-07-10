using So.Demo.Common.Entities;
using System.Collections.Generic;

namespace So.Demo.Common.Services
{
    /// <summary>
    /// Creates instances of <see cref="Customer"/>
    /// </summary>
    public interface ICustomerFactory
    {
        /// <summary>
        /// Creates N instances of <see cref="Customer"/> and fills their properties with random data
        /// </summary>
        /// <param name="amount">Amount of customers to generate</param>
        /// <returns>A list of generated <see cref="Customer"/></returns>
        List<Customer> CreateRandom(int amount);
    }
}

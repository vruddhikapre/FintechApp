
using FinTech.Models;

namespace FinTech.Interfaces

{/// <summary>
/// Declaration of all methods in the repository.
/// </summary>
    public interface IFinTechRepository
    {


        /// <summary>
        /// returns a dictionary of string and dynamic objects with the analysis results.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, dynamic> analyzeBill();

        /// <summary>
        /// saves any changes made to the bills repository.
        /// </summary>
        /// <returns></returns>
        bool Save();

    }
}
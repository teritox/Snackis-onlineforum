using Snackis_Forum_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Services
{
    public interface IFulaOrdGateway
    {
        Task<List<FulaOrd>> GetBannedWords();

        Task<Models.FulaOrd> PostBannedWord(FulaOrd fultOrd);

        Task<string> GetFilteredItem(string message);
    }
}

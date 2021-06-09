using Snackis_Forum_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Services
{
    public interface IFulaOrdGateway
    {
        Task<List<FulaOrd>> GetBadWords();
        Task<FulaOrd> PostBadWord(FulaOrd fultOrd);
        Task<string> GetFilteredItem(string message);
        Task<FulaOrd> DeleteBadWord(int deleteId);

    }
}

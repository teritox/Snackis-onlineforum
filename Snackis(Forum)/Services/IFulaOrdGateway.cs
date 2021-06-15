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
        Task<FulaOrd> DeleteBadWord(int deleteId);
        Task<string> FilterBadWords(string message);
 

    }
}

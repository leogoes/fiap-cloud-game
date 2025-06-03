using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.Cloud.Games.Application.Libraries.Filters
{
    public class LibraryFilter
    {
        public Guid? UserId { get; set; }
        public Guid? LibraryId { get; set; }

        public void SetUserId(Guid? userId)
        {
            if (userId is null)
                return;

            UserId = userId;
        }

        public void SetLibraryId(Guid? libraryId)
        {
            if (libraryId is null)
                return;

            LibraryId = libraryId;
        }
    }
}

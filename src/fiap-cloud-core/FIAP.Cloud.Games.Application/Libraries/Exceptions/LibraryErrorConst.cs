using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.Cloud.Games.Application.Libraries.Exceptions
{
    public class LibraryErrorConst
    {
        public static readonly KeyValuePair<string, string> LIBRARY_NOT_FOUND = new("LIBRARY_NOT_FOUND", "Library could not be created.");
        public static readonly KeyValuePair<string, string> LIBRARY_COULD_NOT_BE_CREATED = new("LIBRARY_COULD_NOT_BE_CREATED", "Library could not be created.");
        public static readonly KeyValuePair<string, string> LIBRARY_COULD_NOT_BE_CHANGED = new("LIBRARY_COULD_NOT_BE_CHANGED", "Library could not be changed.");
    }
}

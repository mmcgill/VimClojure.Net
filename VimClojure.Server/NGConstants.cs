using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimClojure.Server
{
   static class NGConstants
   {
      public const char CHUNKTYPE_STDIN = '0';

      public const char CHUNKTYPE_STDIN_EOF = '.';

      public const char CHUNKTYPE_STDOUT = '1';

      public const char CHUNKTYPE_STDERR = '2';

      public const char CHUNKTYPE_EXIT = 'X';

      public const char CHUNKTYPE_ARGUMENT = 'A';

      public const char CHUNKTYPE_ENVIRONMENT = 'E';

      public const char CHUNKTYPE_COMMAND = 'C';

      public const char CHUNKTYPE_WORKINGDIRECTORY = 'D';
   }
}

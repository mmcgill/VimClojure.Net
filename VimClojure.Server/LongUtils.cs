using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimClojure.Server
{
   class LongUtils
   {
      public static void ToArray( long l, byte[] b, int offset )
      {
         b[offset + 3] = (byte) ( ( l >>  0 ) & 0xFF );
         b[offset + 2] = (byte) ( ( l >>  8 ) & 0xFF );
         b[offset + 1] = (byte) ( ( l >> 16 ) & 0xFF );
         b[offset + 0] = (byte) ( ( l >> 24 ) & 0xFF );
      }

      public static long FromArray( byte[] b, int offset )
      {
         return ( (long) b[offset + 0] << 24 ) +
                ( (long) b[offset + 1] << 16 ) +
                ( (long) b[offset + 2] <<  8 ) +
                ( (long) b[offset + 3] );
      }
   }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VimClojure.Server
{
   class NGSession
   {
      private readonly NetworkStream _stream;

      public NGSession( NetworkStream stream )
      {
         _stream = stream;
      }

      public void Process()
      {
         var output = new NGTextWriter( NGConstants.CHUNKTYPE_STDOUT, _stream );
         var error = new NGTextWriter( NGConstants.CHUNKTYPE_STDERR, _stream );
         var exit = new NGTextWriter( NGConstants.CHUNKTYPE_EXIT, _stream );

         var context = NGContext.Parse( _stream );

         Console.WriteLine( "Command: {0}", context.Command );
         Console.WriteLine( "Working Directory: {0}", context.Cwd );
         Console.WriteLine( "Args: {0}", string.Join( " ", context.Args ) );
         Console.WriteLine( "Environment:" );
         foreach ( var kv in context.Environment )
         {
            Console.WriteLine( "  {0}={1}", kv.Key, kv.Value );
         }

         output.WriteLine( "Hello, world!" );

         exit.WriteLine( 0 );
         output.Flush();
         error.Flush();
         exit.Flush();
      }
   }
}

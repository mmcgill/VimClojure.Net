using clojure.lang;
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
      static NGSession() {
         var require = RT.var( "clojure.core", "require" );
         require.invoke( Symbol.create( "vimclojure.nails" ) );
      }

      private readonly NetworkStream _stream;

      public NGSession( NetworkStream stream )
      {
         _stream = stream;
      }

      public void Process()
      {
         var context = NGContext.Parse( _stream );

         Console.WriteLine();
         Console.WriteLine( "Command: {0}", context.Command );
         Console.WriteLine( "Working Directory: {0}", context.Cwd );
         Console.WriteLine( "Args: {0}", string.Join( " ", context.Args ) );

         var nail = context.Args.First();
         int slashIndex = nail.IndexOf( "/" );
         var ns = slashIndex == -1 ? "vimclojure.nails" : nail.Substring( 0, slashIndex );
         var func = slashIndex == -1 ? nail : nail.Substring( slashIndex + 1 );

         var nailDriver = RT.var( ns, func );
         nailDriver.invoke( context );

         context.Exit.WriteLine( 0 );

         context.Out.Flush();
         context.Error.Flush();
         context.Exit.Flush();
      }
   }
}

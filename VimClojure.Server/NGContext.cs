using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimClojure.Server
{
   class NGContext
   {
      private readonly List<string> _args = new List<string>();

      private readonly Dictionary<string, string> _environment = new Dictionary<string,string>();

      public string Command { get; private set; }

      public IEnumerable<string> Args { get { return _args.AsReadOnly(); } }

      public string Cwd { get; private set; }

      public IEnumerable<KeyValuePair<string, string>> Environment { get { return _environment; } }

      public static NGContext Parse( Stream stream )
      {
         var ctx = new NGContext();

         while ( ctx.Command == null )
         {
            ParseChunk( stream, ctx );
         }

         return ctx;
      }

      private static void ParseChunk( Stream stream, NGContext ctx )
      {
         var header = ReadBytes( stream, 5 );

         var bytesToRead = LongUtils.FromArray( header, 0 );

         var payload = Encoding.ASCII.GetString( ReadBytes( stream, (int) bytesToRead ) );

         var chunkType = (char) header[4];
         switch ( chunkType )
         {
            case NGConstants.CHUNKTYPE_ARGUMENT:
               ctx._args.Add( payload );
               break;
            case NGConstants.CHUNKTYPE_ENVIRONMENT:
               var parts = payload.Split( '=' );
               ctx._environment[parts[0]] = parts[1];
               break;
            case NGConstants.CHUNKTYPE_COMMAND:
               ctx.Command = payload;
               break;
            case NGConstants.CHUNKTYPE_WORKINGDIRECTORY:
               ctx.Cwd = payload;
               break;
            default:
               throw new InvalidDataException( "Invalid chunk type: " + chunkType );
         }
      }

      private static byte[] ReadBytes( Stream stream, int numBytes )
      {
         var buffer = new byte[numBytes];
         var bytesRead = 0;

         while ( ( bytesRead += stream.Read( buffer, bytesRead, numBytes - bytesRead ) ) < numBytes - bytesRead );

         return buffer;
      }
   }
}

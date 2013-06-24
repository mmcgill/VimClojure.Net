using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VimClojure.Server
{
   class NGTextReader : System.IO.TextReader
   {
      private readonly Stream _stream;
      private readonly Queue<int> _remainingChars = new Queue<int>();
      private bool _isEof = false;

      public NGTextReader( Stream stream )
      {
         _stream = stream;
      }

      private void ReadNextPacket()
      {
         if ( _isEof )
            return;
         var header = ReadBytes( 5 );
         if ( _isEof )
            return;
         switch ( (char) header[4] )
         {
            case NGConstants.CHUNKTYPE_STDIN:
               var payloadBytes = LongUtils.FromArray( header, 0 );
               var payload = ReadBytes( (int) payloadBytes );
               if ( _isEof )
                  return;
               foreach ( var b in payload )
               {
                  _remainingChars.Enqueue( b );
               }
               break;
            case NGConstants.CHUNKTYPE_STDIN_EOF:
               _isEof = true;
               break;
            default:
               throw new InvalidDataException( "Invalid chunk type: " + header[4] );
         }
      }

      public override int Peek()
      {
         if ( _remainingChars.Count == 0 )
         {
            if ( !_isEof )
               ReadNextPacket();
         }
         return _remainingChars.Count > 0 ? _remainingChars.Peek() : -1;
      }

      public override int Read()
      {
         if ( _remainingChars.Count == 0 )
         {
            if ( !_isEof )
               ReadNextPacket();
         }
         return _remainingChars.Count > 0 ? _remainingChars.Dequeue() : -1;
      }

      private byte[] ReadBytes( int numBytes )
      {
         var buffer = new byte[numBytes];
         var totBytesRead = 0;
         while ( totBytesRead < numBytes )
         {
            var bytesRead = _stream.Read( buffer, totBytesRead, numBytes - totBytesRead );
            if ( bytesRead == 0 )
            {
               _isEof = true;
               return null;
            }
            totBytesRead += bytesRead;
         }

         return buffer;
      }
   }
}

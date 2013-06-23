using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimClojure.Server
{
   class NGTextWriter : TextWriter
   {
      private const int BufferSize = 512;

      private readonly Stream _stream;
      private readonly byte[] _header;
      private readonly MemoryStream _buffer;
      private readonly StreamWriter _writer;

      public override Encoding Encoding
      {
         get { throw new NotImplementedException(); }
      }

      public NGTextWriter( char headerChar, Stream stream )
      {
         _header = new byte[5];
         _header[4] = (byte) headerChar;
         _stream = stream;
         _buffer = new MemoryStream( BufferSize );
         _writer = new StreamWriter( _buffer );
      }

      public override void Write( char value )
      {
         _writer.Write( value );
         _writer.Flush();
         if ( _buffer.Length >= BufferSize )
         {
            Flush();
         }
      }

      public override void Flush()
      {
         if ( _buffer.Length == 0 )
         {
            return;
         }
         lock ( _stream )
         {
            LongUtils.ToArray( _buffer.Length, _header, 0 );
            _stream.Write( _header, 0, _header.Length );

            var bufferArray = _buffer.GetBuffer();
            _stream.Write( bufferArray, 0, (int)_buffer.Length );
            _stream.Flush();

            _buffer.Seek( 0, SeekOrigin.Begin );
         }
      }

      public override void Close()
      {
         Flush();
         base.Close();
      }
   }
}

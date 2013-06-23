using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VimClojure.Server
{
   class NGServer
   {
      const int DefaultPort = 2113;

      static void Main( string[] args )
      {
         var listener = new TcpListener( IPAddress.Any, DefaultPort );
         listener.Start();

         Console.WriteLine( "Listening on port " + DefaultPort );

         while ( true )
         {
            var socket = listener.AcceptSocket();

            Console.WriteLine( "Accepted client {0}", socket.RemoteEndPoint );

            using ( var networkStream = new NetworkStream( socket ) )
            {
               var session = new NGSession( networkStream );
               session.Process();
            }
         }

      }
   }
}

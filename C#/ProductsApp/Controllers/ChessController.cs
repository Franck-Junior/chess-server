using ProductsApp.Models;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    public class ChessController : ApiController
    {
        private List<int> playerGUIDs = new List<int>();

        private const int CHESS_ALLOWED_PLAYERS = 2;

        private const string PUSHER_CLUSTER = "mt1";

        private const string PUSHER_APP_ID = "626583";
        private const string PUSHER_APP_KEY = "05b6d74f8a0e925376a1";
        private const string PUSHER_APP_SECRET = "fed1a216748aa764f8bf";

        private const string PUSHER_CHANNEL_NAME = "my-channel";
        private const string PUSHER_EVENT_NAME = "my-event";

        [HttpPost]
        [ActionName("subscribe")]
        public async Task<IHttpActionResult> Subscribe()
        {
            lock (playerGUIDs)
            {
                if (playerGUIDs.Count < CHESS_ALLOWED_PLAYERS)
                {
                    Player player = new Player()
                    {
                        GUID = Guid.NewGuid(),
                        Channel = new Channel()
                        {
                            Cluster = PUSHER_CLUSTER,
                            ChannelName = PUSHER_CHANNEL_NAME,
                            EventName = PUSHER_EVENT_NAME
                        }
                    };

                    return Ok(player);
                }
                return StatusCode(HttpStatusCode.Forbidden);
            }
            

            var options = new PusherOptions
            {
                Cluster = PUSHER_CLUSTER,
                Encrypted = true
            };

            var pusher = new Pusher(
              PUSHER_APP_ID,
              PUSHER_APP_KEY,
              PUSHER_APP_SECRET,
              options);

            var result = await pusher.TriggerAsync(
              PUSHER_CHANNEL_NAME,
              PUSHER_EVENT_NAME,
              new { message = "hello world" });

            new PusherClient.Pusher(PUSHER_APP_KEY, new PusherClient.PusherOptions
            {
                Cluster = PUSHER_CLUSTER,
                Encrypted = true
            }).Subscribe(PUSHER_CHANNEL_NAME).Bind(PUSHER_EVENT_NAME, (dynamic) => Console.WriteLine("test"));

            return Ok();
        }
    }
}
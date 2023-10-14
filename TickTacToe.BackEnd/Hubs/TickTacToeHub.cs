using Microsoft.AspNetCore.SignalR;
using TickTacToe.BackEnd.Models;

namespace TickTacToe.BackEnd.Hubs
{
    public class TicTacToeHub : Hub
    {
        public async Task JoinSession(UserConnection userConnection)
        {
            string user = userConnection.User != string.Empty ? userConnection.User : "Guest";
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Session);

            if (userConnection.Session != string.Empty)
            {
                await Clients.Group(userConnection.Session).SendAsync("session", $"{user} has Joined");
                //await Clients.Caller.SendAsync("connected", OnConnectedAsync().IsCompleted, Context.ConnectionId);
            }
        }

        public async Task StartGame(UserConnection userConnection)
        {
            await Clients.Group(userConnection.Session).SendAsync("boardRoute", true);
        }
    }
}

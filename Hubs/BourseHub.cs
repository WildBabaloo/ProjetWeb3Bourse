using Microsoft.AspNetCore.SignalR;

namespace ProjetWeb3Bourse.Hubs {
    public class BourseHub: Hub {

        public async Task SendMessage() {
            await Clients.All.SendAsync("BourseChange");
        }

    }
}

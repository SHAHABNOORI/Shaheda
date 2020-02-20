using Shaheda.Enums;

namespace Shaheda.Commands.Modules.Clients
{
    public class ClientAddressCommand : CommandBase
    {
        public string Address { get; set; }

        public string BussinesAddress { get; set; }

        public Town Town { get; set; }

        public City City { get; set; }

        public string PostalCode { get; set; }
    }
}
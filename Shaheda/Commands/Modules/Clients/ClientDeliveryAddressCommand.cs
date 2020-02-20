namespace Shaheda.Commands.Modules.Clients
{
    public class ClientDeliveryAddressCommand: CommandBase
    {
        public string Address { get; set; }

        public string Name { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
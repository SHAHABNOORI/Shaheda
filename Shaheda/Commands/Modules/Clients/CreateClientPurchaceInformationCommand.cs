﻿namespace Shaheda.Commands.Modules.Clients
{
    public class CreateClientPurchaceInformationCommand:CommandBase
    {
        public int ClientCode { get; set; }

        public ClientPurchaceInformationCommand ClientPurchaceInformation { get; set; }

        public ClientExtraInformationCommand ClientExtraInformation { get; set; }

        public ClientPaymentCommand ClientPayment { get; set; }
    }
}
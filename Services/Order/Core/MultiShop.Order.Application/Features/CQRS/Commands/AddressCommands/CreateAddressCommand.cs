﻿namespace MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands
{
    public class CreateAddressCommand
    {
        /// <summary>
        ///     Kullanıcı id bilgisidir.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        ///     İlçe bilgisidir.
        /// </summary>
        public string District { get; set; }
        /// <summary>
        ///      Şehir bilgisidir.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        ///     Detay bilgisidir. Apt.no cad.no bina no gibi bilgilerdir.
        /// </summary>
        public string Detail { get; set; }
    }
}

﻿namespace ClinicAzure.Infrastructure.Settings
{
    public class AzureEntraIDSettings
    {
        public string ClientId { get; set; } = null!;
        public string TenantId { get; set; } = null!;
        public string Secret { get; set; } = null!;
    }
}

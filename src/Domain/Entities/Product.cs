using System;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string URL_Logo { get; private set; }
        public string api_key { get; private set; }
        public string assistant_id { get; private set; }
        public string realm_id { get; private set; }

        // Construtor para o EF Core
        protected Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            URL_Logo = string.Empty;
            api_key = string.Empty;
            assistant_id = string.Empty;
            realm_id = string.Empty;
        }

        public Product(string name, string description, string urlLogo, string apiKey, string assistantId, string realmId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty");

            if (string.IsNullOrWhiteSpace(urlLogo))
                throw new DomainException("Product url logo cannot be empty");

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new DomainException("Product api key cannot be empty");

            if (string.IsNullOrWhiteSpace(assistantId))
                throw new DomainException("Product assistant id cannot be empty");

            if (string.IsNullOrWhiteSpace(realmId))
                throw new DomainException("Product realm id cannot be empty");

            Name = name;
            Description = description;
            URL_Logo = urlLogo;
            api_key = apiKey;
            assistant_id = assistantId;
            realm_id = realmId;
        }

        public void UpdateDetails(string name, string description, string urlLogo, string apiKey, string assistantId, string realmId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty");

            if (string.IsNullOrWhiteSpace(urlLogo))
                throw new DomainException("Product url logo cannot be empty");

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new DomainException("Product api key cannot be empty");

            if (string.IsNullOrWhiteSpace(assistantId))
                throw new DomainException("Product assistant id cannot be empty");

            if (string.IsNullOrWhiteSpace(realmId))
                throw new DomainException("Product realm id cannot be empty");

            Name = name;
            Description = description;
            URL_Logo = urlLogo;
            api_key = apiKey;
            assistant_id = assistantId;
            realm_id = realmId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
namespace TwilightImperiumContentCreatorsCommunity.Models.Domain
{
    public class Owner
    {
        public string OwnerName { get; }

        public string Id { get; }

        public string IdentityProvider { get; }

        public string EmailAddress { get; }

        public bool PublicOwner { get; set; }

        public Owner(string ownerName, 
                     string ownerId, 
                     string identityProvider, 
                     string emailAddress, 
                     bool publicOwner)
        {
            this.OwnerName = ownerName;
            this.Id = ownerId;
            this.IdentityProvider = identityProvider;
            this.EmailAddress = emailAddress;
            this.PublicOwner = publicOwner;
        }

        public Owner()
        {

        }
    }
}
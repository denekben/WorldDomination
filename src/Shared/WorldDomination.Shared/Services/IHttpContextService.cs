using System;

namespace WorldDomination.Shared.Services
{ 
    public interface IHttpContextService
    {
        public Guid GetCurrentUserId();
    }
}

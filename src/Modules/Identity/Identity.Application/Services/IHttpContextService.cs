namespace Identity.Application.Services
{ 
    public interface IHttpContextService
    {
        public Guid GetCurrentUserId();
    }
}

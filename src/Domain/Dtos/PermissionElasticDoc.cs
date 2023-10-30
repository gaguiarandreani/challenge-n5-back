namespace SecurityApi.Domain.Dtos
{
    public class PermissionElasticDoc
    {
        public string Action { get; set; }
        public PermissionDto Permission { get; set; }
    }
}
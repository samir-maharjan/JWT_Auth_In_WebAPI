

using JWT_token_auth_Demo.Models;

namespace JWT_token_auth_Demo.Viewmodel
{
    public class LoggedUserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
       // public bool IsSuperAdmin => AssociatedRoles != null && AssociatedRoles.Any(x => x == EnumApplicationUserType.SuperAdmin.ToString());
        public IList<string> AssociatedRoles { get; set; }
        //as Dynamic Roles can contain other roles too
        public int? RelatedId { get; set; }
        public EnumApplicationUserType? UserType { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}

using FollowTheTask.TransferObjects.Role.DataObjects;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Auth
{
    public class UserRole : IRole<int>
    {
        public int Id => Dto?.Id ?? 0;

        public string Name
        {
            get { return Dto?.Name; }
            set
            {
                if (Dto != null)
                {
                    Dto.Name = value;
                }
            }
        }

        public RoleDto Dto { get; set; }


        public static UserRole FromDto(RoleDto dto)
        {
            return dto != null ? new UserRole {Dto = dto} : null;
        }
    }
}
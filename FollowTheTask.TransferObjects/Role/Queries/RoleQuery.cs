namespace FollowTheTask.TransferObjects.Role.Queries
{
    public class RoleQuery : Query
    {
        public int? Id { get; set; }

        public string Name { get; set; }


        public static RoleQuery WithId(int id)
        {
            return new RoleQuery {Id = id};
        }

        public static RoleQuery WithName(string name)
        {
            return new RoleQuery {Name = name};
        }
    }
}
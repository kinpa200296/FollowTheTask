namespace FollowTheTask.TransferObjects.User.Queries
{
    public class UserQuery : Query
    {
        public int? Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }


        public static UserQuery WithId(int id)
        {
            return new UserQuery {Id = id};
        }

        public static UserQuery WithUsername(string username)
        {
            return new UserQuery {Username = username};
        }

        public static UserQuery WithEmail(string email)
        {
            return new UserQuery {Email = email};
        }
    }
}
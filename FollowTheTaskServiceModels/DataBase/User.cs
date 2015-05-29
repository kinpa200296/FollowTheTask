using System.Runtime.Serialization;

namespace FollowTheTaskServiceModels.DataBase
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int? ManagerId { get; set; }

        [DataMember]
        public int? WorkerId { get; set; }
    }
}

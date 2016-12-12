using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Team.ViewModels
{
    public class TeamInfoViewModel : ModelViewModel
    {
        public string Name { get; set; }

        public int LeaderId { get; set; }

        public string Leader { get; set; }
    }
}
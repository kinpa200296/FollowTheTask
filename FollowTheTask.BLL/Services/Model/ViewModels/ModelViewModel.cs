using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.BLL.Services.Model.ViewModels
{
    public class ModelViewModel : ViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
    }
}
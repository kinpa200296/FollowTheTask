using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.Model.Commands
{
    public class UpdateModelCommand<TModelDto> : Command where TModelDto : ModelDto
    {
        public TModelDto ModelDto { get; set; }
    }
}
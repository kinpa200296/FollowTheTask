using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.Feature.DataObjects
{
    public class FeatureDto : ModelDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public int TeamId { get; set; }
    }
}
﻿using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.Notification.DataObjects
{
    public class NotificationInfoDto : ModelDto
    {
        public int TargetId { get; set; }

        public string Target { get; set; }

        public int ActionSourceId { get; set; }

        public int ActionTypeId { get; set; }

        public int SenderId { get; set; }

        public string Sender { get; set; }

        public int ReceiverId { get; set; }

        public string Receiver { get; set; }
    }
}
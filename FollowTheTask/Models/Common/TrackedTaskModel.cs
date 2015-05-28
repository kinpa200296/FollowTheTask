using System;
using System.ComponentModel.DataAnnotations;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Common
{
    public class TrackedTaskModel
    {
        public TrackedTaskModel()
        {
        }

        public TrackedTaskModel(TrackedTask trackedTask)
        {
            Id = trackedTask.Id;
            Title = trackedTask.Title;
            Description = trackedTask.Description;
            IssuedDate = trackedTask.IssuedDate;
            CompletionDate = trackedTask.CompletionDate ?? DateTime.Now;
            DeadLine = trackedTask.DeadLine;
            IsFinished = trackedTask.IsFinished;
            HoursSpent = trackedTask.HoursSpent ?? 0;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название задачи")]
        [Display(Name = "Название задачи")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите описание задачи")]
        [Display(Name = "Описание задачи")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите дату выдачи")]
        [Display(Name = "Дата выдачи")]
        public DateTime IssuedDate { get; set; }

        [Display(Name = "Дата завершения")]
        public DateTime CompletionDate { get; set; }

        [Required(ErrorMessage = "Введите крайний срок завершения")]
        [Display(Name = "Крайний срок завершения")]
        public DateTime DeadLine { get; set; }

        [Display(Name = "Выполнена")]
        public bool IsFinished { get; set; }

        //[Required(ErrorMessage = "Введите количество потраченных часов")]
        //[RegularExpression(@"[1-9][0-9]*", ErrorMessage = "Введите челое число")]
        [Display(Name = "Потраченные часы")]
        public int HoursSpent { get; set; }        
    }
}
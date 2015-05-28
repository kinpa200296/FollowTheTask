using System;
using System.ComponentModel.DataAnnotations;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Common
{
    public class QuestModel
    {
        public QuestModel()
        {
        }

        public QuestModel(Quest quest)
        {
            Id = quest.Id;
            Title = quest.Title;
            Description = quest.Description;
            IssuedDate = quest.IssuedDate;
            CompletionDate = quest.CompletionDate ?? DateTime.Now;
            DeadLine = quest.DeadLine;
            IsFinished = quest.IsFinished;
            HoursSpent = quest.HoursSpent ?? 0;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название подзадачи")]
        [Display(Name = "Название подзадачи")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите описание подзадачи")]
        [Display(Name = "Описание подзадачи")]
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
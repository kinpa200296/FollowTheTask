using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Models.DataBase;
using Microsoft.AspNet.Identity.Owin;

namespace FollowTheTask.Controllers
{
    public class QuestsController : Controller
    {
        private ApplicationContext AppContext
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationContext>(); }
        }

        // GET: Quests
        public ActionResult Index()
        {
            var quests = AppContext.Quests.Include(q => q.TrackedTask).Include(q => q.Worker);
            return View(quests.ToList());
        }

        // GET: Quests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = AppContext.Quests.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            return View(quest);
        }

        // GET: Quests/Create
        public ActionResult Create()
        {
            ViewBag.TrackedTaskId = new SelectList(AppContext.TrackedTasks, "Id", "Title");
            ViewBag.WorkerId = new SelectList(AppContext.Workers, "Id", "UserId");
            return View();
        }

        // POST: Quests/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,IssuedDate,CompletionDate,IsFinished,TimeSpent,TrackedTaskId,WorkerId")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                AppContext.Quests.Add(quest);
                AppContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrackedTaskId = new SelectList(AppContext.TrackedTasks, "Id", "Title", quest.TrackedTaskId);
            ViewBag.WorkerId = new SelectList(AppContext.Workers, "Id", "UserId", quest.WorkerId);
            return View(quest);
        }

        // GET: Quests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = AppContext.Quests.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrackedTaskId = new SelectList(AppContext.TrackedTasks, "Id", "Title", quest.TrackedTaskId);
            ViewBag.WorkerId = new SelectList(AppContext.Workers, "Id", "UserId", quest.WorkerId);
            return View(quest);
        }

        // POST: Quests/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,IssuedDate,CompletionDate,IsFinished,TimeSpent,TrackedTaskId,WorkerId")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                AppContext.Entry(quest).State = EntityState.Modified;
                AppContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrackedTaskId = new SelectList(AppContext.TrackedTasks, "Id", "Title", quest.TrackedTaskId);
            ViewBag.WorkerId = new SelectList(AppContext.Workers, "Id", "UserId", quest.WorkerId);
            return View(quest);
        }

        // GET: Quests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = AppContext.Quests.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            return View(quest);
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quest quest = AppContext.Quests.Find(id);
            AppContext.Quests.Remove(quest);
            AppContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AppContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

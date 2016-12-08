using FollowTheTask.DAL.Contexts;

namespace FollowTheTask.Sandbox
{
    class Program
    {
        static void Main()
        {
            AppData.Set();
            using (var context = new FollowTheTaskContext())
            {
            }
        }
    }
}

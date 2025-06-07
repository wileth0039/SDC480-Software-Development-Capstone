namespace IsItConvergent.Models.ViewModels
{
    public class HomePage_VM
    {
        public string Title { get; set; } = "Is It Convergent?";
        public int AccessLevel { get; set; } = 0; // 0 = Guest, 1 = User, 2 = Moderator, 3 = Admin
        public LinuxApp_VM NewAppForm { get; set; } = new LinuxApp_VM();
        public List<LinuxApp_VM>? LinuxApps { get; set; }
    }
}

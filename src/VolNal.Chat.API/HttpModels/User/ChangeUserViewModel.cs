namespace VolNal.Chat.API.Controllers;

public class ChangeUserViewModel : UserViewModelBase
{
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public string CurrentPassword { get; set; }
    public string ConfirmPassword { get; set; }
    public string NewPassword { get; set; }
}
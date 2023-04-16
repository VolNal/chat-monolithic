namespace VolNal.Chat.API.Controllers;

public class AuthorizeUserViewModel : UserViewModelBase
{
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public int GroupId { get; set; }
}
namespace Auto.Helpers;

public static class RoleNameHelper
{
    public static string RoleToString(string role)
    {
        return role switch
        {
            Consts.Student => "Студент",
            Consts.Instructor => "Инструктор",
            Consts.Admin => "Администратор",
            _ => role
        };
    }
}
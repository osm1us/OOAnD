namespace SpaceBattle.Lib;

public class RegisterIoCDependencyAuthCheck : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Authorization.Check",
            (object[] args) =>
            {
                var subjectId = (string)args[0];
                var action = (string)args[1];
                var objectId = (string)args[2];

                var permissions = IoC.Resolve<IDictionary<string, IEnumerable<string>>>(
                    "Authorization.GetPermissions",
                    subjectId);

                if (permissions.ContainsKey("*"))
                {
                    return (object)true;
                }

                if (!permissions.ContainsKey(objectId))
                {
                    return (object)false;
                }

                var objectPermissions = permissions[objectId];
                return (object)(objectPermissions.Contains("*") || objectPermissions.Contains(action));
            }
        ).Execute();
    }
}

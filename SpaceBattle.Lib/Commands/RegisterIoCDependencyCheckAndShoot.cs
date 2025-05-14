namespace SpaceBattle.Lib;

public class RegisterIoCDependencyCheckAndShoot : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "AddAmmo",
            (object[] args) =>
            {
                var ammo = (IAddAmmo)args[0];
                var count = (int)args[1];
                return new AddAmmoCommand(ammo, count);
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "RemoveAmmo",
            (object[] args) =>
            {
                var ammo = (IRemoveAmmo)args[0];
                var count = (int)args[1];
                return new RemoveAmmoCommand(ammo, count);
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "CheckAmmo",
            (object[] args) =>
            {
                var ammo = (IRemoveAmmo)args[0];
                return new CheckAmmoCommand(ammo);
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "CheckAndShoot",
            (object[] args) =>
            {
                var auth = (ICommand)args[0];
                var check = (ICommand)args[1];
                var remove = (ICommand)args[2];
                var shot = (ICommand)args[3];
                return new SimpleMacroCommand(new List<ICommand> { auth, check, remove, shot });
            }
        ).Execute();
    }
}

public class CheckAmmoCommand : ICommand
{
    private readonly IRemoveAmmo _ammo;

    public CheckAmmoCommand(IRemoveAmmo ammo)
    {
        _ammo = ammo;
    }

    public void Execute()
    {
        if (!_ammo.HasAmmo())
        {
            throw new InvalidOperationException("Нет боезапаса");
        }
    }
}

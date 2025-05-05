namespace SpaceBattle.Lib;

public class RemoveAmmoCommand : ICommand
{
    private readonly IRemoveAmmo _ammo;
    private readonly int _amount;

    public RemoveAmmoCommand(IRemoveAmmo ammo, int amount = 1)
    {
        _ammo = ammo;
        _amount = amount;
    }

    public void Execute()
    {
        _ammo.Remove(_amount);
    }
}

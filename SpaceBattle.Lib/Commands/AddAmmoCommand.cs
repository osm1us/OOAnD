public class AddAmmoCommand : ICommand
{
    private readonly IAddAmmo _ammo;
    private readonly int _amount;

    public AddAmmoCommand(IAddAmmo ammo, int amount)
    {
        _ammo = ammo;
        _amount = amount;
    }

    public void Execute()
    {
        _ammo.Add(_amount);
    }
}

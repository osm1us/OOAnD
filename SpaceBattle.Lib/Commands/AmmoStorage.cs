namespace SpaceBattle.Lib;

public class AmmoStorage : IAddAmmo, IRemoveAmmo
{
    private int _ammo;

    public void Add(int count)
    {
        _ammo += count;
    }

    public void Remove(int count)
    {
        if (_ammo < count)
        {
            throw new InvalidOperationException("Недостаточно боезапаса");
        }

        _ammo -= count;
    }

    public bool HasAmmo()
    {
        return _ammo > 0;
    }
}

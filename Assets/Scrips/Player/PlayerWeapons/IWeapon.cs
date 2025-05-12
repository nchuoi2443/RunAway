using UnityEngine;

public interface IWeapon
{
    public void Attack();
    public WeaponInfo GetWeaponInfo();

    public void FollowingOffSet();

    public void WeaponUpdate();
}

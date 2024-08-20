public interface IVisitor
{
    void Visit(Health health);
    void UnVisit(Health health);

    // void Visit(PlayerWeaponHolder playerWeaponHolder);
    // void UnVisit(PlayerWeaponHolder playerWeaponHolder);
}

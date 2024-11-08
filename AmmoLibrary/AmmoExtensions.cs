namespace AmmoLibrary
{
    public static class AmmoExtensions
    {
        public static int SerializeAmmo(this Ammo ammo)
        {
            if (ammo is ExplosiveAmmo)
            {
                return 0;
            }
            if (ammo is PiercingAmmo)
            {
                return 1;
            }
            if (ammo is SupersonicAmmo)
            {
                return 2;
            }

            return -1;
        }
    }
}

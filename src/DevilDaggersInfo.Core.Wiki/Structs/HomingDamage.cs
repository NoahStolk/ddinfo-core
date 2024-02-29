namespace DevilDaggersInfo.Core.Wiki.Structs;

// TODO: Add percentage amount for level 3 and 4 daggers (25% for Skull I + old Transmuted Skull I, 100% for everything else).
public readonly record struct HomingDamage(int Level3HomingDamage, int Level4HomingDamage, int Level4SplashDamage);

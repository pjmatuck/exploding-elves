using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Models/Elf")]
public class ElfModel : ScriptableObject
{
    [SerializeField] float speed;
    [SerializeField] ElfColor color;
    [SerializeField] float spawningTime;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;

    public float Speed => speed;
    public ElfColor Color => color;
    public float SpawningTime => spawningTime;
    public Material ActiveMaterial => activeMaterial;
    public Material InactiveMaterial => inactiveMaterial;
}
public enum ElfColor
{
    Black, Red, White, Blue
}
namespace Interfaces
{
    public interface IDieAble
    {
        public float Health { get; set; }
        void Die(float waitBeforeDestroy);
    }
}
public class EnamyDead : Die
{
    public override void OnDead()
    {
        Destroy(gameObject);
    }
}

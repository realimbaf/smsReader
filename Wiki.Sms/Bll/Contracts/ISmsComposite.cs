namespace Wiki.Sms.Bll.Contracts
{
    public interface ISmsComposite
    {
        void Compose(params ISmsHandler[] handlers);
    }
}

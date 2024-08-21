public interface IPlayerElement
{
    void Accept(IVisitor visitor);
    void Deline(IVisitor visitor);

}

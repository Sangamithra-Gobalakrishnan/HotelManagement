namespace UserAuthentication.Interfaces
{
    public interface IGenerateToken<L>
    {
        public string GenerateToken(L loginDTO);
    }
}

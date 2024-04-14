namespace trainingEF.Configuration;

public class JwtConfig
{
    public JwtConfig(string _secret)
    {
        Secret = _secret;
    }

    public string Secret { get; set; }
}

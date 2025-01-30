namespace classes;
public class Env
{
    public static Env INS { get; private set; }
    private string _baseHttpUrl;

    private string _baseWSurl;

    private string _encryptorPassword;

    private string _encryptorSalt;

    public void Assign()
    {
        INS = this;
    }
    public static Env Get()
    {
        return INS;
    }
    public string BaseHttpUrl()
    {
        return this._baseHttpUrl;
    }
    public string BaseWSurl()
    {
        return this._baseWSurl;
    }
    public string EncryptorPassword()
    {
        return this._encryptorPassword;
    }
    public string EncryptorSalt()
    {
        return this._encryptorSalt;
    }
}
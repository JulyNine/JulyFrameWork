using System.Collections.Generic;

public partial class GameProtocol
{
    public static string GetAccounts_URL = HttpRootURL;

    public static Dictionary<string, string> GetAccounts_ParamDict = new Dictionary<string, string>();


    //注册
    public static string Register_URL = HttpRootURL + "Register.php";

    public static Dictionary<string, object> Register_ParamDict = new Dictionary<string, object>
    {
        {"name", ""}
        //  { "password", ""},
    };

   

    //创建角色
    public static string CreatePlayer_URL = HttpRootURL + "users/createUser";

    public static Dictionary<string, string> CreatePlayer_ParamDict = new Dictionary<string, string>
    {
        {"userId", ""},
        {"nickName", ""},
        {"icon", ""},
        {"sex", ""}
    };

    //获取玩家基础信息
    public static string GetPlayerInfo_URL = HttpRootURL + "users/getUserData";

    public static Dictionary<string, string> GetPlayer_ParamDict = new Dictionary<string, string>
    {
        {"userId", ""}
    };


    //抽取英雄
    public static string CreateHero_URL = HttpRootURL + "avatar";

    public static Dictionary<string, string> CreateHero_ParamDict = new Dictionary<string, string>
    {
        {"userId", ""}
    };
    //public static string HttpRootURL = "http://47.92.0.165/gametest/";

    public static string HttpRootURL
    {
        get
        {
            switch (ConfigSettings.Instance.server)
            {
                case Server.DevelopeServer:
                    return "https://www.matrixsci.cn/FakeMiss/";
                case Server.TestServer:
                    return "https://www.matrixsci.cn/FakeMiss/";
                case Server.StableTestServer:
                    return "https://www.matrixsci.cn/FakeMiss/";
                case Server.StableDevelopeServer:
                    return "https://www.matrixsci.cn/FakeMiss/";
                default:
                    return "https://www.matrixsci.cn/FakeMiss/";
            }
        }
    }
}
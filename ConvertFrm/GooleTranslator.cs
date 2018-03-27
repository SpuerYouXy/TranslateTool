using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ConvertFrm
{
    public class GooleTranslator : IDisposable
    {
        private HttpClient _httpClient;
        public GooleTranslator()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// 谷歌翻译
        /// </summary>
        /// <param name="text">待翻译文本</param>
        /// <param name="fromLanguage">自动检测：auto</param>
        /// <param name="toLanguage">中文：zh-CN，英文：en</param>
        /// <returns>翻译后文本</returns>
        public async Task<string> GoogleTranslate(string text, string fromLanguage, string toLanguage)
        {
            var gooleUrl = "https://translate.google.cn/";

            var baseHtml =await _httpClient.GetStringAsync(gooleUrl);
            Regex re = new Regex(@"(?<=TKK=)(.*?)(?=\);)");

            var TKKStr = re.Match(baseHtml).ToString() + ")";//在返回的HTML中正则匹配TKK的JS代码

            var TKK = ExecuteScript(TKKStr, TKKStr);//执行TKK代码，得到TKK值

            var jsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gettk.js");
            var GetTkkJS = File.ReadAllText(jsPath);

            var tk = ExecuteScript("tk(\"" + text + "\",\"" + TKK + "\")", GetTkkJS);
            
            var q = HttpUtility.UrlEncode(text);
            string googleTransUrl = $"https://translate.google.cn/translate_a/single?client=t&sl=zh-CN&tl=en&hl=zh-CN&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&ie=UTF-8&oe=UTF-8&source=btn&ssel=0&tsel=0&kc=0&tk={tk}&q={q}";
            var json= await _httpClient.GetStringAsync(googleTransUrl);
            dynamic TempResult = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            string ResultText = Convert.ToString(TempResult[0][0][0]);

            return ResultText;
        }

        /// <summary>
        /// 执行JS
        /// </summary>
        /// <param name="sExpression">参数体</param>
        /// <param name="sCode">JavaScript代码的字符串</param>
        /// <returns></returns>
        private string ExecuteScript(string sExpression, string sCode)
        {
            MSScriptControl.ScriptControl scriptControl = new MSScriptControl.ScriptControl();
            scriptControl.UseSafeSubset = true;
            scriptControl.Language = "JScript";
            scriptControl.AddCode(sCode);
            try
            {
                string str = scriptControl.Eval(sExpression).ToString();
                return str;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return null;
        }

        public void Dispose(bool flag)
        {
            _httpClient.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

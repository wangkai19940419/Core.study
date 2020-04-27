using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Wk.Study.Common.Helper
{
    public static class StringHelper
    {
        private static String[] Ls_ShZ = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖", "拾" };
        private static String[] Ls_DW_Zh = { "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万" };
        private static String[] Num_DW = { "", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万" };
        private static String[] Ls_DW_X = { "角", "分" };
        /// <summary>
        /// 金额小写转中文大写。
        /// 整数支持到万亿；小数部分支持到分(超过两位将进行Banker舍入法处理)
        /// </summary>
        /// <param name="Num">需要转换的双精度浮点数</param>
        /// <returns>转换后的字符串</returns>
        public static String NumGetStr(this Double Num)
        {
            Boolean iXSh_bool = false;//是否含有小数，默认没有(0则视为没有)
            Boolean iZhSh_bool = true;//是否含有整数,默认有(0则视为没有)

            string NumStr;//整个数字字符串
            string NumStr_Zh;//整数部分
            string NumSr_X = "";//小数部分
            string NumStr_DQ;//当前的数字字符
            string NumStr_R = "";//返回的字符串

            Num = Math.Round(Num, 2);//四舍五入取两位

            //各种非正常情况处理
            if (Num < 0)
                return "不转换欠条";
            if (Num > 9999999999999.99)
                return "很难想象谁会有这么多钱！";
            if (Num == 0)
                return Ls_ShZ[0];

            //判断是否有整数
            if (Num < 1.00)
                iZhSh_bool = false;

            NumStr = Num.ToString();

            NumStr_Zh = NumStr;//默认只有整数部分
            if (NumStr_Zh.Contains("."))
            {//分开整数与小数处理
                NumStr_Zh = NumStr.Substring(0, NumStr.IndexOf("."));
                NumSr_X = NumStr.Substring((NumStr.IndexOf(".") + 1), (NumStr.Length - NumStr.IndexOf(".") - 1));
                iXSh_bool = true;
            }


            if (NumSr_X == "" || int.Parse(NumSr_X) <= 0)
            {//判断是否含有小数部分
                iXSh_bool = false;
            }

            if (iZhSh_bool)
            {//整数部分处理
                NumStr_Zh = string.Join("", (NumStr_Zh).Reverse());//反转字符串

                for (int a = 0; a < NumStr_Zh.Length; a++)
                {//整数部分转换
                    NumStr_DQ = NumStr_Zh.Substring(a, 1);
                    if (int.Parse(NumStr_DQ) != 0)
                        NumStr_R = Ls_ShZ[int.Parse(NumStr_DQ)] + Ls_DW_Zh[a] + NumStr_R;
                    else if (a == 0 || a == 4 || a == 8)
                    {
                        if (NumStr_Zh.Length > 8 && a == 4)
                            continue;
                        NumStr_R = Ls_DW_Zh[a] + NumStr_R;
                    }
                    else if (int.Parse(NumStr_Zh.Substring(a - 1, 1)) != 0)
                        NumStr_R = Ls_ShZ[int.Parse(NumStr_DQ)] + NumStr_R;

                }

                if (!iXSh_bool)
                    return NumStr_R + "整";

                //NumStr_R += "零";
            }

            for (int b = 0; b < NumSr_X.Length; b++)
            {//小数部分转换
                NumStr_DQ = NumSr_X.Substring(b, 1);
                if (int.Parse(NumStr_DQ) != 0)
                    NumStr_R += Ls_ShZ[int.Parse(NumStr_DQ)] + Ls_DW_X[b];
                else if (b != 1 && iZhSh_bool)
                    NumStr_R += Ls_ShZ[int.Parse(NumStr_DQ)];
            }

            return NumStr_R;

        }
        /// <summary>
        /// 16进制字符串转bytes
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStrToBytes(this string hexString)
        {
            return Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }

        public static string ToJsonString(this object obj,
            DefaultContractResolver resolver = null)
        {
            if (obj == null)
            {
                return "{}";
            }

            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 3,
                ContractResolver = resolver ?? new DefaultContractResolver()
            });
        }

        private static List<string> emptyStrs = new List<string>()
        {
            ' '.ToString(), '\r'.ToString(), '\n'.ToString(), '\t'.ToString()
        };

        public static string RemoveEmptyString(this string s)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            foreach (var str in emptyStrs)
            {
                s = s.Replace(str, "");
            }

            return s;
        }

        public static string GetMd5String(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new Exception("加密参数不能为空");
            }

            using (var md5 = MD5.Create())
            {
                var rst = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
                return BitConverter.ToString(rst).Replace("-", "").ToUpper();
            }
        }

        /// <summary>
        /// 获取自定义错误
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionMessage(this Exception ex)
        {
            if (ex == null)
            {
                return "";
            }

            //if (ex is BizException bex)
            //{
            //    return bex.BizMsg;
            //}
            else if (ex is ValidationException vex)
            {
                return vex.ValidationResult.ErrorMessage.Replace("'", "");
            }
            else
            {
                return "系统异常";
            }
        }

        /// <summary>
        /// 是否符合登录密码规则
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsPayPwd(this string PayPwd)
        {
            if (string.IsNullOrEmpty(PayPwd))
            {
                return false;
            }

            var res = System.Text.RegularExpressions.Regex.IsMatch(PayPwd, @"[0-9]{6}");
            return res;
        }

        /// <summary>
        /// 是否符合登录密码规则
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsLoginPwd(this string LoginPwd)
        {
            if (string.IsNullOrEmpty(LoginPwd))
            {
                return false;
            }

            var res = System.Text.RegularExpressions.Regex.IsMatch(LoginPwd,
                @"^ ([A - Z] |[a - z] |[0 - 9] |[`~!@#$%^&*()+=|{}':;',\\\\[\\\\].<>/?~！@#￥%……&*（）——+|{}【】‘；：”“'。，、？]){6,20}$");
            return res;
        }

        /// <summary>
        /// 是否为手机号     false:非手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsMobile(this string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
            {
                return false;
            }

            var res = System.Text.RegularExpressions.Regex.IsMatch(mobile,
                @"^(0|86|17951)?(13[0-9]|15[012356789]|17[013678]|18[0-9]|14[57])[0-9]{8}$");
            return res;
        }

        /// <summary>
        /// /// 获取后几位数
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="num">返回的具体位数</param>
        /// <returns>返回结果的字符串</returns>
        public static string GetLastStr(string str, int num)
        {
            int count = 0;
            if (str.Length > num)
            {
                count = str.Length - num;
                str = str.Substring(count, num);
            }

            return str;
        }


        /// <summary>
        /// 去除英文提示，只留下中文
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string KeepChinese(this string s)
        {
            var temp = System.Text.RegularExpressions.Regex.Replace(s + "", "[a-zA-Z]", "");
            var symbols = new List<string>() { ":", "。" };
            foreach (var symbol in symbols)
            {
                temp = temp.Replace(symbol, "");
            }

            if (String.IsNullOrEmpty(temp))
            {
                return "消息异常";
            }

            return temp;
        }

        /// <summary>
        /// 生成订单序号
        /// </summary>
        /// <param name="OldOrderNo"></param>
        /// <param name="ParamID"></param>
        /// <param name="Header"></param>
        /// <param name="orderNoSign"></param>
        /// <returns></returns>
        public static string RandomOrderNo(string OldOrderNo, int ParamID, string Header, string orderNoSign)
        {
            string Body = "";
            string curDate = DateTime.Now.ToString("yyyyMMdd");
            int LastNum = 1;
            if (ParamID.ToString().Length <= 6)
                Body = ParamID.ToString("000000");
            else
                Body = ParamID.ToString();
            if (string.IsNullOrEmpty(OldOrderNo))
                return orderNoSign + Header + Body + curDate + LastNum.ToString("0000");
            string lastStr = GetLastStr(OldOrderNo, 4);
            LastNum = Convert.ToInt32(lastStr) + 1;
            //if (Number > 0)
            //    LastNum += Number;
            return orderNoSign + Header + Body + curDate + LastNum.ToString("0000");
        }

        public static List<int> StrToIntList(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new Exception("字符串不能为空");
            }

            return str.Split(',').Select(w => Convert.ToInt32(w)).ToList();
        }

        /// <summary>
        /// 手机号处理
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MaskPhone(this string s)
        {
            if (String.IsNullOrEmpty(s) || s.Length < 11)
            {
                return s;
            }

            return s.Substring(0, 3) + "****" + s.Substring(7, 4);
        }

        /// 转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal StrToInt(this string str)
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>  
        ///   
        /// 将对象属性转换为key-value对  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        public static Dictionary<String, String> ToMap(Object o)
        {
            Dictionary<String, String> map = new Dictionary<String, String>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                var v = (DescriptionAttribute[])p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                map.Add(p.Name, v[0].Description);
            }

            return map;
        }

        #region

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public static string CreateValidateCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }

            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }

            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }

            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }

            return validateNumberStr;
        }

        /// <summary>
        /// Rea加密
        /// </summary>
        /// <param name="normaltxt"></param>
        /// <returns></returns>
        public static string RSAEncrypt(this string Str)
        {
            var bytes = Encoding.Default.GetBytes(Str);
            var encryptBytes = new RSACryptoServiceProvider(new CspParameters()).Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }

        #endregion

        // public static string HtmlEnocding (this )
        /// <summary>
        /// 获取本月的第几周
        /// </summary>
        /// <param name="daytime"></param>
        /// <returns></returns>
        public static int WeekNumInMonth(this DateTime daytime)
        {
            int dayInMonth = daytime.Day;
            //本月第一天  
            DateTime firstDay = daytime.AddDays(1 - daytime.Day);
            //本月第一天是周几  
            int weekday = (int)firstDay.DayOfWeek == 0 ? 7 : (int)firstDay.DayOfWeek;
            //本月第一周有几天  
            int firstWeekEndDay = 7 - (weekday - 1);
            //当前日期和第一周之差  
            int diffday = dayInMonth - firstWeekEndDay;
            diffday = diffday > 0 ? diffday : 1;
            //当前是第几周,如果整除7就减一天  
            int WeekNumInMonth = ((diffday % 7) == 0
                                     ? (diffday / 7 - 1)
                                     : (diffday / 7)) + 1 + (dayInMonth > firstWeekEndDay ? 1 : 0);
            return WeekNumInMonth;
        }

        /// <summary>
        /// 得到当天是当月的第几周
        /// </summary>
        /// <param name="day"></param>
        /// <param name="WeekStart"></param>
        /// <returns></returns>
        public static int WeekOfMonth(this DateTime day, int WeekStart)
        {
            //WeekStart                                                                      
            //1表示 周一至周日 为一周                                                        
            //2表示 周日至周六 为一周                                                        
            DateTime FirstofMonth;
            FirstofMonth = Convert.ToDateTime(day.Date.Year + "-" + day.Date.Month + "-" + 1);
            int i = (int)FirstofMonth.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }

            if (WeekStart == 1)
            {
                return (day.Date.Day + i - 2) / 7 + 1;
            }

            if (WeekStart == 2)
            {
                return (day.Date.Day + i - 1) / 7;
            }

            return 0;
            //错误返回值0                                                                    
        }

        /// <summary>
        /// 获取季度
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int QuarterlyMonth(this DateTime month)
        {
            int qua = 1;
            switch (month.Month)
            {
                case 1:
                case 2:
                case 3:
                    qua = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    qua = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    qua = 3;
                    break;
                case 10:
                case 11:
                case 12:
                    qua = 4;
                    break;
            }

            return qua;
        }

        /// <summary>
        /// 有字符会被截断，所以要选长的那个
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static string GetLongerOne(string str1, string str2)
        {
            return str1?.Length > str2?.Length ? str1 : str2;
        }

        /// <summary>
        /// 中文逗号转英文逗号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDBC(this String str)
        {
            char[] c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        //加权因子
        private static readonly int[] _factors = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
        //验证位置
        private static readonly int[] _codes = { 1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        /// <summary>
        /// 验证是否为身份证号
        /// </summary>
        /// <param name="idcard">身份证号</param>
        /// <returns></returns>
        public static bool Check(this string idcard)
        {
            if (idcard.Length == 18)
            {
                if (ValidBirthday(idcard) && ValidateCode(idcard))
                {
                    return true;
                }
            }
            else if (idcard.Length == 15)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证生日
        /// </summary>
        /// <param name="idcard">身份证号</param>
        /// <returns></returns>
        private static bool ValidBirthday(string idcard)
        {
            string year = idcard.Substring(6, 4);
            string month = idcard.Substring(10, 2);
            string day = idcard.Substring(12, 2);
            DateTime date;
            string xdate = year + month + day;
            bool result = DateTime.TryParseExact(xdate, "yyyyMMdd", new DateTimeFormatInfo(), DateTimeStyles.AdjustToUniversal, out date);
            if (!result)
            {
                return false;
            }
            string xmonth = date.Month < 10 ? "0" + date.Month : date.Month.ToString();
            string xday = date.Day < 10 ? "0" + date.Day : date.Day.ToString();
            if (!date.Year.ToString().Equals(year) || !month.Equals(xmonth) || !day.Equals(xday))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证身份证规则
        /// </summary>
        /// <param name="idcard">身份证号</param>
        /// <returns></returns>
        private static bool ValidateCode(string idcard)
        {
            int sum = 0;
            char[] chars = idcard.ToCharArray();
            List<string> list = chars.Select(c => c.ToString()).ToList();
            if (list[17] == "x")
            {
                list[17] = "10";
            }
            for (int i = 0; i < 17; i++)
            {
                sum += _factors[i] * Convert.ToInt32(list[i]);
            }
            //获取验证位置
            int position = sum % 11;
            if (list[17].Equals(_codes[position].ToString()))
            {
                return true;
            }
            return false;
        }
    }
}
